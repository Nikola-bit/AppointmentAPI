using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class BookingRecurrence
    {
        public BookingRecurrence()
        {
            BookingRecurrenceDays = new HashSet<BookingRecurrenceDays>();
            Reminder = new HashSet<Reminder>();
        }

        public int RecurrenceId { get; set; }
        public int UserId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? Duration { get; set; }
        public int? Type { get; set; }
        public int? Value { get; set; }
        public int? WeekIncrement { get; set; }
        public int? MonthIncrement { get; set; }
        public int? YearIncrement { get; set; }
        public bool? CustomDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EndAfter { get; set; }
        public bool? NoEndDate { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
        public virtual PlatformConfiguration TypeNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BookingRecurrenceDays> BookingRecurrenceDays { get; set; }
        public virtual ICollection<Reminder> Reminder { get; set; }
    }
}
