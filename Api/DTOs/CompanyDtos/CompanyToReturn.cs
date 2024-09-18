using Core.Enums;

namespace Api.DTOs.CompanyDtos
{
    public class CompanyToReturn
    {
        public string Name { get; set; }
        public CompanySize CompanySize { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
