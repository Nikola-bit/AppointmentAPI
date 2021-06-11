using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO
{
   public class CityCreateDTO
    {
        public string Name { get; set; }

    }
    public class CityDTO : CityCreateDTO
    {
        public string CityId { get; set; }
    }
}
