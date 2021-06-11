using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
    public class BookingParticipantsCreateDTO
    {
        public string BookingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class BookingParticipantsDTO : BookingParticipantsCreateDTO
    {
        public string ParticipantId { get; set; }
        public string InvitationStatusId { get; set; }
    }
    public class BookingParticipantsShowDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string InvitationStatus { get; set; }
        public string ParticipantId { get; set; }
        public string BookingId { get; set; }
        public string InvitationStatusId { get; set; }
    }
    public class ParticipantPaginationFilter : PaginationFilter
    {
        public string BookingId { get; set; }
        public string InvitationStatusId { get; set; }
    }
}