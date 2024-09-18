using Core.Enums;

namespace Core.Entities
{
    public class Job:BaseEntity<int>
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public JobLevel JobLevel { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Candidate> Candidates { get; set; }=new List<Candidate>();

        public int GetCandidates()
        {
            return Candidates.Count;
        }
    }
}