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
    public class PlatformConfigurationMapper : Profile
    {
        public PlatformConfigurationMapper()
        {
            CreateMap<PlatformConfiguration, PlatformConfigurationDTO>()
                .ForMember(o => o.Value, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(Convert.ToString(c.Value))));
        }
    }
}
