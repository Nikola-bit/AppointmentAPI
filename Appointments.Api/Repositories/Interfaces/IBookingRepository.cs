using Appointments.Api.Models;
using AppointmentsDTO.DTO;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        public Booking CreateBooking(Booking informations);
        public Booking UpdateBooking(Booking informations);
        public Booking FindBookingById(int ID);
        public Booking DeleteBookingById(int ID);
        public List<Booking> ListAllBookings(BookingPaginationFilter informations);
        public List<Booking> ListBookingsByDay(DateTime date, int RoomId);
        public List<Booking> ListBookingByWeek(DateTime date, int RoomId);
        public List<Booking> ListBookingByMonth(DateTime date, int RoomId);
        public List<Room> FreeRoomsByTime(DateTime startingDate, DateTime endingDate, string locationId);
        public List<BookingParticipants> GetBookingParticipants(int ID);
        public void UpdateBookingInfoEmail(BookingParticipants participantInfo, Booking bookingInfo);
        //For Reminder
        public Reminder CreateReminder(Reminder reminder);
        public Reminder SelectReminder(int Id);
        public Reminder Delete(int id);
        public List<Reminder> ListBooking(ReminderPaginationFilter info);
        public List<Reminder> ListRecurrence(ReminderPaginationFilter info);

    }
}
