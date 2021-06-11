using AppointmentsDTO.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Models;
using Appointments.Api.Utilities;

namespace Appointments.Api.Mappers
{
    public class ReminderMapper : Profile
    {
        public ReminderMapper()
        {
            CreateMap<ReminderCreateByBookingDTO, Reminder>()
                .ForMember(r => r.BookingId, opt => opt.MapFrom(e => Convert.ToInt32(DataEncryption.Decrypt(e.BookingId))))
                .ForMember(r => r.TypeId, opt => opt.MapFrom(e => Convert.ToInt32(DataEncryption.Decrypt(e.TypeId))))
                .ForMember(r => r.DateCreated, opt => opt.MapFrom(e => DateTime.Now))
                .ForMember(r => r.IsDone, opt => opt.MapFrom(e => false));
            CreateMap<ReminderCreateByRecurrenceDTO, Reminder>()
                .ForMember(r => r.BookingRecurrenceId, opt => opt.MapFrom(e => Convert.ToInt32(DataEncryption.Decrypt(e.BookingRecurrenceId))))
                .ForMember(r => r.TypeId, opt => opt.MapFrom(e => Convert.ToInt32(DataEncryption.Decrypt(e.TypeId))))
                .ForMember(r => r.DateCreated, opt => opt.MapFrom(e => DateTime.Now))
                .ForMember(r => r.IsDone, opt => opt.MapFrom(e => false));
            CreateMap<Reminder, ReminderByBookingDTO>()
                .ForMember(r => r.ReminderId, opt => opt.MapFrom(e => DataEncryption.Encrypt(Convert.ToString(e.ReminderId))))
                .ForMember(r => r.BookingId, opt => opt.MapFrom(e => DataEncryption.Encrypt(Convert.ToString(e.BookingId))))
                .ForMember(r => r.TypeId, opt => opt.MapFrom(e => DataEncryption.Encrypt(Convert.ToString(e.TypeId))));
            CreateMap<Reminder, ReminderByRecurrenceDTO>()
                .ForMember(r => r.ReminderId, opt => opt.MapFrom(e => DataEncryption.Encrypt(Convert.ToString(e.ReminderId))))
                .ForMember(r => r.BookingRecurrenceId, opt => opt.MapFrom(e => DataEncryption.Encrypt(Convert.ToString(e.BookingRecurrenceId))))
                .ForMember(r => r.TypeId, opt => opt.MapFrom(e => DataEncryption.Encrypt(Convert.ToString(e.TypeId))));
        }
    }
}
