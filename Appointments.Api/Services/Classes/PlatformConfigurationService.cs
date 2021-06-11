using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Services.Interface;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Classes
{
    public class PlatformConfigurationService : IPlatformConfigurationService
    {
        private readonly IPlatformConfigurationRepository repository;
        private readonly IMapper mapper;

        public PlatformConfigurationService(IPlatformConfigurationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public List<PlatformConfigurationDTO> GetRoomAttributes()
        {
            List<PlatformConfiguration> result = repository.GetRoomAttributes();

            List<PlatformConfigurationDTO> response = mapper.Map<List<PlatformConfigurationDTO>>(result);

            return response;
        }
        public List<PlatformConfigurationDTO> GetBookingInvitationStatuses()
        {
            List<PlatformConfiguration> result = repository.GetBookingInvitationStatuses();

            List<PlatformConfigurationDTO> response = mapper.Map<List<PlatformConfigurationDTO>>(result);

            return response;
        }
        public List<PlatformConfigurationDTO> RecurrenceType()
        {
            List<PlatformConfiguration> result = repository.GetRecurrenceType();

            List<PlatformConfigurationDTO> response = mapper.Map<List<PlatformConfigurationDTO>>(result);

            return response;
        }

        public List<PlatformConfigurationDTO> GetWeekday()
        {
            List<PlatformConfiguration> result = repository.GetWeekday();

            List<PlatformConfigurationDTO> response = mapper.Map<List<PlatformConfigurationDTO>>(result);

            return response;
        }
    }
}
