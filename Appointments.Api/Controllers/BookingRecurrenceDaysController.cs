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
    [Route("BookingRecurrenceDays")]
    [ApiController]
    public class BookingRecurrenceDaysController : ControllerBase
    {
        private readonly IBookingRecurrenceDaysService service;

        public BookingRecurrenceDaysController(IBookingRecurrenceDaysService _service)
        {
            service = _service;

        }
        /// <summary>
        /// Get All BookingRecurrenceDays
        /// </summary>
        /// <returns>All BookingRecurrenceDays</returns>
        /// <response code = "200">Return all BookingRecurrenceDays</response>
        /// <response code = "400">Some error occured</response>
        /// 
        [HttpPost("list")]
        public IActionResult GetAllB([FromBody] PaginationFilter filter)
        {
            PagedResponse<List<BookingRecurrenceDaysDTO>> list = service.GetAll(filter);
            return new ObjectResult(list);
        }
        /// <summary>
        /// Get BookingRecurrenceDays By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/Booking/Recurrence/Days
        ///     {
        ///         "Id" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single BookingRecurrenceDays</returns>
        /// <response code = "200">Return BookingRecurrenceDays by Id</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("single")]
        public IActionResult GetDById([FromQuery] string id)
        {
            BookingRecurrenceDaysDTO bdays = service.GetById(id);
            if (bdays != null)
            {
                return new ObjectResult(bdays);
            }
            else
            {
                return Content("Room with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new Booking Recurrence Day
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create/Booking Recurrence Day
        ///         {
        ///               "Id": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "RecurrenceId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "Weekly" : I14kpXunkSuJIU+8++5v3Q==
        ///         }
        /// </remarks>
        /// <param name="bookDCreate"></param>
        /// <returns>Updated list of Booking Recurrence Day</returns>
        /// <response code = "200">Return updated list of Booking Recurrence Day</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("create/update")]
        public IActionResult AddUpdate([FromBody] BookingRecurrenceDaysDTO bookDCreate)
        {
            if (bookDCreate.Id != "string" && !string.IsNullOrEmpty(bookDCreate.Id))
            {
                bool result = service.Update(bookDCreate);
                return new ObjectResult(result);
            }
            else
            {
                bookDCreate.RecurrenceId = bookDCreate.RecurrenceId == "string" ? "" : bookDCreate.RecurrenceId;
                if (string.IsNullOrEmpty(bookDCreate.RecurrenceId) || string.IsNullOrEmpty(bookDCreate.Weekday.ToString()))
                {
                    return BadRequest("Missing items");
                }
                BookingRecurrenceDaysDTO newD = service.AddNew(bookDCreate);
                return new ObjectResult(newD);
            }
        }
        /// <summary>
        /// Remove BookingRecurrenceDays by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/BookingRecurrenceDays
        ///     {
        ///         "Id" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single BookingRecurrenceDays</returns>
        /// <response code = "200">Return single BookingRecurrenceDays</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("remove")]
        public IActionResult RemoveRoom([FromQuery] string id)
        {
            bool result = service.Remove(id);
            return new ObjectResult(result);
        }
    }
}
