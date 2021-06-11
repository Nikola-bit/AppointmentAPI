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
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<BookingCreateDTO, Booking>()
                .ForMember(b => b.RoomId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.RoomId))))
                .ForMember(b => b.UserId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.UserId))));
            CreateMap<BookingDTO, Booking>()
                .ForMember(b => b.BookingId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.BookingId))))
                .ForMember(b => b.RoomId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.RoomId))))
                .ForMember(b => b.UserId, opt => opt.MapFrom(r => Convert.ToInt32(DataEncryption.Decrypt(r.UserId))));
            CreateMap<Booking, BookingListingDTO>()
                .ForMember(b => b.BookingId, opt => opt.MapFrom(r => DataEncryption.Encrypt(Convert.ToString(r.BookingId))))
                .ForMember(b => b.RoomId, opt => opt.MapFrom(r => DataEncryption.Encrypt(Convert.ToString(r.RoomId))))
                .ForMember(b => b.UserId, opt => opt.MapFrom(r => DataEncryption.Encrypt(Convert.ToString(r.UserId))))
                .ForMember(b => b.UserName, opt => opt.MapFrom(r => r.User.FirstName + " " + r.User.LastName))
                .ForMember(b => b.UserEmail, opt => opt.MapFrom(r => r.User.Email))
                .ForMember(b => b.LocationName, opt => opt.MapFrom(r => r.Room.Location.Name))
                .ForMember(b => b.LocationAddress, opt => opt.MapFrom(r => r.Room.Location.Address + ", " + r.Room.Location.City.Name))
                .ForMember(b => b.Capacity, opt => opt.MapFrom(r => r.Room.Capacity));
            CreateMap<BookingDTO, BookingCreateDTO>();
        }
    }
}
