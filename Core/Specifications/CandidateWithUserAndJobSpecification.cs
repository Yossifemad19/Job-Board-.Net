using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class CandidateWithUserAndJobSpecification : BaseSpecification<Candidate, int>
    {
        public CandidateWithUserAndJobSpecification()
        {
            AddInclude(x => x.User);
            AddInclude(x => x.Job);
            AddInclude(x => x.Job.Company);
        }

        public CandidateWithUserAndJobSpecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(x=>x.User);
            AddInclude(x=>x.Job);
            AddInclude(x=>x.Job.Company);
        }
    }
}
