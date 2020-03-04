using FluentValidation;
using FluentValidation.Validators;

namespace Blog.Services.Users
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.UserName).Length(4, 60);
            RuleFor(x => x.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(60);
        }
    }
}