using ResearchReportsAPI.Data;
using ResearchReportsAPI.Models;
using ResearchReportsAPI.Repositories.Interfaces;

namespace ResearchReportsAPI.Repositories
{
    public class EnquiryRepository : IEnquiryRepository
    {
        private readonly ResearchReportsDbContext _context;


        public EnquiryRepository(ResearchReportsDbContext context)
        {
            _context = context;
        }

        public async Task<Enquiry> AddEnquiryAsync(Enquiry enquiry)
        {
            _context.Enquiry.Add(enquiry);
            await _context.SaveChangesAsync();
            return enquiry;
        }
    }
}
