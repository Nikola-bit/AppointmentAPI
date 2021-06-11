using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
   public class RoomAtributeCreateDTO
    {
        public string RoomId { get; set; }
        public string AttributeId { get; set; }
    }
    public class RoomAtributeDTO : RoomAtributeCreateDTO
    {
        public string RoomAttributeId { get; set; }
    }
}
