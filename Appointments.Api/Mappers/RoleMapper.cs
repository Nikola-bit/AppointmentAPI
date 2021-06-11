using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Appointments.Api.Models;
using Appointments.Api.Utilities;
using AppointmentsDTO.DTO;
using Appointments.Api.Wrappers;

namespace Appointments.Api.Mappers
{
    public class PatientMapper : Profile
    {
        public PatientMapper()
        {
            CreateMap<RoleCreateDTO, Role>()
                .ForMember(r => r.DateCreated, opt => opt.MapFrom(p => DateTime.Now));
            CreateMap<Role, RoleDTO>()
                .ForMember(r => r.RoleId, opt => opt.MapFrom(p => DataEncryption.Encrypt(Convert.ToString(p.RoleId))));
            CreateMap<RoleDTO, RoleCreateDTO>();
            CreateMap<RoleDTO, Role>()
                .ForMember(r => r.RoleId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.RoleId))));

        }
    }
}