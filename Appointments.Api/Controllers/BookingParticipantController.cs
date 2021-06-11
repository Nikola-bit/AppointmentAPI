using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Services;
using AppointmentsDTO.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [Route("bookingParticipant")]
    [ApiController]
    public class BookingParticipantController : ControllerBase
    {
        private readonly IBookingParticipantService service;
        public BookingParticipantController(IBookingParticipantService _service)
        {
            service = _service;
        }
        /// <summary>
        /// Create/Update a participant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST bookingParticipant/create/update
        ///         {
        ///               "participantId": "I14kpXunkSuJIU+8++5v3Q==" (If making update),
        ///               "invitationStatusId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "bookingId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "firstName": "Peter",
        ///               "lastName": "Johnson",
        ///               "email": "peter.johnson@mail.com"
        ///         }
        /// </remarks>
        /// <returns>Participant</returns>
        /// <response code = "200">Returns the created/updated participnat.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("create/update")]
        public IActionResult CreateParticipant([FromBody] BookingParticipantsDTO info)
        {
            if (!string.IsNullOrEmpty(info.ParticipantId) && info.ParticipantId != "string")
            {
                BookingParticipantsShowDTO response = service.UpdateParticipant(info);

                if (response != null)
                {
                    return new ObjectResult(response);
                }

                else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
            }
            else
            {
                BookingParticipantsShowDTO response = service.CreateParticipant(info);

                if (response != null)
                {
                    return new ObjectResult(response);
                }

                else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
            }
        }
        /// <summary>
        /// Find a participant by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET bookingParticipant/single
        ///         {
        ///               "participantId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Participant</returns>
        /// <response code = "200">Returns the specific participant.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("single")]
        public IActionResult FindParticipantByID([FromQuery] string ID)
        {
            BookingParticipantsShowDTO response = service.FindByIdParticipant(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There is not any participant with the inserted ID.");
        }
        /// <summary>
        /// Delete a participant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET bookingParticipant/delete
        ///         {
        ///               "participantId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Participant</returns>
        /// <response code = "200">Returns the deleted participant.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("delete")]
        public IActionResult DeleteParticipant([FromQuery] string ID)
        {
            BookingParticipantsShowDTO response = service.DeleteParticipant(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There is not any participant with the inserted ID.");
        }
        /// <summary>
        /// List participants by filter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET bookingParticipant/list
        ///         {
        ///               "bookingId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "invitationStatusId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "pageNumber": 1,
        ///               "pageSize": 15
        ///         }
        /// </remarks>
        /// <returns>Participant</returns>
        /// <response code = "200">Returns the filtered participants.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("list")]
        public IActionResult ListAllParticipants([FromBody] ParticipantPaginationFilter info)
        {
            List<BookingParticipantsShowDTO> response = service.ListAllParticipants(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There aren't any participants with the custom filter or there aren't any.");
        }
    }
}
