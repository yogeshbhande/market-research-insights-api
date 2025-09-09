namespace ResearchReportsAPI.Models
{
    public class Industry
    {
        public int Id { get; set; }

        public string IndustryName { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
