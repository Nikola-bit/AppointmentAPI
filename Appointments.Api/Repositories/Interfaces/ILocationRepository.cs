using Appointments.Api.Models;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Data.Interfaces
{
   public interface ILocationRepository
    {
        public PagedResponse <List<LocationDTO>> ListAllLocations(PaginationFilter filters);

        public Location LocationById(string id);

        public Location CreateLocation(Location location);
        public bool DeleteLocation(string id);
        bool UpdateLocation(LocationDTO request);
        public List<Location> LocationByCity(int cityId);
    }
}
