using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services
{
    public interface IRoleService
    {
        public RoleDTO AddRole(RoleDTO informations);
        public RoleDTO UpdateRole(RoleCreateDTO informations);
        public RoleDTO RoleById(string ID);
        public List<RoleDTO> ListAllRoles(RolePaginationFilter informations);
        public RoleDTO DeleteRole(string ID);
    }
}
