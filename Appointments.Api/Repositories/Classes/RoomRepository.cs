using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Security;
using Appointments.Api.Utilities;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.DTO.Common;
using AppointmentsDTO.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Classes
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IMapper mapper;
        public RoomRepository(IMapper _mapper)
        {

            mapper = _mapper;
        }
        public PagedResponse<List<RoomDTO>> AllRooms(PaginationFilter filter)
        {
            using (var db = new AppointmentContext())
            {
                List<Room> list = db.Room
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToList();
                var rooms = mapper.Map<List<RoomDTO>>(list);
                int totalCount = db.Room.Count();
                PagedResponse<List<RoomDTO>> response = new PagedResponse<List<RoomDTO>>()
                {
                    Data = rooms,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalRecords = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
                };
                return response;
            }
        }
        public Room RoomById(string id)
        {
            using (var db = new AppointmentContext())
            {
                int romId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                Room room = db.Room.Where(s => s.RoomId == romId).FirstOrDefault();

                if (room != null)
                {
                    Room result = mapper.Map<Room>(room);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public Room AddRoom(Room newRoom)
        {
            using (var db = new AppointmentContext())
            {
                db.Room.Add(newRoom);
                db.SaveChanges();
                return newRoom;
            }
        }
        public bool DeleteRoom(string id)
        {
            using (var db = new AppointmentContext())
            {
                int rId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                Room room = db.Room.Where(s => s.RoomId == rId).FirstOrDefault();
                if (room != null)
                {
                    db.Room.Remove(room);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UpdateRoom(RoomDTO request)
        {
            using (var db = new AppointmentContext())
            {
                int romId = Convert.ToInt32(EncryptionHelper.Decrypt(request.RoomId));

                Room updateRoom = db.Room.Where(s => s.RoomId == romId).FirstOrDefault();
                if (updateRoom != null)
                {
                    updateRoom.Name = request.Name;
                    updateRoom.LocationId = Convert.ToInt32(EncryptionHelper.Decrypt(request.LocationId));
                    updateRoom.Capacity = request.Capacity;
                    updateRoom.IsUsable = request.IsUsable;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
        public RoomAndAttributeDTO RoomByIdAttribute(string id)
        {
            using(var db = new AppointmentContext())
            {
                Room result = db.Room.Where(r => r.RoomId == Convert.ToInt32(DataEncryption.Decrypt(id))).FirstOrDefault();

                if (result != null)
                {
                    List<RoomAttribute> helper = db.RoomAttribute.Where(r => r.RoomId == Convert.ToInt32(DataEncryption.Decrypt(id)))
                        .Include(p => p.Attribute).ToList();

                    List<AttributeDTO> result2 = mapper.Map<List<AttributeDTO>>(helper);

                    RoomAndAttributeDTO response = mapper.Map<RoomAndAttributeDTO>(result);

                    response.Atrributes = result2;

                    return response;
                }
                else return null;
            }
        }
        public List<Room> FreeRoomsByCapacity(int capacity)
        {
            using (var db = new AppointmentContext())
            {
                List<Room> rooms = db.Room.Where(r => r.Capacity >= capacity).Include(u => u.Location).ToList();

                return rooms;
            }
        }
        public List<Room> FreeRoomsByAttribute(int attributeId)
        {
            using (var db = new AppointmentContext())
            {
                List<RoomAttribute> roomsAttributes = db.RoomAttribute.Where(r => r.AttributeId == attributeId)
                    .Include(r => r.Room).ToList();

                List<Room> rooms = mapper.Map<List<Room>>(roomsAttributes);

                return rooms;
            }
        }
        public List<RoomAttribute> AttributeFinder(int roomId)
        {
            using (var db = new AppointmentContext())
            {
                List<RoomAttribute> helper = db.RoomAttribute.Where(r => r.RoomId == roomId)
                    .Include(p => p.Attribute).ToList();

                return helper;
            }
        }
        public List<Room> FindByFilter(FilterDTO info)
        {
            using(var db = new AppointmentContext())
            {
                List<Room> response = new List<Room>();
                if (info.AttributeIds != null)
                {
                    List<RoomAttribute> list = new List<RoomAttribute>();
                    List<RoomAttribute> helper = new List<RoomAttribute>();
                    foreach (string attribute in info.AttributeIds)
                    {
                        if (attribute != "string" && !string.IsNullOrEmpty(attribute))
                        {
                            helper = db.RoomAttribute.Where(r => r.AttributeId == Convert.ToInt32(DataEncryption.Decrypt(attribute)))
                                .Include(r => r.Room).ToList();
                        }
                        list.AddRange(helper);
                    }
                    foreach (RoomAttribute room in list)
                    {
                        int i = list.Where(r => r.RoomId == room.RoomId).Count();
                        if (i == info.AttributeIds.Count)
                        {
                            response.AddRange(db.Room.Where(r => r.RoomId == room.RoomId).Include(l => l.Location).ToList());
                            list = list.Where(r => r.RoomId != room.RoomId).ToList();
                        }
                    }
                }
                if(info.LocationId != "string" && !string.IsNullOrEmpty(info.LocationId))
                {
                    if(response.Count == 0)
                    {
                        response = db.Room.Where(x => x.LocationId == Convert.ToInt32(DataEncryption.Decrypt(info.LocationId))).Include(l => l.Location).ToList();
                    }
                    else response = response.Where(x => x.LocationId == Convert.ToInt32(DataEncryption.Decrypt(info.LocationId))).ToList();
                }
                if (info.CityId != "string" && !string.IsNullOrEmpty(info.CityId))
                {
                    if (response.Count == 0)
                    {
                        response = db.Room.Where(x => x.Location.CityId == Convert.ToInt32(DataEncryption.Decrypt(info.CityId))).ToList();
                    }
                    else response = response.Where(x => x.Location.CityId == Convert.ToInt32(DataEncryption.Decrypt(info.CityId))).ToList();
                }
                if (info.Capacity != null)
                {
                    if (response.Count == 0)
                    {
                        response = db.Room.Where(x => x.Capacity >= info.Capacity).ToList();
                    }
                    else response = response.Where(x => x.Capacity >= info.Capacity).ToList();
                }
                if(response.Count == 0)
                {
                    response = db.Room.ToList();
                }
                return response;
            }
        }
    }
}
