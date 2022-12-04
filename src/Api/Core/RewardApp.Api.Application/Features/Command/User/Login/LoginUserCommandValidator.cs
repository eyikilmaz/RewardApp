using FluentValidation;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.User.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
	public LoginUserCommandValidator()
	{
        RuleFor(i => i.EmailAddress)
            .NotEmpty()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} geçerli olmayan e-mail adresi.");

        RuleFor(i => i.Password)
            .NotNull()
            .MinimumLength(6)
            .WithMessage("{PropertyName} en az {MinLenght} karakter olmalı.");
    }
}
