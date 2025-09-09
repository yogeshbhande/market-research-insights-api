using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchReportsAPI.Models
{
    [Table("Enquiries")]
    public class Enquiry
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CountryCode { get; set; }
        public string? CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
