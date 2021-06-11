using AppointmentsDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services
{
    public interface IUserService
    {
        public UserRoleDTO CreateUser(UserDTO informations);
        public UserRoleDTO UpdateUser(UserDTO informations);
        public UserRoleDTO FindByID(string ID);
        public UserRoleDTO DeleteByID(string ID);
        public List<UserRoleDTO> ListAllUsers(UserPaginationFilter informations);
        public UserToken Login(UserLogin informations);
    }
}
