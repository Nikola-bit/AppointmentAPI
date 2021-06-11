using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Interface
{
   public interface IBookingRecurrenceDaysService
    {
        public PagedResponse<List<BookingRecurrenceDaysDTO>> GetAll(PaginationFilter filters);

        public BookingRecurrenceDaysDTO GetById(string id);

        public BookingRecurrenceDaysDTO AddNew(BookingRecurrenceDaysCreateDTO days);
        public bool Remove(string id);
        bool Update(BookingRecurrenceDaysDTO request);
    }
}

