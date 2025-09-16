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

        public async Task<bool> DeleteReportByIdAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return false;

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteReportsByIndustryAsync(int industryId)
        {
            var reports = _context.Reports.Where(r => r.IndustryId == industryId).ToList();
            if (!reports.Any()) return 0;

            _context.Reports.RemoveRange(reports);
            return await _context.SaveChangesAsync();
        }
    }
}
