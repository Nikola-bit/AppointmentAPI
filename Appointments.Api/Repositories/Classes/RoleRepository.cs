using Appointments.Api.Models;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Api.Models;
using Appointments.Api.Wrappers;
using Microsoft.AspNetCore.Diagnostics;

namespace Appointments.Api.Data
{
    public class RoleRepository : IRoleRepository
    {
        public Role AddRole(Role informations)
        {
            using (var db = new AppointmentContext())
            {

                db.Role.Add(informations);
                db.SaveChanges();

                return informations;
            }
        }

        public Role DeleteRole(int ID)
        {
            using (var db = new AppointmentContext())
            {
                Role deletedRole = db.Role.Where(r => r.RoleId == ID).FirstOrDefault();

                if (deletedRole != null)
                {
                    var response = deletedRole;

                    db.Role.Remove(deletedRole);
                    db.SaveChanges();

                    return response;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Role> ListAllRoles(RolePaginationFilter informations)
        {
            using (var db = new AppointmentContext())
            {
                List<Role> response = db.Role.ToList();

                if (informations.IsActive == true || informations.IsActive == false)
                {
                    response = db.Role.Where(r => r.IsActive == informations.IsActive)
                    .ToList();
                }

                response = response
                    .Skip((informations.PageNumber - 1) * informations.PageSize)
                    .Take(informations.PageSize)
                    .ToList();

                return response;
            }

        }
        public Role RoleById(int ID)
        {
            using (var db = new AppointmentContext())
            {
                Role role = db.Role.Where(r => r.RoleId == ID).FirstOrDefault();

                if (role == null) return null;
                else return role;
            }
        }
        public Role UpdateRole(Role informations)
        {
            using (var db = new AppointmentContext())
            {

                Role role = db.Role.Where(r => r.RoleId == informations.RoleId).FirstOrDefault();

                if (role == null)
                {
                    return null;
                }
                else
                {
                    role.Name = informations.Name;
                    role.IsActive = informations.IsActive;

                    db.SaveChanges();
                    return role;
                }
            }
        }
    }
}
