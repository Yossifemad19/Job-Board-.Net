using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUnitOfWork unitOfWork,ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsUser(AppUser user, string password)
        {

            user.PasswordHash = GetHashedPassword(password);
            _unitOfWork.UserRepository.Add(user);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> RegisterAsCompany(Company company, string password)
        {

            company.PasswordHash = GetHashedPassword(password);
            _unitOfWork.CompanyRepository.Add(company);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<AppUser> LoginAsUser(string email, string password)
        {
            var user =await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (user == null || !VerifyPassword(password,user.PasswordHash) ) {
                return null;
            }
            return user;
        }

        public async Task<Company> LoginAsCompany(string email, string password)
        {
            var company = await _unitOfWork.CompanyRepository.GetCompanyByEmailAsync(email);
            if (company == null || !VerifyPassword(password, company.PasswordHash))
            {
                return null;
            }
            return company;
        }

        public async Task<bool> DoesUserExist(string email)
        {
            
            return  (await _unitOfWork.UserRepository.GetUserByEmailAsync(email) != null);
        }

        public async Task<bool> DoesCompanyExist(string email)
        {

            return (await _unitOfWork.CompanyRepository.GetCompanyByEmailAsync(email) != null);
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
