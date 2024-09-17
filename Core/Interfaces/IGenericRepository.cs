using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T,Tkey>where T : BaseClass<Tkey>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> GetByIdAsync(Tkey id);

        Task<IEnumerable<T>> GetAllAsync();

    }
}
