namespace Api.DTOs.UserDtos
{
    public class UserResiterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }
        public IFormFile? Resume { get; set; }
    }
}
