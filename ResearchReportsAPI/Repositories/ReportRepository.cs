using Microsoft.EntityFrameworkCore;
using ResearchReportsAPI.Data;
using ResearchReportsAPI.Models;
using ResearchReportsAPI.Repositories.Interfaces;

namespace ResearchReportsAPI.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ResearchReportsDbContext _context;
        public ReportRepository(ResearchReportsDbContext context)
        {
            _context = context;
        }

        public async Task<Report?> GetReportByIdAsync(int id)
        {
            return await _context.Reports.Include(r => r.Industry)
                                         .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Report>> GetReportsByIndustry(int industryId)
        {
            return await _context.Reports
                                 .Where(r => r.IndustryId == industryId)    
                                 .ToListAsync();
        }
        public async Task AddReportsAsync(IEnumerable<Report> reports)
        {
            try
            {
                await _context.Reports.AddRangeAsync(reports);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {

            }

        }
    }
}
