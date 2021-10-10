using FluentValidation;
using Webchat.Models;

namespace Webchat.Validators
{
    /// <summary>
    /// Represents the validator class for handling a sign up request.
    /// </summary>
    public sealed class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        private const string PasswordRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        public SignUpRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("The username is required.")
                .MaximumLength(30).WithMessage("The username has to be less thant 30 character.");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The username is required.")
                .MaximumLength(30).WithMessage("The username has to be less thant 30 character.")
                .Matches(PasswordRegex).WithMessage("The password does not meet our security policies.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The username is required.")
                .MaximumLength(30).WithMessage("The username has to be less thant 30 character.")
                .Matches(PasswordRegex).WithMessage("The password does not meet our security policies.")
                .Equal(x => x.Password).WithMessage("The password and the confirmation are different.");
        }
    }
}
