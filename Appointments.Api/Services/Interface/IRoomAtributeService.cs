using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Interface
{
    public interface IRoomAtributeService
    {
        public PagedResponse<List<RoomAtributeDTO>> GetAllRoomsAtribute(PaginationFilter filters);

        public RoomAtributeDTO RoomAtributeById(string id);

        public RoomAtributeDTO CreateRoomAtribute(RoomAtributeCreateDTO atribute);
        public bool RemoveRoomAtribute(string id);
        bool UpdateRoomAtribute(RoomAtributeDTO request);
    }
}
