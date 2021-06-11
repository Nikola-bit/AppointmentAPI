using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentsDTO.DTO.Common
{
    class CommonEntity
    {
    }

    public enum BOOKING_INVITATION_STATUS
    {
        Pending = 1,
        Accepted = 2,
        Declined = 3,
        Tentative = 4
    }
    public enum ROOM_ATTRIBUTE
    {
        Projector = 10,
        TV = 11,
        Whiteboard = 12
    }
    public enum RECURRENCE_TYPE
    {
        Daily = 101,
        Weekly = 102,
        Monthly = 103,
        Yearly = 104,
        Hourly = 105,
        Minutly = 106
    }
    public enum WEEKDAY
    {
        Monday = 111,
        Tuesday = 112,
        Wednesday = 113,
        Thursday = 114,
        Friday = 115,
        Saturday = 116,
        Sunday = 117
    }
}
