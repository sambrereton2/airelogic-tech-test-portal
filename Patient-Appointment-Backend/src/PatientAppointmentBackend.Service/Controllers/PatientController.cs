using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PatientAppointmentBackend.Service.Models;
using PatientAppointmentBackend.Service.Services.Interfaces;
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
        private readonly IStringLocalizer<PatientController> _stringLocalizer;

        public PatientController(IPatientService patientService,
            ILogger<PatientController> logger,
            IStringLocalizer<PatientController> stringLocalizer)
        {
            _patientService = patientService;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }


        /// <summary>
        /// Query a Patient's record
        /// </summary>
        /// <param name="id" example="1373645350">the NhsNumber of the patient to query</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Patient/{id}")]
        public async Task<IActionResult> GetPatient(string? id, CancellationToken cancellationToken)
        {
            if (!NhsNumberValidator.Validate(id))
            {
                return BadRequest(_stringLocalizer[ResourceKeys.NhsNumberInvalid]);
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
                        
            if (!ModelState.IsValid)
            {               
                var responseObj = new { Errors = GetModelStateErrors() };
                return new BadRequestObjectResult(responseObj);
            }

            try
            {
                await _patientService.AddPatientAsync(request, false, cancellationToken);
                return Ok(_stringLocalizer[ResourceKeys.PatientCreated]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception creating a new user");
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

            if (!ModelState.IsValid)
            {
                var responseObj = new { Errors = GetModelStateErrors() };
                return new BadRequestObjectResult(responseObj);
            }

            try
            {
                await _patientService.AddPatientAsync(request, true, cancellationToken);
                return Ok(_stringLocalizer[ResourceKeys.PatientUpdated]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private List<string> GetModelStateErrors()
        {
            List<string> formattedErrors = new List<string>();
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

            foreach (var error in errors)
            {
                formattedErrors.Add(_stringLocalizer[error]);
            }
            return formattedErrors;
        }
    }
}
