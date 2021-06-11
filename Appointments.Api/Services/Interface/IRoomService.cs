using Appointments.Api.Models;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Services.Interface
{
    public interface IRoomService
    {
        public PagedResponse<List<RoomDTO>> GetAllRooms(PaginationFilter filters);

        public RoomDTO RoomById(string id);

        public RoomDTO CreateRoom(RoomCreateDTO room);
        public bool RemoveRoom(string id);
        bool UpdateRoom(RoomDTO request);
        public RoomAndAttributeDTO RoomByIdAttribute(string id);
        public List<RoomAndAttributeDTO> FindRoomByCapacity(int capacity);
        public List<RoomAndAttributeDTO> FindRoomByAttribute(string attributeId);
        public List<RoomAndAttributeDTO> FindByFilter(FilterDTO info);
    }
}
