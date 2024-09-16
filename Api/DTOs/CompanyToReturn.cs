using Core.Enums;

namespace Api.DTOs
{
    public class CompanyToReturn
    {
        public string Name { get; set; }
        public CompanySize CompanySize { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
