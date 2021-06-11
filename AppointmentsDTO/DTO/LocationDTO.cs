using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
   public class LocationCreateDTO
    {
        public string Name { get; set; }
        public string CityId { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class LocationDTO : LocationCreateDTO
    {
        public string LocationId { get; set; }
    }
}
