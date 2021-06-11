using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
            BookingRecurrence = new HashSet<BookingRecurrence>();
            RoomAttribute = new HashSet<RoomAttribute>();
        }

        public int RoomId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool? IsUsable { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<BookingRecurrence> BookingRecurrence { get; set; }
        public virtual ICollection<RoomAttribute> RoomAttribute { get; set; }
    }
}
