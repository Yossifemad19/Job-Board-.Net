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
    public static class SpecificationEvaluator<T,Tkey> where T:BaseClass<Tkey>
    {
        public static IQueryable<T> GetQuery(IQueryable<T> queryInput,ISpecification<T,Tkey> spec)
        {
            var query = queryInput.AsQueryable();
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (curr, include) =>
                        curr.Include(include));

            return query;
        }
    }
}
