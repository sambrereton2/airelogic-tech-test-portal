using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PatientAppointmentBackend.Service.Models;
using PatientAppointmentBackend.Service.Services.Interfaces;
using PatientAppointmentBackend.Shared.ApiModels;
using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Service.Controllers
{
    /// <summary>
    /// Create and Update Patient information
    /// </summary>
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientService patientService,
            ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }


        /// <summary>
        /// Query a Patient's record
        /// </summary>
        /// <param name="id">the NhsNumber of the patient to query</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Patient/{id}")]
        public async Task<IActionResult> GetPatient(string? id, CancellationToken cancellationToken)
        {
            if (!NhsNumberValidator.Validate(id))
            {
                return BadRequest("Not a valid NhsNumber");
            }
            try
            {
                var patientModel = await _patientService.GetPatientAsync(id, cancellationToken);
                if (patientModel == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(patientModel);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

            // Ideally want ModelState validity checking done globally
            if (!ModelState.IsValid)
            {                
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var responseObj = new { Errors = errors };
                return new BadRequestObjectResult(responseObj);
            }

            try
            {
                await _patientService.AddPatientAsync(request, false, cancellationToken);
                return Ok("Patient Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a Patient's record
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("api/Patient")]
        public async Task<IActionResult> UpdatePatient([FromBody] NewPatient request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Ideally want ModelState validity checking done globally
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var responseObj = new { Errors = errors };
                return new BadRequestObjectResult(responseObj);
            }

            try
            {
                await _patientService.AddPatientAsync(request, true, cancellationToken);
                return Ok("Patient Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
