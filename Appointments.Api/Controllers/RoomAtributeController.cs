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
    [Route("roomAttribute")]
    [ApiController]
    public class RoomAtributeController : ControllerBase
    {
        private readonly IRoomAtributeService service;

        public RoomAtributeController(IRoomAtributeService _service)
        {
            service = _service;

        }
        /// <summary>
        /// Get All RoomsAtribute
        /// </summary>
        /// <returns>All RoomsAtribute</returns>
        /// <response code = "200">Return all RoomsAtibute</response>
        /// <response code = "400">Some error occured</response>
        /// 
        [HttpPost("list")]
        public IActionResult GetAllRoomsAtribute([FromBody] PaginationFilter filter)
        {
            PagedResponse<List<RoomAtributeDTO>> atributelist = service.GetAllRoomsAtribute(filter);
            return new ObjectResult(atributelist);
        }
        /// <summary>
        /// Get RoomAtribute By Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     GET /single/RoomAtribute
        ///     {
        ///         "roomAtributeId" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single RoomAtribute</returns>
        /// <response code = "200">Return RoomAtribute by Id</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("single")]
        public IActionResult GetRoomAtributeById([FromQuery] string id)
        {
            RoomAtributeDTO atribute = service.RoomAtributeById(id);
            if (atribute != null)
            {
                return new ObjectResult(atribute);
            }
            else
            {
                return Content("RoomAtribute with that ID is not found!");
            }
        }
        /// <summary>
        /// Create a new RoomAtribute
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /create/Update/RoomAtribute
        ///         {
        ///               "roomId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "atributeId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <param name="roomAtributeCreate"></param>
        /// <returns>Updated list of RoomAtribute</returns>
        /// <response code = "200">Return updated list of RoomAtribute</response>
        /// <response code = "400">Some error occured</response>
        [HttpPost("create/update")]
        public IActionResult AddRoomAtribute([FromBody] RoomAtributeDTO roomAtributeCreate)
        {
            if (roomAtributeCreate.RoomAttributeId != "string" && !string.IsNullOrEmpty(roomAtributeCreate.RoomAttributeId))
            {
                bool result = service.UpdateRoomAtribute(roomAtributeCreate);
                return new ObjectResult(result);
            }
            else
            {
                if (string.IsNullOrEmpty(roomAtributeCreate.RoomId))
                    if (string.IsNullOrEmpty(roomAtributeCreate.AttributeId))
                    {
                        return BadRequest("Missing items");
                    }
                RoomAtributeDTO newRoomAtribute = service.CreateRoomAtribute(roomAtributeCreate);
                return new ObjectResult(newRoomAtribute);
            }
        }
        /// <summary>
        /// Remove RoomAtribute by Id
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        /// 
        ///     Delete /single/RoomAtribute
        ///     {
        ///         "RoomAtributeId" : "I14kpXunkSuJIU+8++5v3Q=="
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Single RoomAtribute</returns>
        /// <response code = "200">Return single RoomAtribute</response>
        /// <response code = "400">Some error occured</response>
        [HttpGet("remove")]
        public IActionResult RemoveRoomAtribute([FromQuery] string id)
        {
            bool result = service.RemoveRoomAtribute(id);
            return new ObjectResult(result);
        }
    }
}
