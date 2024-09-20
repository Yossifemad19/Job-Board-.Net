using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class CompanyJobCandidatesSpecification : BaseSpecification<Candidate, int>
    {
        public CompanyJobCandidatesSpecification()
        {
            AddInclude(x => x.User);
            AddInclude(x => x.Job);
            AddInclude(x => x.Job.Company);
        }

        public CompanyJobCandidatesSpecification(string companyId,int jobId) : base(x=>x.JobId==jobId&&x.Job.CompanyId==companyId)
        {
            AddInclude(x => x.User);
            AddInclude(x => x.Job);
            AddInclude(x => x.Job.Company);
        }
    }
}
