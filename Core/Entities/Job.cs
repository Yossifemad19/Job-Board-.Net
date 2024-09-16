using Core.Enums;

namespace Core.Entities
{
    public class Job:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public JobLevel JobLevel { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
    }
}