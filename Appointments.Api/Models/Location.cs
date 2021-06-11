using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class Location
    {
        public Location()
        {
            Room = new HashSet<Room>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Room> Room { get; set; }
    }
}
