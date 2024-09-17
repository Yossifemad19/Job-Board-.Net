namespace Core.Entities
{
    public class Candidate:BaseEntity<int>
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        // Relations

        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}