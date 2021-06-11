using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Services.Interface;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [Route("city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService service;

        public CityController(ICityService _service)
        {
            service = _service;

        }
        /// <summary>
        /// Get All Cities
        /// </summary>
        /// <returns>All Cities</returns>
        /// <response code = "200">Return all Cities</response>
        /// <response code = "400">Some error occured</response>
        /// 
        [HttpPost("list")]
        public IActionResult GetAllCities([FromBody] PaginationFilter filter)
        {
            PagedResponse<List<CityDTO>> locationlist = service.GetAllCity(filter);
            return new ObjectResult(locationlist);
        }
        /// <summary>
        /// Get City By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/City
        ///     {
        ///         "cityId" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single City</returns>
        /// <response code = "200">Return City by Id</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("single")]
        public IActionResult GetCityById([FromQuery] string id)
        {
            CityDTO city = service.GetCityById(id);
            if (city != null)
            {
                return new ObjectResult(city);
            }
            else
            {
                return Content("Location with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new City
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create/Update/City
        ///         {
        ///               "CityId" : "OqbI1toilNgCe9GJnppPug==",
        ///               "Name": "Skopje"
        ///         }
        /// </remarks>
        /// <param name="cityCreate"></param>
        /// <returns>Updated list of Cities</returns>
        /// <response code = "200">Return updated list of cities</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("create/update")]
        public IActionResult CreateCity([FromBody] CityDTO cityCreate)
        {
            if (cityCreate.CityId != "string" && !string.IsNullOrEmpty(cityCreate.CityId))
            {
                bool result = service.UpdateCity(cityCreate);
                return new ObjectResult(result);
            }
            else
            {
                cityCreate.Name = cityCreate.Name == "string" ? "" : cityCreate.Name;
                if (string.IsNullOrEmpty(cityCreate.Name))
                {
                    return BadRequest("Missing items");
                }
                CityDTO newCity = service.AddCity(cityCreate);
                return new ObjectResult(newCity);
            }
        }
        /// <summary>
        /// Remove City by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/City
        ///     {
        ///         "CityId" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single City</returns>
        /// <response code = "200">Return single City</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("remove")]
        public IActionResult RemoveCity([FromQuery] string id)
        {
            bool result = service.RemoveCity(id);
            return new ObjectResult(result);
        }
    }
}
