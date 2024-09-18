using Core.Enums;

namespace Api.DTOs.JobDtos
{
    public class PostJobDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public JobLevel JobLevel { get; set; }
    }
}
