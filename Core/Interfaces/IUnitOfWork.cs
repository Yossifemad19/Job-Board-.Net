using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        Task<int> CompleteAsync();
        IGenericRepository<TEntity,Tkey> Repository<TEntity,Tkey>() where TEntity:BaseClass<Tkey>;

        IUserRepository UserRepository {  get; }
        ICompanyRepository CompanyRepository { get; }
    }
}
