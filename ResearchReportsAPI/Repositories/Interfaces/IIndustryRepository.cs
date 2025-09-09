using ResearchReportsAPI.Models;

namespace ResearchReportsAPI.Repositories.Interfaces
{
    public interface IIndustryRepository
    {
        Task<Industry?> GetIndustryByIdAsync(int id);
        Task<Industry?> GetIndustryByNameAsync(string name);
        Task<Industry> AddIndustryAsync(Industry industry);
    }
}
