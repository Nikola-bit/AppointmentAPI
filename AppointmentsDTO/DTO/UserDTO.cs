using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
    public class UserCreateDTO
    {
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
    }
    public class UserDTO : UserCreateDTO
    {
        public string UserId { get; set; }
    }
    public class UserRoleDTO : UserDTO
    {
        public string Role { get; set; }

    }
    public class UserPaginationFilter : PaginationFilter
    {
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserToken
    {
        public UserRoleDTO User { get; set; }
        public string Token { get; set; }
    }
}
