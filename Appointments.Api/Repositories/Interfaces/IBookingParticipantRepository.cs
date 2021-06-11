using Appointments.Api.Models;
using AppointmentsDTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Interfaces
{
    public interface IBookingParticipantRepository
    {
        public BookingParticipants CreateParticipant(BookingParticipants informations);
        public BookingParticipants UpdateParticipant(BookingParticipants informations);
        public BookingParticipants DeleteParticipant(int ID);
        public BookingParticipants FindByIdParticipant(int ID);
        public List<BookingParticipants> ListAllParticipants(ParticipantPaginationFilter info);
    }
}
