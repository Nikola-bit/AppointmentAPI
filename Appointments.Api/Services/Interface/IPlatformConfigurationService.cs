using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Interface
{
   public interface IPlatformConfigurationService
    {
        public List<PlatformConfigurationDTO> GetRoomAttributes();
        public List<PlatformConfigurationDTO> GetBookingInvitationStatuses();
        public List<PlatformConfigurationDTO> RecurrenceType();
        public List<PlatformConfigurationDTO> GetWeekday();
    }
}
