using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
    public class RoomCreateDTO
    {
        public string LocationId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool? IsUsable { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class RoomDTO : RoomCreateDTO
    {
        public string RoomId { get; set; }
    }
    public class AttributeDTO
    {
        public string Name { get; set; }
    }
    public class RoomAndAttributeDTO : RoomDTO
    {
        public List<AttributeDTO> Atrributes { get; set; }
    }
    public class FilterDTO
    {
        public List<string> AttributeIds { get; set; }
        public string LocationId { get; set; }
        public string CityId { get; set; }
        public int Capacity { get; set; }
    }
}
