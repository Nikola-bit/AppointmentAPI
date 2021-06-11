using System;
using System.Collections.Generic;
using System.Text;
using AppointmentsDTO.Filters;

namespace AppointmentsDTO.DTO
{
    public class RoleCreateDTO
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
    public class RoleDTO : RoleCreateDTO
    {
        public string RoleId { get; set; }
    }
    public class RolePaginationFilter : PaginationFilter
    {
        public bool? IsActive { get; set; }
    }
}
