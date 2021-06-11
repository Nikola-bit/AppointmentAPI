using Appointments.Api.Models;
using Appointments.Api.Security;
using AppointmentsDTO.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Mappers
{
    public class RoomMapper : Profile
    {
        public RoomMapper()
        {
            CreateMap<Room, RoomDTO>()
                .ForMember(o => o.RoomId, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.RoomId.ToString())))
                .ForMember(o => o.LocationId, opt => opt.MapFrom(x => EncryptionHelper.Encrypt(x.LocationId.ToString())));
            CreateMap<RoomCreateDTO, Room>()
                .ForMember(e => e.LocationId, opt => opt.MapFrom(x => EncryptionHelper.Decrypt(x.LocationId.ToString())))
                .ForMember(s => s.DateCreated, opt => opt.MapFrom(d => DateTime.Now));
            CreateMap<RoomAttribute, AttributeDTO>()
                .ForMember(e => e.Name, opt => opt.MapFrom(x => x.Attribute.Name));
            CreateMap<Room, RoomAndAttributeDTO>()
                .ForMember(o => o.RoomId, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.RoomId.ToString())))
                .ForMember(o => o.LocationId, opt => opt.MapFrom(x => EncryptionHelper.Encrypt(x.LocationId.ToString())));
            CreateMap<RoomAttribute, Room>()
                .ForMember(r => r.Capacity, opt => opt.MapFrom(a => a.Room.Capacity))
                .ForMember(r => r.DateCreated, opt => opt.MapFrom(a => a.Room.DateCreated))
                .ForMember(r => r.IsUsable, opt => opt.MapFrom(a => a.Room.IsUsable))
                .ForMember(r => r.LocationId, opt => opt.MapFrom(a => a.Room.LocationId))
                .ForMember(r => r.Name, opt => opt.MapFrom(a => a.Room.Name))
                .ForMember(r => r.RoomId, opt => opt.MapFrom(a => a.Room.RoomId));
        }
    }
}
