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
   public interface IRoomAtributeRepository
    {
        public PagedResponse<List<RoomAtributeDTO>> AllRoomAtribute(PaginationFilter filters);

        public RoomAttribute ById(string id);

        public RoomAttribute AddRoomAttribute(RoomAttribute atribute);
        public bool DeleteAtribute(string id);
        bool UpdateAtribute(RoomAtributeDTO request);
    }
}
