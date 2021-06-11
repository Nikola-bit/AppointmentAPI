using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Services.Interface;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Classes
{
    public class CityService : ICityService
    {
        private readonly ICityRepository repository;
        private readonly IMapper mapper;

        public CityService(ICityRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public PagedResponse<List<CityDTO>> GetAllCity(PaginationFilter filter)
        {

            PagedResponse<List<CityDTO>> cities = mapper.Map<PagedResponse<List<CityDTO>>>(repository.AllCity(filter));
            return cities;
        }
        public CityDTO GetCityById(string id)
        {
            CityDTO result = mapper.Map<CityDTO>(repository.CityById(id));
            return result;
        }
        public CityDTO AddCity(CityCreateDTO city)
        {
            City location = mapper.Map<City>(city);
            CityDTO result = mapper.Map<CityDTO>(repository.AddCity(location));
            return result;
        }
        public bool RemoveCity(string id)
        {
            bool results = repository.DeleteCity(id);
            return results;
        }
        public bool UpdateCity(CityDTO id)
        {
            bool update = repository.UpdateCity(id);
            return update;
        }
    }
}
