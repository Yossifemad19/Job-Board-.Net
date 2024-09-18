using Core.Entities;
using Core.Enums;

namespace Api.DTOs.JobDtos
{
    public class JobToReturnForCompany
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }

        public JobLevel JobLevel { get; set; }

        public IEnumerable<Candidate> Candidates { get; set; }
    }
}
