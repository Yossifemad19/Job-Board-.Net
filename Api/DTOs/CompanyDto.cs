using Core.Enums;

namespace Api.DTOs
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public CompanySize CompanySize { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }

    }
}
