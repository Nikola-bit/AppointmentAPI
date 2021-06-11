using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class BookingParticipants
    {
        public int ParticipantId { get; set; }
        public int BookingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int InvitationStatus { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual PlatformConfiguration InvitationStatusNavigation { get; set; }
    }
}
