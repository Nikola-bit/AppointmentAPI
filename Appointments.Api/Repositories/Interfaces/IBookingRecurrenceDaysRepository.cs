using Appointments.Api.Models;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Interfaces
{
   public interface IBookingRecurrenceDaysRepository
    {
        public PagedResponse<List<BookingRecurrenceDaysDTO>> AllDays(PaginationFilter filters);

        public BookingRecurrenceDays ById(string id);

        public BookingRecurrenceDays Add(BookingRecurrenceDays day);
        public bool DayD(string id);
        bool Update(BookingRecurrenceDaysDTO request);
    }
}
