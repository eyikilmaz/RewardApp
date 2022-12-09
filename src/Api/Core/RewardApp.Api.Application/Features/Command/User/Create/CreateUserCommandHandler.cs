using AutoMapper;
using MediatR;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common;
using RewardApp.Common.Events.User;
using RewardApp.Common.Exceptions;
using RewardApp.Common.Infrastructure;
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
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = dbUser.EmailAddress
            };

            QueueFactory.SendMessageToExchange(exchangeName: AppConstant.UserExchangeName,
                                               exchangeType: AppConstant.DefaultExchangeType,
                                               queueName: AppConstant.UserEmailChangedQueueName,
                                               obj: @event);
        }

        return dbUser.Id;
    }
}
