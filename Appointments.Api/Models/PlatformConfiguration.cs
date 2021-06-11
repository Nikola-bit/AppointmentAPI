using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class PlatformConfiguration
    {
        public PlatformConfiguration()
        {
            BookingParticipants = new HashSet<BookingParticipants>();
            BookingRecurrence = new HashSet<BookingRecurrence>();
            BookingRecurrenceDays = new HashSet<BookingRecurrenceDays>();
            Reminder = new HashSet<Reminder>();
            RoomAttribute = new HashSet<RoomAttribute>();
        }

        public int Value { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<BookingParticipants> BookingParticipants { get; set; }
        public virtual ICollection<BookingRecurrence> BookingRecurrence { get; set; }
        public virtual ICollection<BookingRecurrenceDays> BookingRecurrenceDays { get; set; }
        public virtual ICollection<Reminder> Reminder { get; set; }
        public virtual ICollection<RoomAttribute> RoomAttribute { get; set; }
    }
}
