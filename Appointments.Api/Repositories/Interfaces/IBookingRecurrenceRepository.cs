using Appointments.Api.Models;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsDTO;

namespace Appointments.Api.Repositories
{
    public interface IBookingRecurrenceRepository
    {
        public BookingRecurrence CreateBookingRecurrenceByNoDate(BookingRecurrenceCreateDTO info);
        public BookingRecurrence CreateBookingRecurrenceByDays(BookingRecurrenceCreateDTO info);
        public BookingRecurrence CreateBookingRecurrenceByDate(BookingRecurrenceCreateDTO info);
        public BookingRecurrence GetBookingRecurrence (int Id);
        public BookingRecurrence DeleteBookingRecurrence (int Id);
        public List<BookingRecurrence> ListBookingRecurrences(RecurrencePaginationFilter info);
        public bool CheckAvailability(int RoomId, DateTime startingDate, DateTime endingDate);
    }
}
