using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseClass<Tkey>
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repoType = typeof(GenericRepository<,>);
                var repo = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repo);

            }

            return (IGenericRepository<TEntity, Tkey>)_repositories[type];
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_repositories == null)
                    _repositories = new Hashtable();
                var type = typeof(AppUser).Name;

                if (!_repositories.ContainsKey(type))
                {
                    var repo = new UserRepository(_context);
                    _repositories.Add(type, repo);
                }
                return (IUserRepository)_repositories[type];
            }
        }

        public ICompanyRepository CompanyRepository
        {
            get
            {
                if (_repositories == null)
                    _repositories = new Hashtable();
                var type = typeof(Company).Name;

                if (!_repositories.ContainsKey(type))
                {
                    var repo = new CompanyRepository(_context);
                    _repositories.Add(type, repo);
                }
                return (ICompanyRepository)_repositories[type];
            }
        }
    }
}
