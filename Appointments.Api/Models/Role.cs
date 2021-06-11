using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
