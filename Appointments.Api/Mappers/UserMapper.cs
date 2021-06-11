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
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserCreateDTO, User>()
                .ForMember(r => r.RoleId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.RoleId))))
                .ForMember(r => r.Email, opt => opt.MapFrom(p => DataEncryption.Encrypt(p.Email)))
                .ForMember(r => r.Password, opt => opt.MapFrom(p => DataEncryption.Encrypt(p.Password)));
            CreateMap<UserDTO, User>()
                .ForMember(r => r.UserId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.UserId))))
                .ForMember(r => r.RoleId, opt => opt.MapFrom(p => Convert.ToInt32(DataEncryption.Decrypt(p.RoleId))))
                .ForMember(r => r.Email, opt => opt.MapFrom(p => DataEncryption.Encrypt(p.Email)))
                .ForMember(r => r.Password, opt => opt.MapFrom(p => DataEncryption.Encrypt(p.Password)));
            CreateMap<UserDTO, UserCreateDTO>();
            CreateMap<User, UserRoleDTO>()
                .ForMember(r => r.UserId, opt => opt.MapFrom(p => DataEncryption.Encrypt(Convert.ToString(p.UserId))))
                .ForMember(r => r.RoleId, opt => opt.MapFrom(p => DataEncryption.Encrypt(Convert.ToString(p.RoleId))))
                .ForMember(r => r.Role, opt => opt.MapFrom(p => p.Role.Name));
        }
    }
}
