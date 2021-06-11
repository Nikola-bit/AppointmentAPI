using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Filters;
using Appointments.Api.Security;
using Appointments.Api.Services;
using Appointments.Api.Utilities;
using AppointmentsDTO.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService _service)
        {
            service = _service;
        }

        /// <summary>
        /// Create/Update an user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST user/create/update
        ///         {
        ///               "userId": "I14kpXunkSuJIU+8++5v3Q==" (If making update),
        ///               "roleId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "firstName": "Peter",
        ///               "lastName": "Johnson",
        ///               "email": "peter.johnson@mail.com",
        ///               "password": "123456789",
        ///               "gender": "Male"
        ///         }
        /// </remarks>
        /// <returns>User</returns>
        /// <response code = "200">Returns the created/updated user.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("create/update")]
        public IActionResult CreateUser([FromBody] UserDTO info)
        {
            if (!string.IsNullOrEmpty(info.UserId) && info.UserId != "string")
            {
                UserRoleDTO response = service.UpdateUser(info);

                if (response != null)
                {
                    return new ObjectResult(response);
                }

                else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
            }
            else
            {
                UserRoleDTO response = service.CreateUser(info);

                if (response != null)
                {
                    return new ObjectResult(response);
                }

                else return new BadRequestObjectResult("Try again, you missed some of the needed informations.");
            }
        }
        /// <summary>
        /// Find an user by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /user/single
        ///         {
        ///               "userId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>User</returns>
        /// <response code = "200">Returns the searched user.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("single")]
        public IActionResult FindUserByID([FromQuery] string ID)
        {
            UserRoleDTO response = service.FindByID(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There is not any user with the inserted ID.");
        }
        /// <summary>
        /// Delete an user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /user/delete
        ///         {
        ///               "userId": "I14kpXunkSuJIU+8++5v3Q=="
        ///         }
        /// </remarks>
        /// <returns>User</returns>
        /// <response code = "200">Returns the deleted user.</response>
        /// <response code = "400">An error occured.</response>
        [HttpGet("delete")]
        public IActionResult DeleteUser([FromQuery] string ID)
        {
            UserRoleDTO response = service.DeleteByID(ID);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There is not any user with the inserted ID.");
        }
        /// <summary>
        /// List filtered users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST user/list
        ///         {
        ///               "roleId": "I14kpXunkSuJIU+8++5v3Q==",
        ///               "firstName": "Peter",
        ///               "lastName": "Johnson",
        ///               "gender": "Male",
        ///               "pageNumber": "1",
        ///               "pageSize": "15"
        ///         }
        /// </remarks>
        /// <returns>User</returns>
        /// <response code = "200">Returns the filtered users.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("list")]
        public IActionResult ListAllUsers([FromBody] UserPaginationFilter info)
        {
            List<UserRoleDTO> response = service.ListAllUsers(info);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("There aren't any users with the custom filter or there aren't any.");
        }
        /// <summary>
        /// Log in for token.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST user/login
        ///         {
        ///               "email": "peter.johnson@mail.com",
        ///               "password": "123456789"
        ///         }
        /// </remarks>
        /// <returns>User</returns>
        /// <response code = "200">Returns the logged user with the token.</response>
        /// <response code = "400">An error occured.</response>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin informations)
        {
            UserToken response = service.Login(informations);

            if (response != null)
            {
                return new ObjectResult(response);
            }

            else return new BadRequestObjectResult("Wrong credentials.");
        }
        [HttpGet("decrypt")]
        public IActionResult Decrypt([FromQuery] string Text)
        {
            string response = EncryptionHelper.Decrypt(Text);

            return new ObjectResult(response);
        }
        [HttpGet("encrypt")]
        public IActionResult Encrypt([FromQuery] string text)
        {
            string response = EncryptionHelper.Encrypt(text);

            return new ObjectResult(response);
        }
    }
}
