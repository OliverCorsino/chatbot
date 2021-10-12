using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Webchat.Models;

namespace Webchat.Validators
{
    /// <summary>
    /// Represents the validator class for handling a sign ip request.
    /// </summary>
    public sealed class SignInRequestValidator : AbstractValidator<AuthRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("The username is required.")
                .MaximumLength(30).WithMessage("The username has to be less thant 30 character.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The password is required.")
                .MaximumLength(30).WithMessage("The password has to be less thant 30 character.");
        }
    }
}
