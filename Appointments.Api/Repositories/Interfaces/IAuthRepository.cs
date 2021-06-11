using Appointments.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.API.Data
{
    public interface IAuthRepository
    {
        public string CreateToken(User users);
    }
}
