using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Models;
using Appointments.Api.Services.Interface;
using Appointments.Api.Utilities;
using AppointmentsDTO;
using AppointmentsDTO.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Appointments.Api.Controllers
{
    [Route("booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public IBookingService service;
        public BookingController(IBookingService _service)
        {
            service = _service;
        }
        /// <summary>
        /// Create/Update a booking.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST booking/create/update
        ///         {
        ///               "bookingId": "I14kpXunkSuJIU+8++5v3Q==" (If making update),
        ///               "roomId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "userId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "startingDateTime": "2020-09-16T10:33:42.366Z",
        ///               "endingDateTime": "2020-09-16T10:33:42.366Z"
        ///         }
        /// </remarks>
        /// <returns>Booking</returns>
        /// <response code = "200">Returns the created/updated booking.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("create/update")]
        public IActionResult CreateUpdateBooking([FromBody] BookingDTO informations)
        {
            if (informations.StartingDateTime < informations.EndingDateTime)
            {
                BookingListingDTO response;

                if (informations.BookingId == "string" || string.IsNullOrEmpty(informations.BookingId))
                {
                    response = service.CreateBooking(informations);
                }
                else
                {
                    response = service.UpdateBooking(informations);
                }
                if (response != null)
                {
                    return new ObjectResult(response);
                }

                else return new NotFoundObjectResult("You have missed some of the informations or that time there is a scheduled meeting.");
            }
            else return new BadRequestObjectResult("The starting of the appointment must be before its ending.");
        }
        /// <summary>
        /// Find a booking by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET booking/single
        ///         {
        ///               "bookingId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Booking</returns>
        /// <response code = "200">Returns the searched booking.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("single")]
        public IActionResult FindBookingByID(string ID)
        {
            BookingListingDTO response = service.FindBookingById(ID);

            if (response != null)
                return new ObjectResult(response);

            else return new NotFoundObjectResult("A booking with the inserted ID is not existing.");
        }
        /// <summary>
        /// Delete a booking.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET booking/delete
        ///         {
        ///               "bookingId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Booking</returns>
        /// <response code = "200">Returns the deleted booking.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("delete")]
        public IActionResult DeleteByID(string ID)
        {
            BookingListingDTO response = service.DeleteBookingById(ID);

            if (response != null)
                return new ObjectResult(response);

            else return new NotFoundObjectResult("A booking with the inserted ID is not existing.");
        }
        /// <summary>
        /// List bookings by filter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST booking/list
        ///         {
        ///               "roomId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "userId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "locationId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "pageNumber": 1,
        ///               "pageSize": 15
        ///         }
        /// </remarks>
        /// <returns>Booking</returns>  
        /// <response code = "200">Returns the filtered bookings.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("list")]
        public IActionResult ListByFilter(BookingPaginationFilter informations)
        {
            List<BookingListingDTO> response = service.ListAllBookings(informations);

            if (response != null)
                return new ObjectResult(response);

            else return new NotFoundObjectResult("There aren't any bookings with that filter, or there aren't any at all.");
        }
        /// <summary>
        /// List all bookings by a period in a room.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST booking/list/period
        ///         {
        ///               "date": "2020-09-16T10:37:58.738Z",
        ///               "roomId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "type": "Weekly",
        ///         }
        /// </remarks>
        /// <returns>Booking</returns>
        /// <response code = "200">Returns the searched bookings.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("list/period")]
        public IActionResult ListByMonth(ListingDTO informations)
        {
            if (informations.Type == "Daily" || informations.Type == "Weekly" || informations.Type == "Monthly")
            {
                List<BookingListingDTO> response = new List<BookingListingDTO>();
                if (informations.Type == "Daily")
                {
                    response = service.ListBookingsByDay(informations.Date, informations.RoomId);
                }
                if (informations.Type == "Weekly")
                {
                    response = service.ListBookingsByWeek(informations.Date, informations.RoomId);
                }
                if (informations.Type == "Monthly")
                {
                    response = service.ListBookingsByMonth(informations.Date, informations.RoomId);
                }
                if (response != null)
                    return new ObjectResult(response);

                else return new NotFoundObjectResult("There aren't any bookings through the inserted period or in this room.");
            }
            else return new BadRequestObjectResult("The type need to be equal to 'Daily', 'Weekly' or 'Monthly'");

        }
        /// <summary>
        /// Find a free room in specific time.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET booking/find/room
        ///         {
        ///               "startingDateTime": "2020-09-16T10:37:58.738Z",
        ///               "endingDateTime": "2020-09-16T12:37:58.738Z",
        ///         }
        /// </remarks>
        /// <returns>Booking</returns>
        /// <response code = "200">Returns the free rooms in the inserted time span.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("find/room")]
        public IActionResult FindRoom(DateTime startingDateTime, DateTime endingDateTime, string locationId)
        {
            if (startingDateTime < endingDateTime)
            {
                List<RoomDTO> response = service.FindFreeRoom(startingDateTime, endingDateTime, locationId);

                if (response != null)
                    return new ObjectResult(response);

                else return new NotFoundObjectResult("There aren't any free rooms in that timing, look for another timing.");
            }
            else return new BadRequestObjectResult("You can't enter earlier ending then starting.");
        }
        /// <summary>
        /// Create a new Reminder
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST booking/reminder/create/byBooking
        ///         {
        ///               "bookingId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "typeId": "NkfIeIy8+DHe9khIuWpyBg==",
        ///               "value": 1
        ///         }
        /// </remarks>
        /// <param name="create"></param>
        /// <returns>Reminder</returns>
        /// <response code = "200">Returns the created reminder</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("reminder/create/byBooking")]
        public IActionResult CreateReminder([FromBody] ReminderCreateByBookingDTO create)
        {
            if (string.IsNullOrEmpty(create.BookingId))
            {
                return BadRequest("Missing items");
            }
            ReminderByBookingDTO newReminder = service.CreateByBooking(create);
            return new ObjectResult(newReminder);
        }
        /// <summary>
        /// Create a new Reminder
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST booking/reminder/create/byRecurrence
        ///         {
        ///               "bookingRecurrenceId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "typeId": "NkfIeIy8+DHe9khIuWpyBg==",
        ///               "value": 1
        ///         }
        /// </remarks>
        /// <param name="create"></param>
        /// <returns>Reminder</returns>
        /// <response code = "200">Returns the created reminder</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("reminder/create/byRecurrence")]
        public IActionResult CreateRr([FromBody] ReminderCreateByRecurrenceDTO create)
        {
            if (string.IsNullOrEmpty(create.BookingRecurrenceId))
            {
                return BadRequest("Missing items");
            }
            ReminderByRecurrenceDTO reminder = service.CreteByRecurrence(create);
            return new ObjectResult(reminder);

        }
        /// <summary>
        /// Find a reminder by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET booking/reminder/single/byRecurrence
        ///         {
        ///               "reminderId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Reminder</returns>
        /// <response code = "200">Returns the searched reminder.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("reminder/single/byRecurrence")]
        public IActionResult FindReminderR(string ID)
        {
            ReminderByRecurrenceDTO response = service.SelectR(ID);

            if (response != null)
                return new ObjectResult(response);

            else return new NotFoundObjectResult("A reminder with the inserted ID is not existing.");
        }
        /// <summary>
        /// Find a reminder by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET booking/reminder/single/byBooking
        ///         {
        ///               "reminderId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Reminder</returns>
        /// <response code = "200">Returns the searched reminder.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("reminder/single/byBooking")]
        public IActionResult FindReminderB(string ID)
        {
            ReminderByBookingDTO response = service.SelectB(ID);

            if (response != null)
                return new ObjectResult(response);

            else return new NotFoundObjectResult("A reminder with the inserted ID is not existing.");
        }
        /// <summary>
        /// Remove a reminder by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET booking/reminder/remove
        ///         {
        ///               "reminderId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Reminder</returns>
        /// <response code = "200">Returns if the reminder is deleted.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("reminder/remove")]
        public IActionResult RemoveCity([FromQuery] string id)
        {
            Reminder result = service.Delete(id);
            return new ObjectResult(result);
        }
        /// <summary>
        /// List reminders by filter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST booking/reminder/list/byBooking
        ///         {
        ///               "BookingId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "BookingRecurrenceId": "string", (Not needed)
        ///               "TypeId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "IsDone": true
        ///         }
        /// </remarks>
        /// <returns>List of reminders</returns>
        /// <response code = "200">Returns a list of the filtered reminders.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("reminder/list/byBooking")]
        public IActionResult ListByBooking([FromBody] ReminderPaginationFilter info)
        {
            List<ReminderByBookingDTO> response = service.ListByBooking(info);
            if (response != null)
            {
                return new ObjectResult(response);
            }
            return new NotFoundObjectResult("There is not any reminder with that filter or there is not any.");
        }
        /// <summary>
        /// List reminders by filter.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST booking/reminder/list/byRecurrence
        ///         {
        ///               "BookingId": "string", (Not needed)
        ///               "BookingRecurrenceId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "TypeId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "IsDone": true
        ///         }
        /// </remarks>
        /// <returns>List of reminders</returns>
        /// <response code = "200">Returns a list of the filtered reminders.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("reminder/list/byRecurrence")]
        public IActionResult ListByRecurrence([FromBody] ReminderPaginationFilter info)
        {
            List<ReminderByRecurrenceDTO> response = service.ListByRecurrence(info);
            if (response != null)
            {
                return new ObjectResult(response);
            }
            return new NotFoundObjectResult("There is not any reminder with that filter or there is not any.");
        }
        [HttpGet("testest")]
        public IActionResult Test([FromQuery] string TypeId, int Increment)
        {
            using (var db = new AppointmentContext())
            {
                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter("@Type", Convert.ToInt32(DataEncryption.Decrypt(TypeId)));

                parameters[1] = new SqlParameter("@Value", Increment);

                DataSet dataSet = CommonFunction.ExecSp("dbo.FindDateByReminder", parameters);

                DateTime Date = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["Date"]);

                SqlParameter[] parameters1 = new SqlParameter[1];

                parameters1[0] = new SqlParameter("@Date", Date);

                DataSet dataSet1 = CommonFunction.ExecSp("dbo.DateWithoutSeconds", parameters1);

                Date = Convert.ToDateTime(dataSet1.Tables[0].Rows[0]["Date"]);
                
                return new ObjectResult(Date);
            }
        }
    }
}