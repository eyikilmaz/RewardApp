using AutoMapper;
using MediatR;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Exceptions;
using RewardApp.Common.Infrastructure;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.User.ChangePassword;

internal class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IUserRepository userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
            throw new ArgumentNullException(nameof(request.UserId));

        var dbUser = await userRepository.GetByIdAsync(request.UserId.Value);

        if (dbUser is null)
            throw new DatabaseValidationException("Kullanıcı bulunamadı!");

        var encPass = PasswordEncryptor.Encrpt(request.OldPassword);
        if (dbUser.Password != encPass)
            throw new DatabaseValidationException("Eski parolanız yanlış!");

        dbUser.Password = PasswordEncryptor.Encrpt(request.NewPassword);

        await userRepository.UpdateAsync(dbUser);

        return true;
    }
}
