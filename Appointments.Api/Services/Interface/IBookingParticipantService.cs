using AppointmentsDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services
{
    public interface IBookingParticipantService
    {
        public BookingParticipantsShowDTO CreateParticipant(BookingParticipantsDTO informations);
        public BookingParticipantsShowDTO UpdateParticipant(BookingParticipantsDTO informations);
        public BookingParticipantsShowDTO DeleteParticipant(string ID);
        public BookingParticipantsShowDTO FindByIdParticipant(string ID);
        public List<BookingParticipantsShowDTO> ListAllParticipants(ParticipantPaginationFilter info);
    }
}
