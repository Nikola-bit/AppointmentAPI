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
    public class LocationMapper : Profile
    {
        public LocationMapper()
        {
            CreateMap<Location, LocationDTO>()
                .ForMember(o => o.LocationId, opt => opt.MapFrom(c => EncryptionHelper.Encrypt(c.LocationId.ToString())))
                .ForMember(o => o.CityId, opt => opt.MapFrom(x => EncryptionHelper.Encrypt(x.CityId.ToString())));
            CreateMap<LocationCreateDTO, Location>()
                .ForMember(e => e.CityId, opt => opt.MapFrom(x => EncryptionHelper.Decrypt(x.CityId.ToString())))
                .ForMember(s => s.DateCreated, opt => opt.MapFrom(d => DateTime.Now));
        }

    }
}
