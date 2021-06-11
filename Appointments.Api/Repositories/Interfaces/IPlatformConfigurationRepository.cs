using Appointments.Api.Models;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Interfaces
{
   public interface IPlatformConfigurationRepository
    {
        public List<PlatformConfiguration> GetRoomAttributes();
        public List<PlatformConfiguration> GetBookingInvitationStatuses();
        public List<PlatformConfiguration> GetRecurrenceType();
        public List<PlatformConfiguration> GetWeekday();

    }
}
