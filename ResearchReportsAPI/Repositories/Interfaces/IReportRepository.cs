using ResearchReportsAPI.Models;

namespace ResearchReportsAPI.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<Report?> GetReportByIdAsync(int id);
        Task<IEnumerable<Report>> GetReportsByIndustry(int id);
        Task AddReportsAsync(IEnumerable<Report> reports);
        Task<bool> DeleteReportByIdAsync(int id);
        Task<int> DeleteReportsByIndustryAsync(int industryId);

    }
}
