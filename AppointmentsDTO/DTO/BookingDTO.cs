using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
    public class BookingCreateDTO
    {
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
    }
    public class BookingDTO : BookingCreateDTO
    {
        public string BookingId { get; set; }
    }
    public class BookingListingDTO : BookingDTO
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public List<BookingParticipantsShowDTO> Participants { get; set; }
    }
    public class BookingPaginationFilter : PaginationFilter
    {
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public string LocationId { get; set; }
    }
    public class ListingDTO
    {
        public DateTime Date { get; set; }
        public string RoomId { get; set; }
        public string  Type { get; set; }
    }
}
