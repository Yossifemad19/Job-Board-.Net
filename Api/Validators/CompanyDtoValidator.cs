using Api.DTOs;
using FluentValidation;

namespace Api.Validators
{
    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x=>x.CompanySize).NotEmpty()
                .IsInEnum()
                .WithMessage("value must be in enum")
                ;
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");


            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");
            RuleFor(x => x.ConfirmedPassword).NotEmpty().Equal(x => x.Password).WithMessage("Password not match");
        }
    }
}
