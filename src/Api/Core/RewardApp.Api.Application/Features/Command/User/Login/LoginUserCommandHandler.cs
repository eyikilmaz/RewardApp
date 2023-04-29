using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Domain.Models;
using RewardApp.Common.Exceptions;
using RewardApp.Common.Infrastructure;
using RewardApp.Common.Models.Queries.User;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.User.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;
    private readonly IRewardUserRepository rewardUserRepository;
    private readonly IRewardRepository rewardRepository;
    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, IRewardUserRepository rewardUserRepository, IRewardRepository rewardRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.rewardUserRepository = rewardUserRepository;
        this.rewardRepository = rewardRepository;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (dbUser == null)
            throw new DatabaseValidationException("User not found!");

        var pass = PasswordEncryptor.Encrpt(request.Password);
        pass = request.Password;
        if (dbUser.Password != pass)
            throw new DatabaseValidationException("Password is wrong!");

        if (!dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Email address is not confirmed yet!");

        var result = mapper.Map<LoginUserViewModel>(dbUser);

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.EmailAddress),
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName)
        };

        result.Token = GenerateToken(claims);

       await GenerateFirstRewardInit(result.Id);

        return result;
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(10);

        var token = new JwtSecurityToken(claims: claims,
                                         expires: expiry,
                                         signingCredentials: creds,
                                         notBefore: DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task GenerateFirstRewardInit(Guid userId)
    {
        var exists = rewardUserRepository.GetList(i => i.UserId == userId).Result;

        if (exists.Any())
            return;

        var rewards = await rewardRepository.GetAll();
        var bombRewards = rewards.Where(r => r.RewardName == "Bomb");
        var defaultRewardsNotBomb = rewards.Where(r => r.RewardName != "Bomb" && r.IsDefault);

        List<RewardUserDetail> rewardUserDetails = new List<RewardUserDetail>();

        var totalCount = 60;
        int totalAdded = 0;
        foreach (var reward in rewards)
        {
            if (reward.IsDefault)
            {
                int defaultCount = 30;
                if (reward.RewardName != "Bomb")
                    defaultCount = reward.Repeat * 2;

                totalAdded += defaultCount;

                FillRewardDetail(rewardUserDetails, reward, defaultCount);
            }
            else
            {
                totalAdded += reward.Repeat;

                FillRewardDetail(rewardUserDetails, reward, reward.Repeat);
            }
        }

        foreach (var drnb in defaultRewardsNotBomb)
        {
            totalAdded += drnb.Repeat;
            FillRewardDetail(rewardUserDetails, drnb, drnb.Repeat);
        }

        foreach (var rb in bombRewards)
        {
            FillRewardDetail(rewardUserDetails, rb, totalCount - totalAdded);
        }

        Domain.Models.RewardUser rewardUser = new Domain.Models.RewardUser()
        {
            UserId = userId,
            RewardUserDetails = rewardUserDetails
        };

        await rewardUserRepository.AddAsync(rewardUser);
    }

    private void FillRewardDetail(List<RewardUserDetail> rewardUserDetails, Reward reward, int repeat)
    {
        for (int i = 0; i < repeat; i++)
        {
            rewardUserDetails.Add(new RewardUserDetail
            {
                CreateDate = reward.CreateDate,
                CurrentRewardId = reward.Id,
                Description = reward.Description,
                IsOpen = false,
                LastRewardId = new Guid(),
                Mod = reward.Mod,
                Uid = Guid.NewGuid()
            });
        }
    }
}
