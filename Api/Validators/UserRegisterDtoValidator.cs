using Api.DTOs.UserDtos;
using FluentValidation;

namespace Api.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserResiterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x=>x.Phone)
                .Matches(@"^0\d{10}$")
                .When(x => x.Phone is not null)
                          .WithMessage("Invalid phone number format.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.ConfirmedPassword).NotEmpty().Equal(x => x.Password).WithMessage("Password not match");

            RuleFor(x => x.Resume)
                .Must(IsValidExtension).When(x=>x.Resume is not null).WithMessage("file must be in this types [\".jpg\", \".jpeg\", \".png\", \".pdf\"]")
                .Must(IsValidSize).When(x => x.Resume is not null).WithMessage("file must be less than 5 MB");
        }

        private bool IsValidSize(IFormFile file)
        {
            const int maxFileSize = 5 * 1024 * 1024; // 5 MB
            return file.Length < maxFileSize;
        }

        private bool IsValidExtension(IFormFile file)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return validExtensions.Contains(fileExtension);
        }
    }
}
