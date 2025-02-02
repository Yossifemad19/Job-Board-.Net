﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISpecification<T,Tkey> where T : BaseClass<Tkey>
    {
        public Expression<Func<T,bool>> Criteria { get;  }
        public List<Expression<Func<T,object>>> Includes { get; }
    }
}
