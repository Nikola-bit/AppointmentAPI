using Appointments.Api.Models;
using Appointments.Api.Security;
using Appointments.Api.Utilities;
using AppointmentsDTO.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Mappers
{
    public class BookingRecurrenceDaysMapper : Profile
    {
        public BookingRecurrenceDaysMapper()
        {
            CreateMap<BookingRecurrenceDays, BookingRecurrenceDaysDTO>()
                .ForMember(x => x.RecurrenceId, opt => opt.MapFrom(t => EncryptionHelper.Encrypt(t.RecurrenceId.ToString())))
                .ForMember(c => c.Weekday, opt => opt.MapFrom(w => EncryptionHelper.Encrypt(w.Weekday.ToString())))
                .ForMember(o => o.Id, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.Id.ToString())));
            CreateMap<BookingRecurrenceDaysCreateDTO, BookingRecurrenceDays>()
                .ForMember(e => e.Weekday, opt => opt.MapFrom(x => Convert.ToInt32(DataEncryption.Decrypt(x.Weekday))))
                .ForMember(r => r.RecurrenceId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.RecurrenceId))));
        }
    }
}
