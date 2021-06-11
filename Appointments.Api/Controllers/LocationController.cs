using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Services;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [Route("location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService service;

        public LocationController(ILocationService _service)
        {
            service = _service;

        }
        /// <summary>
        /// Get All Locations
        /// </summary>
        /// <returns>All Locations</returns>
        /// <response code = "200">Return all Locations</response>
        /// <response code = "400">Some error occured</response>
        /// 
        [HttpPost("list")]
        public IActionResult GetAllLocations([FromBody] PaginationFilter filter)
        {
            PagedResponse<List<LocationDTO>> locationlist = service.GetAllLocations(filter);
            return new ObjectResult(locationlist);
        }
        /// <summary>
        /// Get Location By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/Location
        ///     {
        ///         "LocationId" : "OqbI1toilNgCe9GJnppPug=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Location</returns>
        /// <response code = "200">Return Location by Id</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("single")]
        public IActionResult GetLocationById([FromQuery] string id)
        {
            LocationDTO location = service.GetLocationById(id);
            if (location != null)
            {
                return new ObjectResult(location);
            }
            else
            {
                return Content("Location with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new Location
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create/Update/Location
        ///         {
        ///               "LocationId" : "OqbI1toilNgCe9GJnppPug==",
        ///               "CityId" : "OqbI1toilNgCe9GJnppPug==",
        ///               "Name": "Location1",
        ///               "Adress" : "Kosturska5"
        ///         }
        /// </remarks>
        /// <param name="locationCreate"></param>
        /// <returns>Updated list of Locations</returns>
        /// <response code = "200">Return updated list of locations</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("create/update")]
        public IActionResult Addlocation([FromBody] LocationDTO locationCreate)
        {
            if (locationCreate.LocationId != "string" && !string.IsNullOrEmpty(locationCreate.LocationId))
            {
                bool result = service.UpdateLocation(locationCreate);
                return new ObjectResult(result);
            }
            else
            {
                locationCreate.Address = locationCreate.Address == "string" ? "" : locationCreate.Address;
                if (string.IsNullOrEmpty(locationCreate.Address) || string.IsNullOrEmpty(locationCreate.CityId))
                {
                    return BadRequest("Missing items");
                }
                LocationDTO newLocation = service.Addlocation(locationCreate);
                return new ObjectResult(newLocation);
            }
        }
        /// <summary>
        /// Remove Location by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/Location
        ///     {
        ///         "LocationId" : "OqbI1toilNgCe9GJnppPug=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Location</returns>
        /// <response code = "200">Return single Location</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("remove")]
        public IActionResult RemoveLocationv([FromQuery] string id)
        {
            bool result = service.RemoveLocation(id);
            return new ObjectResult(result);
        }
        /// <summary>
        /// Find locations in a city.
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET location/find/byCity
        ///     {
        ///         "cityId" : "OqbI1toilNgCe9GJnppPug=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Location</returns>
        /// <response code = "200">Returns all the locations in the city.</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("find/byCity")]
        public IActionResult FindByCity([FromQuery] string cityId)
        {
            List<LocationDTO> response = service.LocationByCity(cityId);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new NotFoundObjectResult("There aren't any locations in that city");
        }
    }
}
