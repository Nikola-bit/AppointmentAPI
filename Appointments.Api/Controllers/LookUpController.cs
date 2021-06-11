using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Services.Interface;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [Route("lookUp")]
    [ApiController]
    public class LookUpController : ControllerBase
    {
        private readonly IPlatformConfigurationService service;

        public LookUpController(IPlatformConfigurationService _service)
        {
            service = _service;

        }
        /// <summary>
        /// Get booking invitation statuses. 
        /// </summary>
        /// <returns>LookUp</returns>
        /// <response code = "200">Returns all booking invitation statuses.</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("invitationStatuses/list")]
        public IActionResult GetInvitationStatuses()
        {
            List<PlatformConfigurationDTO> response = service.GetBookingInvitationStatuses();

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There aren't any members of this category.");
        }
        /// <summary>
        /// Get room attributes.
        /// </summary>
        /// <returns>LookUp</returns>
        /// <response code = "200">Returns all room attributes.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("roomAttributes/list")]
        public IActionResult GetRoomAttributes()
        {
            List<PlatformConfigurationDTO> response = service.GetRoomAttributes();

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There aren't any members of this category.");
        }
        /// <summary>
        /// Get recurrence type.
        /// </summary>
        /// <returns>LookUp</returns>
        /// <response code = "200">Returns all recurrence type.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("recurrenceType/list")]
        public IActionResult GetType()
        {
            List<PlatformConfigurationDTO> response = service.RecurrenceType();

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There aren't any members of this category.");
        }
        /// <summary>
        /// Get weekdays.
        /// </summary>
        /// <returns>LookUp</returns>
        /// <response code = "200">Returns all weekdays.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("weekday/list")]
        public IActionResult GetWeekdays()
        {
            List<PlatformConfigurationDTO> response = service.GetWeekday();

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There aren't any members of this category.");
        }

    }
}
