using Microsoft.EntityFrameworkCore;
using ResearchReportsAPI.Data;
using ResearchReportsAPI.Models;
using ResearchReportsAPI.Repositories.Interfaces;

namespace ResearchReportsAPI.Repositories
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly ResearchReportsDbContext _context;
        public IndustryRepository(ResearchReportsDbContext context)
        {
            _context = context;
        }

        public async Task<Industry?> GetIndustryByIdAsync(int id)
        {
            return await _context.Industries.FindAsync(id);
        }

        public async Task<Industry?> GetIndustryByNameAsync(string name)
        {
            return await _context.Industries.FirstOrDefaultAsync(i => i.IndustryName == name);
        }

        public async Task<Industry> AddIndustryAsync(Industry industry)
        {
            _context.Industries.Add(industry);
            await _context.SaveChangesAsync();
            return industry;
        }
    }
}
