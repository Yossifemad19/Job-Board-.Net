using Core.Enums;

namespace Api.DTOs.CompanyDtos
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public CompanySize CompanySize { get; set; } = CompanySize.Small;
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }

    }
}
