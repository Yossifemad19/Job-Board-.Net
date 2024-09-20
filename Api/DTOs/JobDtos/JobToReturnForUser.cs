using Core.Entities;
using Core.Enums;

namespace Api.DTOs.JobDtos
{
    public class JobToReturnForUser
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }

        public JobLevel JobLevel { get; set; }

        public string CompanyId { get; set; }
        public string Company { get; set; }
        public int NumberOfCandidates { get; set; }

    }
}
