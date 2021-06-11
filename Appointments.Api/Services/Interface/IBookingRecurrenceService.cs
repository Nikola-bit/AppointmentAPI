using AppointmentsDTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services
{
    public interface IBookingRecurrenceService
    {
        public BookingRecurrenceDTO CreateBookingRecurrenceByNoDate(BookingRecurrenceCreateDTO info);
        public BookingRecurrenceDTO CreateBookingRecurrenceByDays(BookingRecurrenceCreateDTO info);
        public BookingRecurrenceDTO CreateBookingRecurrenceByDate(BookingRecurrenceCreateDTO info);
        public BookingRecurrenceDTO GetBookingRecurrence (string Id);
        public BookingRecurrenceDTO DeleteBookingRecurrence (string Id);
        public List<BookingRecurrenceDTO> ListBookingRecurrences (RecurrencePaginationFilter info);
    }
}
