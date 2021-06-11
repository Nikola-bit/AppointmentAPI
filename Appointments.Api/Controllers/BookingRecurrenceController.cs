using Appointments.Api.Services;
using AppointmentsDTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Appointments.Api.Controllers
{
    [Route("bookingRecurrence")]
    [ApiController]
    public class BookingRecurrenceController : ControllerBase
    {
        public IBookingRecurrenceService service;
        public BookingRecurrenceController(IBookingRecurrenceService _service)
        {
            service = _service;
        }
        /// <summary>
        /// Create a booking recurrence.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST bookingRecurrence/create
        ///         {
        ///               "recurrenceId": "I14kpXunkSuJIU+8++5v3Q==" (If making update),
        ///               "userId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "roomId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "startTime": "07:30:00",
        ///               "endTime": "07:30:00",
        ///               "startDate": "2020-09-16",
        ///               "endDate": "2020-10-16", (If from date to date)
        ///               "endAfter" : 30, (If by number of meetings)
        ///               "NoEndDate" : true (If not known until when)
        ///               "bookingRecurrenceDays" :
        ///               {
        ///                     {
        ///                         "Weekday" : "I14kpXunkSuJIU+8++5v3Q=="
        ///                     },
        ///                     {
        ///                         "Weekday" : "I14kpXunkSuJIU+8++5v3Q=="
        ///                     }
        ///               }
        ///         }
        /// </remarks>
        /// <returns>BookingRecurrence</returns>
        /// <response code = "200">Returns the created booking recurrence.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("create")]
        public IActionResult CreateARecurrence(BookingRecurrenceCreateDTO info)
        {
            if (info.NoEndDate == true)
            {
                BookingRecurrenceDTO response = service.CreateBookingRecurrenceByNoDate(info);
                return new ObjectResult(response);
            }
            else if (info.EndAfter != null && info.EndAfter != 0)
            {
                BookingRecurrenceDTO response = service.CreateBookingRecurrenceByDays(info);
                return new ObjectResult(response);
            }
            else if (info.EndDate != null)
            {
                BookingRecurrenceDTO response = service.CreateBookingRecurrenceByDate(info);

                return new ObjectResult(response);
            }
            else
            {
                return new BadRequestObjectResult("you need to fill one of the last 3 rows.");
            }
            
        }
        /// <summary>
        /// Find a booking recurrence.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET bookingRecurrence/single
        ///         {
        ///               "recurrenceId": "I14kpXunkSuJIU+8++5v3Q==" (If making update),
        ///         }
        /// </remarks>
        /// <returns>BookingRecurrence</returns>
        /// <response code = "200">Returns the searched booking recurrence.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("single")]
        public IActionResult GetBookingRecurrence(string ID)
        {
            BookingRecurrenceDTO response = service.GetBookingRecurrence(ID);

            return new ObjectResult(response);
        }
        /// <summary>
        /// Delete a booking recurrence.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET bookingRecurrence/delete
        ///         {
        ///               "recurrenceId": "I14kpXunkSuJIU+8++5v3Q==",
        ///         }
        /// </remarks>
        /// <returns>BookingRecurrence</returns>
        /// <response code = "200">Returns the deleted booking recurrence.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("delete")]
        public IActionResult DeleteBookingRecurrence(string ID)
        {
            BookingRecurrenceDTO response = service.DeleteBookingRecurrence(ID);
            return new ObjectResult(response);
        }
        /// <summary>
        /// List booking recurrences.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST bookingRecurrence/list
        ///         {
        ///               "roomId" : "I14kpXunkSuJIU+8++5v3Q==",
        ///               "pageNumber": 1,
        ///               "pageSize": 15
        ///         }
        /// </remarks>
        /// <returns>BookingRecurrence</returns>
        /// <response code = "200">Returns the filtered booking recurrence.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("list")]
        public IActionResult ListBookingRecurrences(RecurrencePaginationFilter info)
        {
            List<BookingRecurrenceDTO> response = service.ListBookingRecurrences(info);

            return new ObjectResult(response);
        }

    }
}
