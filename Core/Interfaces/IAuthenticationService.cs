using Core.Entities;

namespace Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterAsUser(AppUser user, string password);
        Task<bool> RegisterAsCompany(Company company, string password);

        Task<AppUser> LoginAsUser(string email, string password);
        Task<Company> LoginAsCompany(string email, string password);

        Task<bool> DoesUserExist(string email);
        Task<bool> DoesCompanyExist(string email);

    }
}
