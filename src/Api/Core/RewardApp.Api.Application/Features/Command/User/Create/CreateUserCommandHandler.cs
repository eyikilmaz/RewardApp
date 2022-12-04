using AutoMapper;
using MediatR;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Exceptions;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if(existsUser is not null)
            throw new DatabaseValidationException("Kullanıcı zaten kayıtlı.");

        var dbUser = mapper.Map<Domain.Models.User>(request);

        var rows = await userRepository.AddAsync(dbUser);

        if (rows > 0)
        {
            // TODO rabbitmq sendMail
        }

        return dbUser.Id;
    }
}
