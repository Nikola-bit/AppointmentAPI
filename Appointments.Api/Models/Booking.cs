using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingParticipants = new HashSet<BookingParticipants>();
            Reminder = new HashSet<Reminder>();
        }

        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BookingParticipants> BookingParticipants { get; set; }
        public virtual ICollection<Reminder> Reminder { get; set; }
    }
}
