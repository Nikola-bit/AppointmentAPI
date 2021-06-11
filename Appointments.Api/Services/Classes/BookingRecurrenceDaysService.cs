using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Services.Interface;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Classes
{
    public class BookingRecurrenceDaysService : IBookingRecurrenceDaysService
    {
            private readonly IBookingRecurrenceDaysRepository repository;
            private readonly IMapper mapper;

            public BookingRecurrenceDaysService(IBookingRecurrenceDaysRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }
        public PagedResponse<List<BookingRecurrenceDaysDTO>> GetAll(PaginationFilter filter)
        {
            PagedResponse<List<BookingRecurrenceDaysDTO>> days = mapper.Map<PagedResponse<List<BookingRecurrenceDaysDTO>>>(repository.AllDays(filter));
            return days;
        }
        public BookingRecurrenceDaysDTO GetById(string id)
        {
            BookingRecurrenceDaysDTO result = mapper.Map<BookingRecurrenceDaysDTO>(repository.ById(id));
            return result;
        }
        public BookingRecurrenceDaysDTO AddNew(BookingRecurrenceDaysCreateDTO booking)
        {
            BookingRecurrenceDays bookingD = mapper.Map<BookingRecurrenceDays>(booking);
            BookingRecurrenceDaysDTO result = mapper.Map<BookingRecurrenceDaysDTO>(repository.Add(bookingD));
            return result;
        }
        public bool Remove(string id)
        {
            bool results = repository.DayD(id);
            return results;
        }
        public bool Update(BookingRecurrenceDaysDTO id)
        {
            bool update = repository.Update(id);
            return update;
        }
    }
}
