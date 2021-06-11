using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class BookingRecurrenceDays
    {
        public int Id { get; set; }
        public int RecurrenceId { get; set; }
        public int Weekday { get; set; }

        public virtual BookingRecurrence Recurrence { get; set; }
        public virtual PlatformConfiguration WeekdayNavigation { get; set; }
    }
}
