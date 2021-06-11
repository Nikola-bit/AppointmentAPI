using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Data.Repositories;
using Appointments.Api.Data.Interfaces;
using Appointments.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Appointments.Api.Utilities;

namespace Appointments.Api.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository repository;
        private readonly IMapper mapper;

        public LocationService(ILocationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public PagedResponse<List<LocationDTO>> GetAllLocations(PaginationFilter filter)
        {

            PagedResponse<List<LocationDTO>> locations = mapper.Map<PagedResponse<List<LocationDTO>>>(repository.ListAllLocations(filter));
            return locations;
        }
        public LocationDTO GetLocationById(string id)
        {
            LocationDTO result = mapper.Map<LocationDTO>(repository.LocationById(id));
            return result;
        }
        public LocationDTO Addlocation(LocationCreateDTO locations)
        {
            Location location = mapper.Map<Location>(locations);
            LocationDTO result = mapper.Map<LocationDTO>(repository.CreateLocation(location));
            return result;
        }
        public bool RemoveLocation(string id)
        {
            bool results = repository.DeleteLocation(id);
            return results;
        }
        public bool UpdateLocation(LocationDTO id)
        {
            bool update = repository.UpdateLocation(id);
            return update;
        }
        public List<LocationDTO> LocationByCity(string cityId)
        {
            List<Location> result = repository.LocationByCity(Convert.ToInt32(DataEncryption.Decrypt(cityId)));

            List<LocationDTO> response = mapper.Map<List<LocationDTO>>(result);

            return response;
        }
    }
}
