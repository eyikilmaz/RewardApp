using MediatR;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Exceptions;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.User.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IUserRepository userRepository;
    private readonly IEmailConfirmationRepository emailConfirmationRepository;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
    {
        this.userRepository = userRepository;
        this.emailConfirmationRepository = emailConfirmationRepository;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmation = await emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

        if (confirmation is null)
            throw new DatabaseValidationException("Onaylama bulunamadı!");

        var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == confirmation.NewEmailAddress);

        if (dbUser is null)
            throw new DatabaseValidationException("Bu e-mail ile bir kullanıcı bulunamadı!");

        if (dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Email adresi zaten onaylanmış!");

        dbUser.EmailConfirmed = true;
        await userRepository.UpdateAsync(dbUser);

        return true;
    }
}
