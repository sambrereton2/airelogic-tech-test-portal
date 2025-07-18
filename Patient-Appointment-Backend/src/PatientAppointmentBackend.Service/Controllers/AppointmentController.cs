using Microsoft.AspNetCore.Mvc;
using PatientAppointmentBackend.Service.Models;
using PatientAppointmentBackend.Service.Services.Interfaces;
using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Service.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new Appointment record
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Appointment ID</returns>
        [ProducesResponseType(typeof(AppointmentId), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("api/Appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] NewAppointment request, CancellationToken cancellationToken)
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
                var result = await _appointmentService.CreateAppointment(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Attend an Appointment
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        [Route("api/Appointment/Attend")]
        public async Task<IActionResult> AttendAppointment([FromBody] AppointmentId request, CancellationToken cancellationToken)
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
                await _appointmentService.Attend(request, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancel an Appointment
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        [Route("api/Appointment/Cancel")]
        public async Task<IActionResult> CancelAppointment([FromBody] AppointmentId request, CancellationToken cancellationToken)
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
                await _appointmentService.Cancel(request, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
