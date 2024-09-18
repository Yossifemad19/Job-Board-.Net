using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T, Tkey> : ISpecification<T, Tkey> where T : BaseClass<Tkey>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; }=new List<Expression<Func<T, object>>>();

        public BaseSpecification(Expression<Func<T, bool>> creiteria)
        {
            Criteria = creiteria;
        }
        public BaseSpecification()
        {

        }
        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
    }
}
