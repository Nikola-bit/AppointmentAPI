using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Security;
using Appointments.Api.Services.Interface;
using Appointments.Api.Utilities;
using Appointments.API.Utilities;
using AppointmentsDTO.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Classes
{
    public class BookingService : IBookingService
    {
        public IMapper mapper;
        public IBookingRepository repository;
        public BookingService(IMapper _mapper, IBookingRepository _repository)
        {
            mapper = _mapper;
            repository = _repository;
        }
        public BookingListingDTO CreateBooking(BookingDTO informations)
        {
            BookingCreateDTO helper = mapper.Map<BookingCreateDTO>(informations);

            Booking request = mapper.Map<Booking>(helper);

            Booking result = repository.CreateBooking(request);

            BookingListingDTO response = new BookingListingDTO();

            response.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(result.BookingId));

            response = mapper.Map<BookingListingDTO>(result);



            if (response != null)
            {
                MailDTO information = new MailDTO();

                information.Subject = "New appointment scheduled.";
                information.RecieverEmail = DataEncryption.Decrypt(response.UserEmail);
                information.Body = $"Hello {response.UserName}, \n\n\n We want to inform you that you have succesfully scheduled" +
                    $" a new appointment on {response.StartingDateTime.ToString("dd MMMM yyyy")} starting in {response.StartingDateTime.ToString("HH:mm")} and " +
                    $"ending in {response.EndingDateTime.ToString("HH:mm")}, which will take place in the {response.RoomName} located in our place {response.LocationName}" +
                    $" or more precisly on {response.LocationAddress}. We hope that you will also register all of the participants so they" +
                    $" could be notified about this meeting also by our company.";

                Task<bool> check = EmailSender.SendEmail(information);
            }

            return response;
        }
        public BookingListingDTO UpdateBooking(BookingDTO informations)
        {
            Booking request = mapper.Map<Booking>(informations);

            Booking result = repository.UpdateBooking(request);

            BookingListingDTO response = new BookingListingDTO();

            response.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(result.BookingId));

            response = mapper.Map<BookingListingDTO>(result);

            if (response != null)
            {
                MailDTO information = new MailDTO();

                information.Subject = "Appointment rescheduled.";
                information.RecieverEmail = DataEncryption.Decrypt(response.UserEmail);
                information.Body = $"Hello {response.UserName}, \n\n\n We want to inform you that your appointment was rescheduled." +
                    $" Now the appointment is on {response.StartingDateTime.ToString("dd MMMM yyyy")} starting in {response.StartingDateTime.ToString("HH:mm")} and " +
                    $"ending in {response.EndingDateTime.ToString("HH:mm")}, which will take place in the {response.RoomName} located in our place {response.LocationName}" +
                    $" or more precisly on {response.LocationAddress}. We hope that you will also register or have already registered all of the participants so they" +
                    $" could be notified about the changes by our company.";

                Task<bool> check = EmailSender.SendEmail(information);
            }

            return response;
        }

        public BookingListingDTO DeleteBookingById(string ID)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(ID));

            Booking result = repository.DeleteBookingById(request);

            BookingListingDTO response = new BookingListingDTO();

            response.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(result.BookingId));

            response = mapper.Map<BookingListingDTO>(result);

            if (response != null)
            {
                MailDTO information = new MailDTO();

                information.Subject = "Appointment cancelled.";
                information.RecieverEmail = DataEncryption.Decrypt(response.UserEmail);
                information.Body = $"Hello {response.UserName}, \n\n\n We want to inform you that you have succesfully cancelled your appointment. " +
                    $"It would have been on {response.StartingDateTime.ToString("dd MMMM yyyy")} starting in {response.StartingDateTime.ToString("HH:mm")} and " +
                    $"ending in {response.EndingDateTime.ToString("HH:mm")}, which would have taken place in the {response.RoomName} located in our place {response.LocationName}" +
                    $" or more precisly on {response.LocationAddress}. We hope that you will also register or have already registered all of the participants so they" +
                    $" could be notified about the cancelling from our company.";

                Task<bool> check = EmailSender.SendEmail(information);
            }


            return response;
        }

        public BookingListingDTO FindBookingById(string ID)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(ID));

            Booking result = repository.FindBookingById(request);

            BookingListingDTO response = new BookingListingDTO();

            response.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(result.BookingId));

            response = mapper.Map<BookingListingDTO>(result);

            return response;
        }

        public List<BookingListingDTO> ListAllBookings(BookingPaginationFilter informations)
        {
            List<Booking> result = repository.ListAllBookings(informations);

            result = result.OrderBy(o => o.RoomId).ToList();
            result = result.OrderBy(o => o.StartingDateTime).ToList();

            List<BookingListingDTO> response = new List<BookingListingDTO>();

            response = mapper.Map<List<BookingListingDTO>>(result);

            foreach (BookingListingDTO booking in response)
            {
                int id = Convert.ToInt32(DataEncryption.Decrypt(booking.BookingId));

                booking.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(id));
            }

            return response;
        }
        public List<BookingListingDTO> ListBookingsByDay(DateTime date, string RoomId)
        {
            List<Booking> result = repository.ListBookingsByDay(date, Convert.ToInt32(DataEncryption.Decrypt(RoomId)));

            result = result.OrderBy(o => o.StartingDateTime).ToList();

            List<BookingListingDTO> response = mapper.Map<List<BookingListingDTO>>(result);

            foreach (BookingListingDTO booking in response)
            {
                int id = Convert.ToInt32(DataEncryption.Decrypt(booking.BookingId));

                booking.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(id));
            }

            return response;
        }
        public List<BookingListingDTO> ListBookingsByWeek(DateTime date, string RoomId)
        {
            List<Booking> result = repository.ListBookingByWeek(date, Convert.ToInt32(DataEncryption.Decrypt(RoomId)));

            result = result.OrderBy(o => o.StartingDateTime).ToList();

            List<BookingListingDTO> response = mapper.Map<List<BookingListingDTO>>(result);

            foreach (BookingListingDTO booking in response)
            {
                int id = Convert.ToInt32(DataEncryption.Decrypt(booking.BookingId));

                booking.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(id));
            }

            return response;
        }
        public List<BookingListingDTO> ListBookingsByMonth(DateTime date, string RoomId)
        {
            List<Booking> result = repository.ListBookingByMonth(date, Convert.ToInt32(DataEncryption.Decrypt(RoomId)));

            result = result.OrderBy(o => o.StartingDateTime).ToList();

            List<BookingListingDTO> response = mapper.Map<List<BookingListingDTO>>(result);

            foreach (BookingListingDTO booking in response)
            {
                int id = Convert.ToInt32(DataEncryption.Decrypt(booking.BookingId));

                booking.Participants = mapper.Map<List<BookingParticipantsShowDTO>>(repository.GetBookingParticipants(id));
            }

            return response;
        }
        public List<RoomDTO> FindFreeRoom(DateTime startingDateTime, DateTime endingDateTime, string locationId)
        {
            List<Room> result = repository.FreeRoomsByTime(startingDateTime, endingDateTime, locationId);

            List<RoomDTO> response = mapper.Map<List<RoomDTO>>(result);

            return response;
        }
        public ReminderByBookingDTO CreateByBooking(ReminderCreateByBookingDTO informations)
        {
            Reminder reminder = mapper.Map<Reminder>(informations);
            ReminderByBookingDTO result = mapper.Map<ReminderByBookingDTO>(repository.CreateReminder(reminder));
            return result;
        }
        public ReminderByRecurrenceDTO CreteByRecurrence(ReminderCreateByRecurrenceDTO informations)
        {
            Reminder reminder = mapper.Map<Reminder>(informations);
            ReminderByRecurrenceDTO result = mapper.Map<ReminderByRecurrenceDTO>(repository.CreateReminder(reminder));
            return result;
        }
        public ReminderByRecurrenceDTO SelectR(string id)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(id));

            Reminder result = repository.SelectReminder(request);

            ReminderByRecurrenceDTO response = new ReminderByRecurrenceDTO();

            response = mapper.Map<ReminderByRecurrenceDTO>(result);

            return response;
        }
        public ReminderByBookingDTO SelectB(string id)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(id));

            Reminder result = repository.SelectReminder(request);

            ReminderByBookingDTO response = new ReminderByBookingDTO();

            response = mapper.Map<ReminderByBookingDTO>(result);

            return response;
        }
        public Reminder Delete(string id)
        {
            int request = Convert.ToInt32(DataEncryption.Decrypt(id));
            Reminder results = repository.Delete(request);
            return results;
        }
        public List<ReminderByBookingDTO> ListByBooking(ReminderPaginationFilter info)
        {
            List<Reminder> result = repository.ListBooking(info);

            List<ReminderByBookingDTO> response = mapper.Map<List<ReminderByBookingDTO>>(result);

            return response;
        }
        public List<ReminderByRecurrenceDTO> ListByRecurrence(ReminderPaginationFilter info)
        {
            List<Reminder> result = repository.ListRecurrence(info);

            List<ReminderByRecurrenceDTO> response = mapper.Map<List<ReminderByRecurrenceDTO>>(result);

            return response;
        }
    }
}
