using Appointments.Api.Models;
using Appointments.Api.Repositories;
using Appointments.Api.Utilities;
using AppointmentsDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Classes
{
    public class BookingRecurrenceService : IBookingRecurrenceService
    {
        public IMapper mapper;
        public IBookingRecurrenceRepository repository;
        public BookingRecurrenceService(IMapper _mapper, IBookingRecurrenceRepository _repository)
        {
            mapper = _mapper;
            repository = _repository;
        }
        public BookingRecurrenceDTO CreateBookingRecurrenceByNoDate(BookingRecurrenceCreateDTO info)
        {

            BookingRecurrence result = repository.CreateBookingRecurrenceByNoDate(info);

            BookingRecurrenceDTO response = mapper.Map<BookingRecurrenceDTO>(result);

            return response;
        }
        public BookingRecurrenceDTO CreateBookingRecurrenceByDays(BookingRecurrenceCreateDTO info)
        {
            BookingRecurrence result = repository.CreateBookingRecurrenceByDays(info);

           BookingRecurrenceDTO response = mapper.Map<BookingRecurrenceDTO>(result);

            return response;
        }
        public BookingRecurrenceDTO CreateBookingRecurrenceByDate(BookingRecurrenceCreateDTO info)
        {
            BookingRecurrence result = repository.CreateBookingRecurrenceByDate(info);

            BookingRecurrenceDTO response = mapper.Map<BookingRecurrenceDTO>(result);

            return response;
        }
        public BookingRecurrenceDTO DeleteBookingRecurrence(string Id)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(Id));

            BookingRecurrence result = repository.DeleteBookingRecurrence(request);

            BookingRecurrenceDTO response = mapper.Map<BookingRecurrenceDTO>(result);

            return response;
        }

        public BookingRecurrenceDTO GetBookingRecurrence(string Id)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(Id));

            BookingRecurrence result = repository.GetBookingRecurrence(request);

            BookingRecurrenceDTO response = mapper.Map<BookingRecurrenceDTO>(result);

            return response;
        }

        public List<BookingRecurrenceDTO> ListBookingRecurrences(RecurrencePaginationFilter info)
        {
            List<BookingRecurrence> result = repository.ListBookingRecurrences(info);

            List<BookingRecurrenceDTO> response = mapper.Map<List<BookingRecurrenceDTO>>(result);

            return response;
        }
        //public BookingRecurrenceDTO CreateBookingRecurrenceByNoDate(BookingRecurrenceCreateDTO info)
        //{

        //    BookingRecurrence result = repository.CreateBookingRecurrenceByNoDate(info);

        //    BookingRecurrenceDTO response = mapper.Map<BookingRecurrenceDTO>(result);

        //    return response;
        //}
        //public BookingRecurrenceDTO CreateBookingRecurrenceByDays(BookingRecurrenceCreateDTO info)
        //{
        //    BookingRecurrence result = repository.CreateBookingRecurrenceByDays(info);

        //    BookingRecurrenceDTO response = mapper.Map<BookingRecurrenceDTO>(result);

        //    return response;
        //}
    }
}
