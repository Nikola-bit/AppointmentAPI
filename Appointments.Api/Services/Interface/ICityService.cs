using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Interface
{
    public interface ICityService
    {
        public PagedResponse<List<CityDTO>> GetAllCity(PaginationFilter filters);

        public CityDTO GetCityById(string id);

        public CityDTO AddCity(CityCreateDTO city);
        public bool RemoveCity(string id);
        bool UpdateCity(CityDTO request);
    }
}
