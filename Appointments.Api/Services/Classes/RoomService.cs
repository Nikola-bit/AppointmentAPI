using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Security;
using Appointments.Api.Services.Interface;
using Appointments.Api.Utilities;
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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository repository;
        private readonly IMapper mapper;

        public RoomService(IRoomRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public PagedResponse<List<RoomDTO>> GetAllRooms(PaginationFilter filter)
        {
            PagedResponse<List<RoomDTO>> rooms = mapper.Map<PagedResponse<List<RoomDTO>>>(repository.AllRooms(filter));
            return rooms;
        }
        public RoomDTO RoomById(string id)
        {
            RoomDTO result = mapper.Map<RoomDTO>(repository.RoomById(id));
            return result;
        }
        public RoomDTO CreateRoom(RoomCreateDTO room)
        {
            Room rooms = mapper.Map<Room>(room);
            RoomDTO result = mapper.Map<RoomDTO>(repository.AddRoom(rooms));
            return result;
        }
        public bool RemoveRoom(string id)
        {
            bool results = repository.DeleteRoom(id);
            return results;
        }
        public bool UpdateRoom(RoomDTO id)
        {
            bool update = repository.UpdateRoom(id);
            return update;
        }
        public RoomAndAttributeDTO RoomByIdAttribute(string id)
        {
            return repository.RoomByIdAttribute(id);
        }
        public List<RoomAndAttributeDTO> FindRoomByCapacity(int capacity)
        {
            List<Room> result = repository.FreeRoomsByCapacity(capacity);

            List<RoomAndAttributeDTO> response = mapper.Map<List<RoomAndAttributeDTO>>(result);

            foreach (RoomAndAttributeDTO room in response)
            {
                room.Atrributes = mapper.Map<List<AttributeDTO>>(repository.AttributeFinder(Convert.ToInt32(EncryptionHelper.Decrypt(room.RoomId))));
            }

            return response;
        }
        public List<RoomAndAttributeDTO> FindRoomByAttribute(string attributeId)
        {
            List<Room> result = repository.FreeRoomsByAttribute(Convert.ToInt32(DataEncryption.Decrypt(attributeId)));

            List<RoomAndAttributeDTO> response = mapper.Map<List<RoomAndAttributeDTO>>(result);

            foreach (RoomAndAttributeDTO room in response)
            {
                room.Atrributes = mapper.Map<List<AttributeDTO>>(repository.AttributeFinder(Convert.ToInt32(EncryptionHelper.Decrypt(room.RoomId))));
            }

            return response;
        }
        public List<RoomAndAttributeDTO> FindByFilter(FilterDTO info)
        {
            List<Room> result = repository.FindByFilter(info);

            List<RoomAndAttributeDTO> response = mapper.Map<List<RoomAndAttributeDTO>>(result);

            foreach (RoomAndAttributeDTO room in response)
            {
                room.Atrributes = mapper.Map<List<AttributeDTO>>(repository.AttributeFinder(Convert.ToInt32(EncryptionHelper.Decrypt(room.RoomId))));
            }

            return response;
        }

    }
}

