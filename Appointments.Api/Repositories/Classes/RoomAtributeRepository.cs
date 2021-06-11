using Appointments.Api.Models;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Security;
using Appointments.Api.Wrappers;
using AppointmentsDTO.DTO;
using AppointmentsDTO.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories.Classes
{
    public class RoomAtributeRepository : IRoomAtributeRepository
    {
        private readonly IMapper mapper;
        public RoomAtributeRepository(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public PagedResponse<List<RoomAtributeDTO>> AllRoomAtribute(PaginationFilter filter)
        {
            using (var db = new AppointmentContext())
            {
                List<RoomAttribute> list = db.RoomAttribute
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToList();
                var roomA = mapper.Map<List<RoomAtributeDTO>>(list);
                int totalCount = db.RoomAttribute.Count();
                PagedResponse<List<RoomAtributeDTO>> response = new PagedResponse<List<RoomAtributeDTO>>()
                {
                    Data = roomA,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalRecords = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (decimal)filter.PageSize)
                };
                return response;
            }
        }
        public RoomAttribute ById(string id)
        {
            using (var db = new AppointmentContext())
            {
                int romaId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                RoomAttribute room = db.RoomAttribute.Where(s => s.RoomAttributeId == romaId).FirstOrDefault();
                if (room != null)
                {
                    RoomAttribute result = mapper.Map<RoomAttribute>(room);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public RoomAttribute AddRoomAttribute(RoomAttribute newRoomA)
        {
            using (var db = new AppointmentContext())
            {
                db.RoomAttribute.Add(newRoomA);
                db.SaveChanges();
                return newRoomA;
            }
        }
        public bool DeleteAtribute(string id)
        {
            using (var db = new AppointmentContext())
            {
                int raId = Convert.ToInt32(EncryptionHelper.Decrypt(id));
                RoomAttribute rooma = db.RoomAttribute.Where(s => s.RoomAttributeId == raId).FirstOrDefault();
                if (rooma != null)
                {
                    db.RoomAttribute.Remove(rooma);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UpdateAtribute(RoomAtributeDTO request)
        {
            using (var db = new AppointmentContext())
            {
                int romaId = Convert.ToInt32(EncryptionHelper.Decrypt(request.RoomAttributeId));

                RoomAttribute updateRoomA = db.RoomAttribute.Where(s => s.RoomAttributeId == romaId).FirstOrDefault();
                if (updateRoomA != null)
                {
                    updateRoomA.RoomId = Convert.ToInt32(EncryptionHelper.Decrypt(request.RoomId));
                    updateRoomA.AttributeId = Convert.ToInt32(EncryptionHelper.Decrypt(request.AttributeId));
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
