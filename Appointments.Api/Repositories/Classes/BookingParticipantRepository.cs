using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Utilities;
using AppointmentsDTO.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Classes
{
    public class BookingParticipantRepository : IBookingParticipantRepository
    {
        public BookingParticipants CreateParticipant(BookingParticipants informations)
        {
            using (var db = new AppointmentContext())
            {
                informations.InvitationStatus = 1;
                db.BookingParticipants.Add(informations);
                db.SaveChanges();

                informations = db.BookingParticipants.Where(b => b.Email == informations.Email && b.BookingId == informations.BookingId)
                    .Include(u => u.InvitationStatusNavigation)
                    .FirstOrDefault();

                return informations;
            }
        }
        public BookingParticipants UpdateParticipant(BookingParticipants informations)
        {
            using (var db = new AppointmentContext())
            {
                BookingParticipants participant = db.BookingParticipants.Where(b => b.ParticipantId == informations.ParticipantId)
                    .Include(u => u.InvitationStatusNavigation)
                    .FirstOrDefault();

                participant.BookingId = informations.BookingId;
                participant.FirstName = informations.FirstName;
                participant.LastName = informations.LastName;
                participant.Email = informations.Email;
                participant.InvitationStatus = informations.InvitationStatus;

                db.SaveChanges();

                return participant;
            }
        }
        public BookingParticipants DeleteParticipant(int ID)
        {
            using (var db = new AppointmentContext())
            {
                BookingParticipants participant = db.BookingParticipants.Where(b => b.ParticipantId == ID)
                    .Include(u => u.InvitationStatusNavigation)
                    .FirstOrDefault();

                BookingParticipants response = participant;

                db.BookingParticipants.Remove(participant);
                db.SaveChanges();

                return response;
            }
        }

        public BookingParticipants FindByIdParticipant(int ID)
        {
            using (var db = new AppointmentContext())
            {
                BookingParticipants participant = db.BookingParticipants.Where(b => b.ParticipantId == ID)
                    .Include(u => u.InvitationStatusNavigation)
                    .FirstOrDefault();

                return participant;

            }
        }
        public List<BookingParticipants> ListAllParticipants(ParticipantPaginationFilter info)
        {
            using (var db = new AppointmentContext())
            {
                List<BookingParticipants> response = db.BookingParticipants
                    .Include(u => u.InvitationStatusNavigation)
                    .ToList();

                if (info.BookingId != "string" && !string.IsNullOrEmpty(info.BookingId))
                {
                    int helper = Convert.ToInt32(DataEncryption.Decrypt(info.BookingId));
                    response = response.Where(b => b.BookingId == helper)
                        .ToList();
                }
                if (info.InvitationStatusId != "string" && !string.IsNullOrEmpty(info.InvitationStatusId))
                {
                    int helper = Convert.ToInt32(DataEncryption.Decrypt(info.InvitationStatusId));
                    response = response.Where(b => b.InvitationStatus == helper)
                        .ToList();
                }

                response = response
                .Skip((info.PageNumber - 1) * info.PageSize)
                .Take(info.PageSize)
                .ToList();

                return response;
            }
        }
    }
}
