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
    public class CityMapper : Profile
    {
        public CityMapper()
        {
            CreateMap<City, CityDTO>()
                .ForMember(o => o.CityId, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.CityId.ToString())));
            CreateMap<CityCreateDTO, City>();
        }
    }
}
