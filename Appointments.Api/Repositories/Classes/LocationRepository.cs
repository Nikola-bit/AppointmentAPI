using Appointments.Api.Data.Interfaces;
using Appointments.Api.Mappers;
using Appointments.Api.Models;
using Appointments.Api.Security;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IMapper mapper;
        public LocationRepository(IMapper _mapper)
        {

            mapper = _mapper;
        }
        public PagedResponse<List<LocationDTO>> ListAllLocations(PaginationFilter filter)
        {
            using (var db = new AppointmentContext())
            {
                List<Location> list = db.Location
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToList();
                var locations = mapper.Map<List<LocationDTO>>(list);
                int totalCount = db.Location.Count();

                PagedResponse<List<LocationDTO>> response = new PagedResponse<List<LocationDTO>>()
                {
                    Data = locations,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalRecords = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
                };
                return response;
            }

        }
        public Location LocationById(string id)
        {
            using (var db = new AppointmentContext())
            {
                int stId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                Location location = db.Location.Where(s => s.LocationId == stId).FirstOrDefault();

                if (location != null)
                {
                    Location result = mapper.Map<Location>(location);

                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public Location CreateLocation(Location location)
        {
            using (var db = new AppointmentContext())
            {
                db.Location.Add(location);
                db.SaveChanges();
                // Location result = mapper.Map<Location>(location);
                return location;
            }
        }
        public bool DeleteLocation(string id)
        {
            using (var db = new AppointmentContext())
            {
                int locId = Convert.ToInt32(EncryptionHelper.Decrypt(id));

                Location location = db.Location.Where(s => s.LocationId == locId).FirstOrDefault();

                if (location != null)
                {
                    db.Location.Remove(location);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UpdateLocation(LocationDTO request)
        {
            using (var db = new AppointmentContext())
            {
                int locId = Convert.ToInt32(EncryptionHelper.Decrypt(request.LocationId));

                Location updateLocation = db.Location.Where(s => s.LocationId == locId).FirstOrDefault();
                if (updateLocation != null)
                {
                    updateLocation.Name = request.Name;
                    updateLocation.Address = request.Address;
                    updateLocation.IsActive = request.IsActive;
                    updateLocation.CityId = Convert.ToInt32(EncryptionHelper.Decrypt(request.CityId));
                    updateLocation.DateCreated = request.DateCreated;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
        public List<Location> LocationByCity(int cityId)
        {
            using (var db = new AppointmentContext())
            {
                List<Location> locations = db.Location.Where(l => l.CityId == cityId).ToList();

                return locations;
            }
        }
    }
}
