using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Models;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using Appointments.Api.Wrappers;

namespace Appointments.Api.Data
{
    public interface IRoleRepository
    {
        public Role AddRole(Role informations);
        public Role UpdateRole(Role informations);
        public Role RoleById(int ID);
        public List<Role> ListAllRoles(RolePaginationFilter informations);
        public Role DeleteRole(int ID);
    }
}
