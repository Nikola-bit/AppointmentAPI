using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class User
    {
        public User()
        {
            Booking = new HashSet<Booking>();
            BookingRecurrence = new HashSet<BookingRecurrence>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<BookingRecurrence> BookingRecurrence { get; set; }
    }
}
