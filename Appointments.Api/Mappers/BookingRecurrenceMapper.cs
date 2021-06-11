using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Models;
using Appointments.Api.Utilities;
using AppointmentsDTO;
using AutoMapper;

namespace Appointments.Api.Mappers
{
    public class BookingRecurrenceMapper : Profile
    {
        public BookingRecurrenceMapper()
        {
            CreateMap<BookingRecurrenceCreateDTO, BookingRecurrence>()
                .ForMember(b => b.UserId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.UserId))))
                .ForMember(b => b.RoomId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.RoomId))))
                .ForMember(b => b.Type, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.Type))))
                .ForMember(b => b.StartTime, opt => opt.MapFrom(r => TimeSpan.Parse(r.StartTime)))
                .ForMember(b => b.EndTime, opt => opt.MapFrom(r => TimeSpan.Parse(r.EndTime)));
            CreateMap<BookingRecurrenceUpdateDTO, BookingRecurrence>()
                .ForMember(b => b.UserId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.UserId))))
                .ForMember(b => b.RoomId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.RoomId))))
                .ForMember(b => b.Type, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.Type))))
                .ForMember(b => b.RecurrenceId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.RecurrenceId))))
                .ForMember(b => b.StartTime, opt => opt.MapFrom(r => TimeSpan.Parse(r.StartTime)))
                .ForMember(b => b.EndTime, opt => opt.MapFrom(r => TimeSpan.Parse(r.EndTime)));
            CreateMap<BookingRecurrence, BookingRecurrenceDTO>()
                .ForMember(b => b.RecurrenceId, opt => opt.MapFrom(r => DataEncryption.Encrypt(Convert.ToString(r.RecurrenceId))))
                .ForMember(b => b.Type, opt => opt.MapFrom(r => DataEncryption.Encrypt(Convert.ToString(r.Type))))
                .ForMember(b => b.UserId, opt => opt.MapFrom(r => DataEncryption.Encrypt(Convert.ToString(r.UserId))))
                .ForMember(b => b.RoomId, opt => opt.MapFrom(r => DataEncryption.Encrypt(Convert.ToString(r.RoomId))))
                .ForMember(b => b.StartDate, opt => opt.MapFrom(r => r.StartDate))
                .ForMember(b => b.StartTime, opt => opt.MapFrom(r => r.StartTime.ToString(@"d\.h\:mm\:ss")))
                .ForMember(b => b.EndTime, opt => opt.MapFrom(r => r.EndTime.ToString(@"d\.h\:mm\:ss")));
        }
    }
}
