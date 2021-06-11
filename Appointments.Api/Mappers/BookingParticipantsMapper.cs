using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Models;
using Appointments.Api.Utilities;
using AppointmentsDTO.DTO;
using AutoMapper;

namespace Appointments.Api.Mappers
{
    public class BookingParticipantsMapper : Profile
    {
        public BookingParticipantsMapper()
        {
            CreateMap<BookingParticipantsCreateDTO, BookingParticipants>()
                .ForMember(b => b.BookingId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.BookingId))))
                .ForMember(b => b.Email, opt => opt.MapFrom(p => DataEncryption.Encrypt(p.Email)));
            CreateMap<BookingParticipantsDTO, BookingParticipants>()
                .ForMember(b => b.ParticipantId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.ParticipantId))))
                .ForMember(b => b.BookingId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.BookingId))))
                .ForMember(b => b.InvitationStatus, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.InvitationStatusId))))
                .ForMember(b => b.Email, opt => opt.MapFrom(p => DataEncryption.Encrypt(p.Email)));
            CreateMap<BookingParticipantsDTO, BookingParticipantsCreateDTO>();
            CreateMap<BookingParticipants, BookingParticipantsShowDTO>()
                .ForMember(b => b.Name, opt => opt.MapFrom(p => p.FirstName + " " + p.LastName))
                .ForMember(b => b.ParticipantId, opt => opt.MapFrom(p => DataEncryption.Encrypt(Convert.ToString(p.ParticipantId))))
                .ForMember(b => b.BookingId, opt => opt.MapFrom(p => DataEncryption.Encrypt(Convert.ToString(p.BookingId))))
                .ForMember(b => b.InvitationStatusId, opt => opt.MapFrom(p => DataEncryption.Encrypt(Convert.ToString(p.InvitationStatus))))
                .ForMember(b => b.InvitationStatus, opt => opt.MapFrom(p => p.InvitationStatusNavigation.Name));

    }
}
}
