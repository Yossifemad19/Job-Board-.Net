using Core.Enums;

namespace Api.DTOs.CandidateDtos
{
    public class CandidateToReturnDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ResumeUrl { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public JobLevel JobLevel { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
