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
    public class RoomAtributeService : IRoomAtributeService
    {
        private readonly IRoomAtributeRepository repository;
        private readonly IMapper mapper;

        public RoomAtributeService(IRoomAtributeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public PagedResponse<List<RoomAtributeDTO>> GetAllRoomsAtribute(PaginationFilter filter)
        {
            PagedResponse<List<RoomAtributeDTO>> roomsA = mapper.Map<PagedResponse<List<RoomAtributeDTO>>>(repository.AllRoomAtribute(filter));
            return roomsA;
        }
        public RoomAtributeDTO RoomAtributeById(string id)
        {
            RoomAtributeDTO result = mapper.Map<RoomAtributeDTO>(repository.ById(id));
            return result;
        }
        public RoomAtributeDTO CreateRoomAtribute(RoomAtributeCreateDTO roomA)
        {
            RoomAttribute rooms = mapper.Map<RoomAttribute>(roomA);
            RoomAtributeDTO result = mapper.Map<RoomAtributeDTO>(repository.AddRoomAttribute(rooms));
            return result;
        }
        public bool RemoveRoomAtribute(string id)
        {
            bool results = repository.DeleteAtribute(id);
            return results;
        }
        public bool UpdateRoomAtribute(RoomAtributeDTO id)
        {
            bool update = repository.UpdateAtribute(id);
            return update;
        }
    }
}
