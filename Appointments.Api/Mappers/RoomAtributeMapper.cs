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
    public class RoomAtributeMapper : Profile
    {
        public RoomAtributeMapper()
        {
            CreateMap<RoomAttribute, RoomAtributeDTO>()
                .ForMember(o => o.RoomAttributeId, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.RoomAttributeId.ToString())))
                .ForMember(a => a.AttributeId, opt => opt.MapFrom(e => EncryptionHelper.Encrypt(e.AttributeId.ToString())))
                .ForMember(r => r.RoomId, opt => opt.MapFrom(x => EncryptionHelper.Encrypt(x.RoomId.ToString())));
            CreateMap<RoomAtributeCreateDTO, RoomAttribute>()
                .ForMember(a => a.AttributeId, opt => opt.MapFrom(e => EncryptionHelper.Encrypt(e.AttributeId.ToString())))
                .ForMember(r => r.RoomId, opt => opt.MapFrom(x => EncryptionHelper.Encrypt(x.RoomId.ToString())));
        }
    }
}
