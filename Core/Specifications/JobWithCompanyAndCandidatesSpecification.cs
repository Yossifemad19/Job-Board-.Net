using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class JobWithCompanyAndCandidatesSpecification : BaseSpecification<Job, int>
    {
        public JobWithCompanyAndCandidatesSpecification()
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.Candidates);
        }

        public JobWithCompanyAndCandidatesSpecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.Candidates);
        }
    }
}
