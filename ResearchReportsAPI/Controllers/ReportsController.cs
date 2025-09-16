using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearchReportsAPI.Repositories.Interfaces;

namespace ResearchReportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly ExcelService _excelService;
        public ReportsController(IReportRepository reportRepository, ExcelService excelService)
        {
            _reportRepository = reportRepository;
            _excelService = excelService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var report = await _reportRepository.GetReportByIdAsync(id);
            return report == null ? NotFound() : Ok(report);
        }

        [HttpGet("GetReportsByIndustry/{industryId}")]
        public async Task<IActionResult> GetReportsByIndustry(int industryId)
        {
            try
            {
                var reports = await _reportRepository.GetReportsByIndustry(industryId);

                if (reports == null || !reports.Any())
                    return NotFound($"No reports found for IndustryId {industryId}.");

                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel([FromForm] IFormFile[] files)
        {
            try
            {
                if (files == null || files.Length == 0)
                    return BadRequest("No files uploaded.");

                int inserted = await _excelService.ImportReportsAsync(files);
                return Ok(new { message = $"{inserted} reports inserted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _reportRepository.DeleteReportByIdAsync(id);
                if (!deleted)
                    return NotFound($"Report with Id {id} not found.");

                return Ok(new { message = $"Report with Id {id} deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteByIndustry/{industryId}")]
        public async Task<IActionResult> DeleteByIndustry(int industryId)
        {
            try
            {
                var deletedCount = await _reportRepository.DeleteReportsByIndustryAsync(industryId);

                if (deletedCount == 0)
                    return NotFound($"No reports found for IndustryId {industryId}.");

                return Ok(new { message = $"{deletedCount} reports deleted successfully for IndustryId {industryId}." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
