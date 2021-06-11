using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services
{
    public interface ILocationService
    {
        public PagedResponse<List<LocationDTO>> GetAllLocations(PaginationFilter filters);

        public LocationDTO GetLocationById(string id);

        public LocationDTO Addlocation(LocationCreateDTO location);
        public bool RemoveLocation(string id);
        bool UpdateLocation(LocationDTO request);
        public List<LocationDTO> LocationByCity(string cityId);
    }
}
