using Appointments.Api.Models;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Interfaces
{
  public  interface ICityRepository
    {
        public PagedResponse<List<CityDTO>> AllCity(PaginationFilter filters);

        public City CityById(string id);

        public City AddCity(City city);
        public bool DeleteCity(string id);
        bool UpdateCity(CityDTO request);
    }
}
