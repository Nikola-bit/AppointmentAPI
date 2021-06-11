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
    [Route("role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService service;
        public RoleController(IRoleService _service)
        {
            service = _service;
        }

        /// <summary>
        /// Create/Update a role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST role/create/update
        ///         {
        ///               "roleId": "I14kpXunkSuJIU+8++5v3Q==" (If making update),
        ///               "name": "Administrator",
        ///               "isActive": true
        ///         }
        /// </remarks>
        /// <returns>Role</returns>
        /// <response code = "200">Returns the created/updated role.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("create/update")]
        public IActionResult CreateRole([FromBody] RoleDTO info)
        {
            if (!string.IsNullOrEmpty(info.RoleId) && info.RoleId != "string")
            {
                RoleDTO response = service.UpdateRole(info);

                if (response != null)
                {
                    return new ObjectResult(response);
                }

                else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
            }
            else
            {
                RoleDTO response = service.AddRole(info);

                if (response != null)
                {
                    return new ObjectResult(response);
                }

                else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
            }
        }
        /// <summary>
        /// Find a role by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET role/single
        ///         {
        ///               "roleId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Role</returns>
        /// <response code = "200">Returns the specific role.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("single")]
        public IActionResult FindRoleByID([FromQuery] string ID)
        {
            RoleDTO response = service.RoleById(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There is not any role with the inserted ID.");
        }
        /// <summary>
        /// Delete a role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET role/delete
        ///         {
        ///               "roleId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>Role</returns>
        /// <response code = "200">Returns the deleted role.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("delete")]
        public IActionResult DeleteRole([FromQuery] string ID)
        {
            RoleDTO response = service.DeleteRole(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There is not any role with the inserted ID.");
        }
        /// <summary>
        /// List filtered roles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST role/list
        ///         {
        ///               "isActive": true,
        ///               "pageNumber": 1,
        ///               "pageSize": 15
        ///         }
        /// </remarks>
        /// <returns>Role</returns>
        /// <response code = "200">Returns the filtered roles.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("list")]
        public IActionResult ListAllRoles([FromBody] RolePaginationFilter info)
        {
            List<RoleDTO> response = service.ListAllRoles(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There aren't any roles with the custom filter or there aren't any.");
        }

    }
}
