using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace AppointmentsDTO
{
    public class BookingRecurrenceCreateDTO
    {
        public string UserId { get; set; }
        public string RoomId { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? Duration { get; set; }
        public string? Type { get; set; }
        public int? Value { get; set; }
        public int? WeekIncrement { get; set; }
        public int? MonthIncrement { get; set; }
        public int? YearIncrement { get; set; }
        public bool? CustomDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EndAfter { get; set; }
        public bool? NoEndDate { get; set; }
        public List<BookingRecurrenceDaysCreate> WeekDays { get; set; }
    }
    public class BookingRecurrenceUpdateDTO : BookingRecurrenceCreateDTO
    {
        public string RecurrenceId { get; set; }
    }
    public class BookingRecurrenceDaysCreate
    {
        public string Weekday { get; set; }
    }
    public class BookingRecurrenceDTO
    {
        public string RecurrenceId { get; set; }
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? Duration { get; set; }
        public string? Type { get; set; }
        public int? Value { get; set; }
        public int? WeekIncrement { get; set; }
        public int? MonthIncrement { get; set; }
        public int? YearIncrement { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EndAfter { get; set; }
        public bool? NoEndDate { get; set; }
        public List<BookingRecurrenceDaysCreate> WeekDays { get; set; }
    }
    public class RecurrencePaginationFilter : PaginationFilter
    {
        public string RoomId { get; set; }
    }
}
