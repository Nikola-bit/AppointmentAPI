using Appointments.Api.Models;
using Appointments.Api.Utilities;
using AppointmentsDTO.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Data
{
    public class UserRepository : IUserRepository
    {
        public User CreateUser(User informations)
        {
            using (var db = new AppointmentContext())
            {
                db.User.Add(informations);
                db.SaveChanges();
                informations = db.User.Where(u => u.UserId == informations.UserId)
                    .Include(r => r.Role)
                    .FirstOrDefault();

                return informations;
            }
        }
        public User UpdateUser(User informations)
        {
            using (var db = new AppointmentContext())
            {
                User user = db.User.Where(u => u.UserId == informations.UserId)
                    .Include(r => r.Role)
                    .FirstOrDefault();

                if (user != null)
                {
                    user.RoleId = informations.RoleId;
                    user.FirstName = informations.FirstName;
                    user.LastName = informations.LastName;
                    user.Email = informations.Email;
                    user.Password = informations.Password;
                    user.Gender = informations.Gender;

                    db.SaveChanges();

                    return user;
                }

                else return null;
            }
        }
        public User FindByID(int ID)
        {
            using (var db = new AppointmentContext())
            {
                User user = db.User.Where(u => u.UserId == ID)
                    .Include(r => r.Role)
                    .FirstOrDefault();

                return user;
            }
        }
        public User DeleteByID(int ID)
        {
            using (var db = new AppointmentContext())
            {
                User user = db.User.Where(u => u.UserId == ID)
                    .Include(r => r.Role)
                    .FirstOrDefault();

                User deletedUser = user;

                if (user != null)
                {
                    db.User.Remove(deletedUser);
                    db.SaveChanges();

                    return user;
                }

                else return null;
            }
        }
        public List<User> ListAllUsers(UserPaginationFilter informations)
        {
            using (var db = new AppointmentContext())
            {
                List<User> response = db.User.Include(r => r.Role).ToList();



                if (informations.FirstName != "string" && !string.IsNullOrEmpty(informations.FirstName))
                {
                    response = response.Where(u => u.FirstName == informations.FirstName).ToList();
                }

                if (informations.LastName != "string" && !string.IsNullOrEmpty(informations.LastName))
                {
                    response = response.Where(u => u.LastName == informations.LastName).ToList();
                }

                if (informations.Gender != "string" && !string.IsNullOrEmpty(informations.Gender))
                {
                    response = response.Where(u => u.Gender == informations.Gender).ToList();
                }

                if (informations.RoleId != "string" && !string.IsNullOrEmpty(informations.RoleId))
                {
                    int ID = Convert.ToInt32(DataEncryption.Decrypt(informations.RoleId));
                    response = response.Where(u => u.RoleId == ID).ToList();
                }

                response = response
                    .Skip((informations.PageNumber - 1) * informations.PageSize)
                    .Take(informations.PageSize)
                    .ToList();

                return response;
            }
        }
        public User Login(UserLogin informations)
        {
            using (var db = new AppointmentContext())
            {
                User user = db.User.Where(u => u.Email == informations.Email && u.Password == informations.Password)
                    .Include(r => r.Role)
                    .FirstOrDefault();

                return user;
            }

        }
    }
}
