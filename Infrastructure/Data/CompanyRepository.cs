using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CompanyRepository : GenericRepository<Company, string>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task<Company> GetCompanyByEmailAsync(string email)
        {
            return await _context.Companies.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
