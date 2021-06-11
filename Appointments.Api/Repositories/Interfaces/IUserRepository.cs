using Appointments.Api.Models;
using AppointmentsDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Data
{
    public interface IUserRepository
    {
        public User CreateUser(User informations);
        public User UpdateUser(User informations);
        public User FindByID(int ID);
        public User DeleteByID(int ID);
        public List<User> ListAllUsers(UserPaginationFilter informations);
        public User Login(UserLogin informations);
    }
}
