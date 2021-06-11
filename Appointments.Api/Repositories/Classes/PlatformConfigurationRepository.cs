using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Utilities;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Classes
{
    public class PlatformConfigurationRepository : IPlatformConfigurationRepository
    {
        private readonly IMapper mapper;
        public PlatformConfigurationRepository(IMapper _mapper)
        {

            mapper = _mapper;
        }
        public List<PlatformConfiguration> GetRoomAttributes()
        {
            using (var db = new AppointmentContext())
            {
                List<PlatformConfiguration> result = db.PlatformConfiguration.Where(r => r.ProgramCode == "ROOM_ATTRIBUTE").ToList();

                return result;
            }
        }
        public List<PlatformConfiguration> GetBookingInvitationStatuses()
        {
            using (var db = new AppointmentContext())
            {
                List<PlatformConfiguration> result = db.PlatformConfiguration.Where(r => r.ProgramCode == "BOOKING_INVITATION_STATUS").ToList();

                return result;
            }
        }
        public List<PlatformConfiguration> GetRecurrenceType()
        {
            using (var db = new AppointmentContext())
            {
                List<PlatformConfiguration> result = db.PlatformConfiguration.Where(r => r.ProgramCode == "RECURRENCE_TYPE").ToList();

                return result;
            }
        }

        public List<PlatformConfiguration> GetWeekday()
        {
            using (var db = new AppointmentContext())
            {
                List<PlatformConfiguration> result = db.PlatformConfiguration.Where(r => r.ProgramCode == "WEEKDAY").ToList();

                return result;
            }
        }
    }
}
