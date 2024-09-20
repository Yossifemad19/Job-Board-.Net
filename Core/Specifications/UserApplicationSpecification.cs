using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UserApplicationSpecification : BaseSpecification<Candidate, int>
    {
        public UserApplicationSpecification()
        {
            AddInclude(x => x.User);
            AddInclude(x => x.Job);
            AddInclude(x => x.Job.Company);
        }

        public UserApplicationSpecification(string userId) : base(x=>x.UserId==userId)
        {
            AddInclude(x=>x.User);
            AddInclude(x=>x.Job);
            AddInclude(x=>x.Job.Company);
        }
    }
}
