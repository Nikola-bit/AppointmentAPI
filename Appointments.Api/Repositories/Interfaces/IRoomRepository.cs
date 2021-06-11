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
   public interface IRoomRepository
    {
        public PagedResponse<List<RoomDTO>> AllRooms(PaginationFilter filters);

        public Room RoomById(string id);

        public Room AddRoom(Room city);
        public bool DeleteRoom(string id);
        bool UpdateRoom(RoomDTO request);
        public RoomAndAttributeDTO RoomByIdAttribute(string id);
        public List<Room> FreeRoomsByCapacity(int capacity);
        public List<Room> FreeRoomsByAttribute(int attributeId);
        public List<RoomAttribute> AttributeFinder(int roomId);
        public List<Room> FindByFilter(FilterDTO info);
    }
}
