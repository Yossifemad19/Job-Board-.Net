using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthenticationService(ApplicationDbContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsUser(AppUser user, string password)
        {

            user.PasswordHash = GetHashedPassword(password);
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RegisterAsCompany(Company company, string password)
        {

            company.PasswordHash = GetHashedPassword(password);
            _context.Companies.Add(company);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AppUser> LoginAsUser(string email, string password)
        {
            var user =await _context.Users.SingleOrDefaultAsync(u=>u.Email==email);
            if (user == null || !VerifyPassword(password,user.PasswordHash) ) {
                return null;
            }
            return user;
        }

        public async Task<Company> LoginAsCompany(string email, string password)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(u => u.Email == email);
            if (company == null || !VerifyPassword(password, company.PasswordHash))
            {
                return null;
            }
            return company;
        }

        public async Task<bool> DoesUserExist(string email)
        {
            
            return  (await _context.Users.FirstOrDefaultAsync(u => u.Email==email) != null);
        }

        public async Task<bool> DoesCompanyExist(string email)
        {

            return (await _context.Companies.FirstOrDefaultAsync(u => u.Email == email) != null);
        }

        private string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password,string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
