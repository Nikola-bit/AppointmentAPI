using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Security;
using Appointments.Api.Utilities;
using Appointments.Api.Wrappers;
using Appointments.API.Utilities;
using AppointmentsDTO.DTO;
using AppointmentsDTO.DTO.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Classes
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IBookingRecurrenceRepository repository;
        public BookingRepository(IBookingRecurrenceRepository _repository)
        {
            repository = _repository;
        }
        public Booking CreateBooking(Booking informations)
        {
            using (var db = new AppointmentContext())
            {
                Booking helper = new Booking();

                if (repository.CheckAvailability(informations.RoomId, informations.StartingDateTime, informations.EndingDateTime) == true)
                {
                    Booking response = db.Booking.Where(b => b.StartingDateTime == informations.StartingDateTime)
                        .Include(u => u.User)
                        .Include(u => u.Room)
                        .ThenInclude(u => u.Location)
                        .ThenInclude(u => u.City)
                        .FirstOrDefault();

                    return response;
                }
                else return null;
            }
        }
        public Booking UpdateBooking(Booking informations)
        {
            using (var db = new AppointmentContext())
            {
                Booking helper = new Booking();

                if (repository.CheckAvailability(informations.RoomId, informations.StartingDateTime, informations.EndingDateTime) == true)
                {
                    Booking response = db.Booking.Where(b => b.BookingId == informations.BookingId)
                                .Include(u => u.User)
                                .Include(u => u.Room)
                                .ThenInclude(u => u.Location)
                                .ThenInclude(u => u.City)
                                .FirstOrDefault();

                    response.RoomId = informations.RoomId;
                    response.UserId = informations.UserId;
                    response.StartingDateTime = informations.StartingDateTime;
                    response.EndingDateTime = informations.EndingDateTime;

                    db.SaveChanges();

                    List<BookingParticipants> participants = db.BookingParticipants.Where(b => b.BookingId == informations.BookingId)
                        .Include(u => u.InvitationStatusNavigation)
                        .ToList();

                    foreach (BookingParticipants participant in participants)
                    {
                        UpdateBookingInfoEmail(participant, response);
                    }

                    return response;
                }
                else return null;
            }
        }
        public Booking DeleteBookingById(int ID)
        {
            using (var db = new AppointmentContext())
            {
                Booking deletedBooking = db.Booking.Where(b => b.BookingId == ID)
                    .Include(u => u.User)
                    .Include(u => u.Room)
                    .ThenInclude(u => u.Location)
                    .ThenInclude(u => u.City)
                    .FirstOrDefault();

                Booking response = deletedBooking;

                if (deletedBooking != null)
                {
                    db.Booking.Remove(deletedBooking);
                    db.SaveChanges();

                    return response;
                }

                else return response;
            }
        }
        public Booking FindBookingById(int ID)
        {
            using (var db = new AppointmentContext())
            {
                Booking response = db.Booking.Where(b => b.BookingId == ID)
                    .Include(u => u.User)
                    .Include(u => u.Room)
                    .ThenInclude(u => u.Location)
                    .ThenInclude(u => u.City)
                    .FirstOrDefault();

                return response;
            }
        }
        public List<Booking> ListAllBookings(BookingPaginationFilter informations)
        {
            using (var db = new AppointmentContext())
            {

                List<Booking> response = db.Booking
                    .Include(u => u.User)
                    .Include(u => u.Room)
                    .ThenInclude(u => u.Location)
                    .ThenInclude(u => u.City)
                    .ToList();

                if (informations.RoomId != "string" && !string.IsNullOrEmpty(informations.RoomId))
                {
                    int ID = Convert.ToInt32(DataEncryption.Decrypt(informations.RoomId));
                    response = response.Where(u => u.RoomId == ID).ToList();
                }
                if (informations.UserId != "string" && !string.IsNullOrEmpty(informations.UserId))
                {
                    int ID = Convert.ToInt32(DataEncryption.Decrypt(informations.UserId));
                    response = response.Where(u => u.UserId == ID).ToList();
                }
                if (informations.LocationId != "string" && !string.IsNullOrEmpty(informations.LocationId))
                {
                    int ID = Convert.ToInt32(DataEncryption.Decrypt(informations.LocationId));
                    response = response.Where(u => u.Room.LocationId == ID).ToList();
                }

                response = response
                    .Skip((informations.PageNumber - 1) * informations.PageSize)
                    .Take(informations.PageSize)
                    .ToList();

                return response;
            }
        }
        public List<Booking> ListBookingsByDay(DateTime date, int RoomId)
        {
            using (var db = new AppointmentContext())
            {

                List<Booking> response = db.Booking
                    .Include(u => u.User)
                    .Include(u => u.Room)
                    .ThenInclude(u => u.Location)
                    .ThenInclude(u => u.City)
                    .ToList();

                response = response.Where(u => u.RoomId == RoomId).ToList();
                response = response.Where(u => u.StartingDateTime.Date == date.Date || u.EndingDateTime.Date == date.Date).ToList();

                return response;
            }
        }
        public List<Booking> ListBookingByWeek(DateTime date, int RoomId)
        {
            using (var db = new AppointmentContext())
            {
                DateTime result = date;
                while (result.DayOfWeek != System.DayOfWeek.Monday)
                    result = result.AddDays(-1);

                date = result;

                DateTime dateHelper = date.AddDays(6);

                List<Booking> response = db.Booking
                    .Include(u => u.User)
                    .Include(u => u.Room)
                    .ThenInclude(u => u.Location)
                    .ThenInclude(u => u.City)
                    .ToList();

                response = response.Where(u => u.RoomId == RoomId).ToList();
                response = response.Where(u => u.StartingDateTime.Date >= date.Date && u.EndingDateTime.Date <= dateHelper.Date).ToList();

                return response;
            }
        }
        public List<Booking> ListBookingByMonth(DateTime date, int RoomId)
        {
            using (var db = new AppointmentContext())
            {
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                List<Booking> response = db.Booking
                    .Include(u => u.User)
                    .Include(u => u.Room)
                    .ThenInclude(u => u.Location)
                    .ThenInclude(u => u.City)
                    .ToList();

                response = response.Where(u => u.RoomId == RoomId).ToList();
                response = response.Where(u => u.StartingDateTime.Date >= firstDayOfMonth.Date && u.EndingDateTime.Date <= lastDayOfMonth.Date).ToList();

                return response;
            }
        }

        public List<Room> FreeRoomsByTime(DateTime startingDate, DateTime endingDate, string locationId)
        {
            using (var db = new AppointmentContext())
            {
                List<Room> freeRooms = db.Room.ToList();

                foreach (Room room in freeRooms)
                {
                    if (repository.CheckAvailability(room.RoomId, startingDate, endingDate) == true
                        && room.LocationId == Convert.ToInt32(DataEncryption.Decrypt(locationId)))
                    {
                        room.IsUsable = true;
                    }
                    else room.IsUsable = false;
                }

                freeRooms = freeRooms.Where(b => b.IsUsable == true).ToList();
                return freeRooms;
            }
        }
        public void UpdateBookingInfoEmail(BookingParticipants participantInfo, Booking bookingInfo)
        {
            MailDTO information = new MailDTO();

            information.Subject = "Appointment rescheduled.";
            information.RecieverEmail = DataEncryption.Decrypt(participantInfo.Email);
            information.Body = $"Hello {participantInfo.FirstName} {participantInfo.LastName}, \n\n\n We want to inform you that your incoming appointment" +
                $" was moved to be on {bookingInfo.StartingDateTime.ToString("dd MMMM yyyy")} starting in {bookingInfo.StartingDateTime.ToString("HH:mm")} and " +
                $"ending in {bookingInfo.EndingDateTime.ToString("HH:mm")}, which will take place in the {bookingInfo.Room.Name} located in our place {bookingInfo.Room.Location.Name}" +
                $" or more precisly on {bookingInfo.Room.Location.Address}, {bookingInfo.Room.Location.City.Name}. This appointment was scheduled by {bookingInfo.User.FirstName} {bookingInfo.User.LastName}. We hope that you will send us back an email to " +
                $" confirm or deny our invitation.";

            Task<bool> check = EmailSender.SendEmail(information);
        }
        public List<BookingParticipants> GetBookingParticipants(int ID)
        {
            using (var db = new AppointmentContext())
            {
                List<BookingParticipants> response = db.BookingParticipants.Where(b => b.BookingId == ID).ToList();

                return response;
            }
        }
        public Reminder CreateReminder(Reminder reminder)
        {
            using (var db = new AppointmentContext())
            {

                if (reminder.BookingId != null)
                {
                    Booking response = db.Booking.Where(b => b.BookingId == reminder.BookingId)
                        .FirstOrDefault();
                    if (reminder.TypeId == Convert.ToInt32(RECURRENCE_TYPE.Daily))
                    {
                        reminder.ReminderDate = response.StartingDateTime.AddDays(-reminder.Value);
                    }
                    if (reminder.TypeId == Convert.ToInt32(RECURRENCE_TYPE.Weekly))
                    {
                        reminder.ReminderDate = response.StartingDateTime.AddDays(-reminder.Value * 7);
                    }
                    if (reminder.TypeId == Convert.ToInt32(RECURRENCE_TYPE.Monthly))
                    {
                        reminder.ReminderDate = response.StartingDateTime.AddMonths(-reminder.Value);
                    }
                    if (reminder.TypeId == Convert.ToInt32(RECURRENCE_TYPE.Yearly))
                    {
                        reminder.ReminderDate = response.StartingDateTime.AddYears(-reminder.Value);
                    }
                    if (reminder.TypeId == Convert.ToInt32(RECURRENCE_TYPE.Minutly))
                    {
                        reminder.ReminderDate = response.StartingDateTime.AddMinutes(-reminder.Value);
                    }
                    db.Reminder.Add(reminder);
                    db.SaveChanges();
                    return reminder;
                }
                if (reminder.BookingRecurrenceId != null)
                {
                    db.Reminder.Add(reminder);
                    db.SaveChanges();
                    return reminder;
                }
                else return null;

            }
        }
        public Reminder SelectReminder(int ID)
        {
            using (var db = new AppointmentContext())
            {
                Reminder response = db.Reminder.Where(b => b.ReminderId == ID)
                    .FirstOrDefault();

                return response;
            }
        }
        public Reminder Delete(int id)
        {
            using (var db = new AppointmentContext())
            {
                Reminder deletedReminder = db.Reminder.Where(b => b.ReminderId == id)
                    .FirstOrDefault();

                Reminder response = deletedReminder;

                if (deletedReminder != null)
                {
                    db.Reminder.Remove(deletedReminder);
                    db.SaveChanges();

                    return response;
                }

                else return response;
            }
        }
        public List<Reminder> ListBooking(ReminderPaginationFilter info)
        {
            using (var db = new AppointmentContext())
            {
                List<Reminder> reminders = db.Reminder.ToList();

                if(info.IsDone == true || info.IsDone == false)
                {
                    reminders = reminders.Where(r => r.IsDone == info.IsDone).ToList();
                }
                if(info.TypeId != "string" && !string.IsNullOrEmpty(info.TypeId))
                {
                    reminders = reminders.Where(r => r.TypeId == Convert.ToInt32(EncryptionHelper.Decrypt(info.TypeId))).ToList();
                }
                if(info.BookingId != "string" && !string.IsNullOrEmpty(info.BookingId))
                {
                    reminders = reminders.Where(r => r.BookingId == Convert.ToInt32(EncryptionHelper.Decrypt(info.BookingId))).ToList();
                }
                reminders = reminders.Skip((info.PageNumber - 1) * info.PageSize)
                    .Take(info.PageSize)
                    .ToList();

                return reminders;
            }
        }
        public List<Reminder> ListRecurrence(ReminderPaginationFilter info)
        {
            using (var db = new AppointmentContext())
            {
                List<Reminder> reminders = db.Reminder.ToList();

                if (info.IsDone == true || info.IsDone == false)
                {
                    reminders = reminders.Where(r => r.IsDone == info.IsDone).ToList();
                }
                if (info.TypeId != "string" && !string.IsNullOrEmpty(info.TypeId))
                {
                    reminders = reminders.Where(r => r.TypeId == Convert.ToInt32(EncryptionHelper.Decrypt(info.TypeId))).ToList();
                }
                if (info.BookingRecurrenceId != "string" && !string.IsNullOrEmpty(info.BookingRecurrenceId))
                {
                    reminders = reminders.Where(r => r.BookingRecurrenceId == Convert.ToInt32(EncryptionHelper.Decrypt(info.BookingRecurrenceId))).ToList();
                }
                reminders = reminders.Skip((info.PageNumber - 1) * info.PageSize)
                    .Take(info.PageSize)
                    .ToList();

                return reminders;
            }
        }

    }
}