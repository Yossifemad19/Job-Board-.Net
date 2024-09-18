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
    public class GenericRepository<T,Tkey> : IGenericRepository<T, Tkey> where T : BaseClass<Tkey>
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T, Tkey> spec)
        {
            var query =ApplySpecification(spec);
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Tkey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T, Tkey> spec)
        {
            var query = ApplySpecification(spec);
            return await query.SingleOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


        private IQueryable<T> ApplySpecification(ISpecification<T,Tkey> spec)
        {
            return SpecificationEvaluator<T,Tkey>.GetQuery(_dbSet, spec);
        }
    }
}
