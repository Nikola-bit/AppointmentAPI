using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Security;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Classes
{
    public class CityRepository : ICityRepository
    {
        private readonly IMapper mapper;
        public CityRepository(IMapper _mapper)
        {

            mapper = _mapper;
        }
        public PagedResponse<List<CityDTO>> AllCity(PaginationFilter filter)
        {
            using (var db = new AppointmentContext())
            {
                List<City> list = db.City
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToList();
                var cities = mapper.Map<List<CityDTO>>(list);
                int totalCount = db.City.Count();

                PagedResponse<List<CityDTO>> response = new PagedResponse<List<CityDTO>>()
                {
                    Data = cities,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalRecords = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
                };
                return response;
            }
        }
        public City CityById(string id)
        {
            using (var db = new AppointmentContext())
            {
                int ciId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                City city = db.City.Where(s => s.CityId == ciId).FirstOrDefault();

                if (city != null)
                {
                    City result = mapper.Map<City>(city);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public City AddCity(City newCity)
        {
            using (var db = new AppointmentContext())
            {
                db.City.Add(newCity);
                db.SaveChanges();
                return newCity;
            }
        }
        public bool DeleteCity(string id)
        {
            using (var db = new AppointmentContext())
            {
                int citId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                City city = db.City.Where(s => s.CityId == citId).FirstOrDefault();
                if (city != null)
                {
                    db.City.Remove(city);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UpdateCity(CityDTO request)
        {
            using (var db = new AppointmentContext())
            {
                int citId = Convert.ToInt32(EncryptionHelper.Decrypt(request.CityId));

                City updateCity = db.City.Where(s => s.CityId == citId).FirstOrDefault();
                if (updateCity != null)
                {
                    updateCity.Name = request.Name;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
