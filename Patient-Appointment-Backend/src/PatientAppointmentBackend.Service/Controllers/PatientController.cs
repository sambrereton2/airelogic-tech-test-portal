using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PatientAppointmentBackend.Service.Models;
using PatientAppointmentBackend.Shared.ApiModels;

namespace PatientAppointmentBackend.Service.Controllers
{
    /// <summary>
    /// Create and Update Patient information
    /// </summary>
    public class PatientController : ControllerBase
    {
        /// <summary>
        /// Test Function
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Patient")]
        public async Task<string> GetConfig(CancellationToken cancellationToken)
        {
            await Task.Delay(20);
            return "Hello";
        }

        /// <summary>
        /// Create a new Patient record
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Patient")]
        public async Task<IActionResult> CreatePatient([FromBody] NewPatient request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!ModelState.IsValid)
            {
                //return BadRequest();
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var responseObj = new { Errors = errors };
                return new BadRequestObjectResult(responseObj);
            }

            await Task.Delay(20);
            //return "Hello";
            return Ok("Hello");
        }
    }
}
