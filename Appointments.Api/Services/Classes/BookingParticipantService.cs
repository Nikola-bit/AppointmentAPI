using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Services.Interface;
using Appointments.Api.Utilities;
using Appointments.API.Utilities;
using AppointmentsDTO.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Classes
{
    public class BookingParticipantService : IBookingParticipantService
    {
        public IMapper mapper;
        public IBookingParticipantRepository repository;
        public IBookingService service;
        public BookingParticipantService(IMapper _mapper, IBookingParticipantRepository _repository, IBookingService _service)
        {
            mapper = _mapper;
            repository = _repository;
            service = _service;
        }
        public BookingParticipantsShowDTO CreateParticipant(BookingParticipantsDTO informations)
        {
            BookingParticipantsCreateDTO helper = mapper.Map<BookingParticipantsCreateDTO>(informations);

            BookingParticipants request = mapper.Map<BookingParticipants>(helper);

            BookingParticipants result = repository.CreateParticipant(request);

            BookingParticipantsShowDTO response = mapper.Map<BookingParticipantsShowDTO>(result);

            if (response != null)
            {
                MailDTO information = new MailDTO();

                BookingListingDTO helperBooking = service.FindBookingById(response.BookingId);

                information.Subject = "New appointment soon.";
                information.RecieverEmail = DataEncryption.Decrypt(response.Email);
                information.Body = $"Hello {response.Name}, \n\n\n We want to inform you that you have an appointment" +
                    $" on {helperBooking.StartingDateTime.ToString("dd MMMM yyyy")} starting in {helperBooking.StartingDateTime.ToString("HH:mm")} and " +
                    $"ending in {helperBooking.EndingDateTime.ToString("HH:mm")}, which will take place in the {helperBooking.RoomName} located in our place {helperBooking.LocationName}" +
                    $" or more precisly on {helperBooking.LocationAddress}. This appointment was scheduled by {helperBooking.UserName}. We hope that you will send us back an email to " +
                    $" confirm or deny our invitation.";

                Task<bool> check = EmailSender.SendEmail(information);
            }

            return response;
        }
        public BookingParticipantsShowDTO UpdateParticipant(BookingParticipantsDTO informations)
        {
            BookingParticipants request = mapper.Map<BookingParticipants>(informations);

            BookingParticipants result = repository.UpdateParticipant(request);

            BookingParticipantsShowDTO response = mapper.Map<BookingParticipantsShowDTO>(result);

            return response;
        }
        public BookingParticipantsShowDTO DeleteParticipant(string ID)
        {
            int decryptedID = Convert.ToInt32(DataEncryption.Decrypt(ID));

            BookingParticipants result = repository.DeleteParticipant(decryptedID);

            BookingParticipantsShowDTO response = mapper.Map<BookingParticipantsShowDTO>(result);

            return response;
        }

        public BookingParticipantsShowDTO FindByIdParticipant(string ID)
        {
            int decryptedID = Convert.ToInt32(DataEncryption.Decrypt(ID));

            BookingParticipants result = repository.FindByIdParticipant(decryptedID);

            BookingParticipantsShowDTO response = mapper.Map<BookingParticipantsShowDTO>(result);

            return response;
        }
        public List<BookingParticipantsShowDTO> ListAllParticipants(ParticipantPaginationFilter info)
        {

            List<BookingParticipants> result = repository.ListAllParticipants(info);

            List<BookingParticipantsShowDTO> response = mapper.Map<List<BookingParticipantsShowDTO>>(result);

            return response;
        }
    }
}
