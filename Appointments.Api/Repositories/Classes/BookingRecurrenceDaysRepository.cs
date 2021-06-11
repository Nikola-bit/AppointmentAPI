using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Security;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Classes
{
    public class BookingRecurrenceDaysRepository : IBookingRecurrenceDaysRepository
    {
        private readonly IMapper mapper;
        public BookingRecurrenceDaysRepository(IMapper _mapper)
        {

            mapper = _mapper;
        }
        public PagedResponse<List<BookingRecurrenceDaysDTO>> AllDays(PaginationFilter filter)
        {
            using (var db = new AppointmentContext())
            {
                List<BookingRecurrenceDays> list = db.BookingRecurrenceDays
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToList();
                var days = mapper.Map<List<BookingRecurrenceDaysDTO>>(list);
                int totalCount = db.BookingRecurrenceDays.Count();
                PagedResponse<List<BookingRecurrenceDaysDTO>> response = new PagedResponse<List<BookingRecurrenceDaysDTO>>()
                {
                    Data = days,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalRecords = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
                };
                return response;
            }
        }
        public BookingRecurrenceDays ById(string id)
        {
            using (var db = new AppointmentContext())
            {
                int Id = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                BookingRecurrenceDays bdays = db.BookingRecurrenceDays.Where(s => s.Id == Id).FirstOrDefault();
                if (bdays != null)
                {
                    BookingRecurrenceDays result = mapper.Map<BookingRecurrenceDays>(bdays);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public BookingRecurrenceDays Add(BookingRecurrenceDays newBookingDay)
        {
            using (var db = new AppointmentContext())
            {
                db.BookingRecurrenceDays.Add(newBookingDay);
                db.SaveChanges();
                return newBookingDay;
            }
        }
        public bool DayD(string id)
        {
            using (var db = new AppointmentContext())
            {
                int Id = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                BookingRecurrenceDays days = db.BookingRecurrenceDays.Where(s => s.Id == Id).FirstOrDefault();
                if (days != null)
                {
                    db.BookingRecurrenceDays.Remove(days);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool Update(BookingRecurrenceDaysDTO request)
        {
            using (var db = new AppointmentContext())
            {
                int Id = Convert.ToInt32(EncryptionHelper.Decrypt(request.Id));

                BookingRecurrenceDays update = db.BookingRecurrenceDays.Where(s => s.Id == Id).FirstOrDefault();
                if (update != null)
                {
                    update.RecurrenceId = Convert.ToInt32(EncryptionHelper.Decrypt(request.RecurrenceId));
                    update.Weekday = Convert.ToInt32(EncryptionHelper.Decrypt(request.Weekday));
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
