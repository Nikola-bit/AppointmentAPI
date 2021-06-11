using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class Reminder
    {
        public int ReminderId { get; set; }
        public int? BookingId { get; set; }
        public int? BookingRecurrenceId { get; set; }
        public int TypeId { get; set; }
        public int Value { get; set; }
        public bool IsDone { get; set; }
        public DateTime? ReminderDate { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual BookingRecurrence BookingRecurrence { get; set; }
        public virtual PlatformConfiguration Type { get; set; }
    }
}
