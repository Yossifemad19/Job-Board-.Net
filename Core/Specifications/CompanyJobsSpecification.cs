using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class CompanyJobsSpecification : BaseSpecification<Company, string>
    {
        public CompanyJobsSpecification()
        {
            AddInclude(c => c.Jobs);

        }

        public CompanyJobsSpecification(string id ) : base(c=>c.Id==id)
        {
            AddInclude(c => c.Jobs);
        }
    }
}
