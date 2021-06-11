using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
   public class BookingRecurrenceDaysCreateDTO
    {
       
        public string RecurrenceId { get; set; }
        public string Weekday { get; set; }
    }
    public class BookingRecurrenceDaysDTO : BookingRecurrenceDaysCreateDTO
    {
        public string Id { get; set; }
    }
}
