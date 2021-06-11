using Appointments.Api.Models;
using AppointmentsDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Interface
{
    public interface IBookingService
    {
        public BookingListingDTO CreateBooking(BookingDTO informations);
        public BookingListingDTO UpdateBooking(BookingDTO informations);
        public BookingListingDTO FindBookingById(string ID);
        public BookingListingDTO DeleteBookingById(string ID);
        public List<BookingListingDTO> ListAllBookings(BookingPaginationFilter informations);
        public List<RoomDTO> FindFreeRoom(DateTime startingDateTime, DateTime endingDateTime, string locationId);
        public List<BookingListingDTO> ListBookingsByDay(DateTime date, string RoomId);
        public List<BookingListingDTO> ListBookingsByWeek(DateTime date, string RoomId);
        public List<BookingListingDTO> ListBookingsByMonth(DateTime date, string RoomId);
        //For Reminder
        public ReminderByBookingDTO CreateByBooking(ReminderCreateByBookingDTO  informations);
        public ReminderByRecurrenceDTO CreteByRecurrence(ReminderCreateByRecurrenceDTO  informations);
        public ReminderByRecurrenceDTO SelectR(string id);
        public ReminderByBookingDTO SelectB(string id);
        public Reminder Delete(string id);
        public List<ReminderByBookingDTO> ListByBooking(ReminderPaginationFilter info);
        public List<ReminderByRecurrenceDTO> ListByRecurrence(ReminderPaginationFilter info);
    }
}
