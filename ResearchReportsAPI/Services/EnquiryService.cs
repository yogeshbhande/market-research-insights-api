using ResearchReportsAPI.Models;
using ResearchReportsAPI.Repositories.Interfaces;

namespace ResearchReportsAPI.Services
{
    public class EnquiryService
    {
        private readonly IEnquiryRepository _enquiryRepository;

        public EnquiryService(IEnquiryRepository enquiryRepository)
        {
            _enquiryRepository = enquiryRepository;
        }

        public async Task<Enquiry> AddEnquiryAsync(EnquiryDto dto)
        {
            var enquiry = new Enquiry
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CountryCode = dto.CountryCode,
                CompanyName = dto.CompanyName
            };

            return await _enquiryRepository.AddEnquiryAsync(enquiry);
        }
    }
}
