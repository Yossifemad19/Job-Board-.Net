using Core.Enums;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Company:BaseEntity,IUser
    {
        public string Name { get; set; }
        public CompanySize CompanySize { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Job>? Jobs { get; set; }
    }
}
