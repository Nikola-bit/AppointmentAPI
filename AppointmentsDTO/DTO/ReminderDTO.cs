using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
    public class ReminderCreateByBookingDTO
    {
        public string BookingId { get; set; }
        public string TypeId { get; set; }
        public int Value { get; set; }
    }
    public class ReminderCreateByRecurrenceDTO
    {
        public string BookingRecurrenceId { get; set; }
        public string TypeId { get; set; }
        public int Value { get; set; }
    }
    public class ReminderByBookingDTO : ReminderCreateByBookingDTO
    {
        public string ReminderId { get; set; }
        public DateTime DateCreated { get; set; }

        public bool IsDone { get; set; }
    }
    public class ReminderByRecurrenceDTO : ReminderCreateByRecurrenceDTO
    {
        public string ReminderId { get; set; }
        public bool IsDone { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class ReminderPaginationFilter : PaginationFilter
    {
        public string? BookingId { get; set; }
        public string? BookingRecurrenceId { get; set; }
        public string TypeId { get; set; }
        public bool IsDone { get; set; }
    }
}
