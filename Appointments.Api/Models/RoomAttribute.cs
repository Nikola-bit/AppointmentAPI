using System;
using System.Collections.Generic;

namespace Appointments.Api.Models
{
    public partial class RoomAttribute
    {
        public int RoomAttributeId { get; set; }
        public int RoomId { get; set; }
        public int AttributeId { get; set; }

        public virtual PlatformConfiguration Attribute { get; set; }
        public virtual Room Room { get; set; }
    }
}
