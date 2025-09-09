using ResearchReportsAPI.Models;

namespace ResearchReportsAPI.Repositories.Interfaces
{
    public interface IEnquiryRepository
    {
        Task<Enquiry> AddEnquiryAsync(Enquiry enquiry);
    }
}
