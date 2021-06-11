using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Models;
using Appointments.Api.Services.Interface;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [Route("room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService service;

        public RoomController(IRoomService _service)
        {
            service = _service;

        }
        /// <summary>
        /// Get All Rooms
        /// </summary>
        /// <returns>All Rooms</returns>
        /// <response code = "200">Return all Rooms</response>
        /// <response code = "400">Some error occured</response>
        /// 
        [HttpPost("list")]
        public IActionResult GetAllRooms([FromBody] PaginationFilter filter)
        {
            PagedResponse<List<RoomDTO>> roomlist = service.GetAllRooms(filter);
            return new ObjectResult(roomlist);
        }
        /// <summary>
        /// Get Room By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET room/single
        ///     {
        ///         "roomId" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Room</returns>
        /// <response code = "200">Return Room by Id</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("single")]
        public IActionResult GetRoomById([FromQuery] string id)
        {
            RoomDTO room = service.RoomById(id);
            if (room != null)
            {
                return new ObjectResult(room);
            }
            else
            {
                return Content("Room with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new Room
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST room/create
        ///         {
        ///               "Name": "Room1",
        ///               "locationId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "Capacity" : 12,
        ///               "isUsable" ; 1
        ///         }
        /// </remarks>
        /// <param name="roomCreate"></param>
        /// <returns>Updated list of Rooms</returns>
        /// <response code = "200">Return updated list of Rooms</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("create/update")]
        public IActionResult AddRoom([FromBody] RoomDTO roomCreate)
        {
            if (roomCreate.RoomId != "string" && !string.IsNullOrEmpty(roomCreate.RoomId))
            {
                bool result = service.UpdateRoom(roomCreate);
                return new ObjectResult(result);
            }
            else
            {
                roomCreate.Name = roomCreate.Name == "string" ? "" : roomCreate.Name;
                if (string.IsNullOrEmpty(roomCreate.Name))
                {
                    return BadRequest("Missing items");
                }
                RoomDTO newRoom = service.CreateRoom(roomCreate);
                return new ObjectResult(newRoom);
            }
        }
        /// <summary>
        /// Remove Room by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete room/remove
        ///     {
        ///         "RoomId" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Room</returns>
        /// <response code = "200">Return single Room</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("remove")]
        public IActionResult RemoveRoom([FromQuery] string id)
        {
            bool result = service.RemoveRoom(id);
            return new ObjectResult(result);
        }
        /// <summary>
        /// Get Room By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET room/single
        ///     {
        ///         "roomId" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single Room</returns>
        /// <response code = "200">Return room by ID with its attributes.</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("find/byAttribute")]
        public IActionResult GetRoomByIdWithAttribute([FromQuery] string id)
        {
            RoomAndAttributeDTO room = service.RoomByIdAttribute(id);
            if (room != null)
            {
                return new ObjectResult(room);
            }
            else
            {
                return new NotFoundObjectResult("Room with that ID is not found!");
            }
        }
        /// <summary>
        /// Find a room by capacity.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET room/find/byCapacity
        ///         {
        ///               "capacity": "20",
        ///         }
        /// </remarks>
        /// <returns>Room</returns>
        /// <response code = "200">Returns the rooms with the needed capacity.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("find/byCapacity")]
        public IActionResult FindRoomByCapacity(int capacity)
        {
            List<RoomAndAttributeDTO> response = service.FindRoomByCapacity(capacity);

            if (response != null)
            {
                return new ObjectResult(response);
            }
            else return new NotFoundObjectResult("There aren't any rooms with that capacity");
        }
        /// <summary>
        /// Find a room by an attribute.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET room/find/byAttribute
        ///         {
        ///               "attributeId": "I14kpXunkSuJIU+8++5v3Q==",
        ///         }
        /// </remarks>
        /// <returns>Room</returns>
        /// <response code = "200">Returns the rooms with the needed capacity.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("find/byFilter")]
        public IActionResult FindRoomsByFilter(FilterDTO info)
        {
            List<RoomAndAttributeDTO> response = service.FindByFilter(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }
            else return new NotFoundObjectResult("There aren't any rooms with that filter.");
        }

    }
}
