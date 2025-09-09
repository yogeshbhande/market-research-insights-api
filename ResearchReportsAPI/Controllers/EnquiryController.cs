using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearchReportsAPI.Models;
using ResearchReportsAPI.Services;

namespace ResearchReportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly EnquiryService _enquiryService;

        public EnquiryController(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitEnquiry([FromBody] EnquiryDto enquiryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var enquiry = await _enquiryService.AddEnquiryAsync(enquiryDto);

                return Ok(new
                {
                    message = "Thank you! Your enquiry has been submitted successfully.",
                    data = enquiry
                });
            }
            catch (ArgumentException ex) // For invalid inputs
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    error = "Invalid data provided"
                });
            }
            catch (InvalidOperationException ex) // For business logic errors
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    error = "Operation failed"
                });
            }
            catch (Exception ex) // For any unexpected exceptions
            {
                // Log the exception here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An unexpected error occurred while submitting your enquiry.",
                    error = ex.Message
                });
            }
        }
    }
}
