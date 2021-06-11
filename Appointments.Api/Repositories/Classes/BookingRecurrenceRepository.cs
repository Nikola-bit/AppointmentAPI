using Appointments.Api.Models;
using Appointments.Api.Utilities;
using AppointmentsDTO;
using AppointmentsDTO.DTO.Common;
using AppointmentsDTO.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Appointments.Api.Repositories
{
    public class BookingRecurrenceRepository : IBookingRecurrenceRepository
    {
        public IMapper mapper;
        public BookingRecurrenceRepository(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public bool CheckAvailability(int RoomId, DateTime startingDate, DateTime endingDate)
        {
            using (var db = new AppointmentContext())
            {
                var startingTime = startingDate.TimeOfDay;
                var endingTime = endingDate.TimeOfDay;

                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@StartDate", startingDate);
                parameters[1] = new SqlParameter("@EndDate", endingDate);
                parameters[2] = new SqlParameter("@StartTime", startingTime);
                parameters[3] = new SqlParameter("@EndTime", endingTime);
                parameters[4] = new SqlParameter("@RoomId", RoomId);

                DataSet dataSet = CommonFunction.ExecSp("dbo.CheckAvailability", parameters);

                List<Booking> bookings = CommonFunction.DatatableToList<Booking>(dataSet.Tables[0]).ToList();
                if (bookings != null)
                {
                    List<BookingRecurrence> bookingRecurrences = CommonFunction.DatatableToList<BookingRecurrence>(dataSet.Tables[1]).ToList();
                    if (bookingRecurrences != null)
                    {
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else return false;
            }
        }

        public BookingRecurrence CreateBookingRecurrenceByDate(BookingRecurrenceCreateDTO info)
        {
            using (var db = new AppointmentContext())
            {
                DateTime date = info.StartDate;
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Daily))
                {
                    if (info.CustomDay == true)//Every X days
                    {
                        bool created = true;
                        while (info.StartDate <= info.EndDate)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                            {
                                Booking booking = new Booking()
                                {
                                    UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                    RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                    StartingDateTime = meetingStart,
                                    EndingDateTime = meetingEnd,
                                };

                                db.Booking.Add(booking);
                                db.SaveChanges();

                                info.StartDate = info.StartDate.AddDays(Convert.ToInt32(info.Value));


                            }
                            else
                            {
                                info.StartDate = info.StartDate = info.StartDate.AddYears(10);
                                created = false;
                            }
                        }
                        if (created == true)
                        {
                            info.StartDate = date;
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(info);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();

                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;
                        }
                        else return null;
                    }
                    else
                    {
                        bool created = true;
                        while (info.StartDate <= info.EndDate)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (Convert.ToString(meetingStart.DayOfWeek) == "Sunday" || Convert.ToString(meetingStart.DayOfWeek) == "Saturday")
                            {
                                info.StartDate = info.StartDate.AddDays(1);
                            }
                            else
                            {
                                if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                                {
                                    Booking booking = new Booking()
                                    {
                                        UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                        RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                        StartingDateTime = meetingStart,
                                        EndingDateTime = meetingEnd,
                                    };

                                    db.Booking.Add(booking);
                                    db.SaveChanges();

                                    info.StartDate = info.StartDate.AddDays(1);
                                }
                                else
                                {
                                    info.StartDate = info.StartDate.AddYears(10);
                                    created = false;
                                }
                            }
                        }
                        if (created == true)
                        {
                            info.StartDate = date;
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(info);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();


                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;

                        }
                        else return null;
                    }

                }
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Weekly))
                {
                    bool created = true;
                    foreach (BookingRecurrenceDaysCreate day in info.WeekDays)
                    {
                        info.StartDate = date;
                        PlatformConfiguration days = db.PlatformConfiguration.Where(b => b.Value == Convert.ToInt32(DataEncryption.Decrypt(day.Weekday))).FirstOrDefault();
                        while (Convert.ToString(info.StartDate.DayOfWeek) != days.Name)
                        {
                            info.StartDate = info.StartDate.AddDays(1);
                        }
                        while (info.StartDate <= info.EndDate)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                            {
                                Booking booking = new Booking()
                                {
                                    UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                    RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                    StartingDateTime = meetingStart,
                                    EndingDateTime = meetingEnd,
                                };

                                db.Booking.Add(booking);
                                db.SaveChanges();
                                info.StartDate = info.StartDate.AddDays(Convert.ToInt32(info.Value) * 7);
                            }
                            else
                            {
                                info.StartDate = info.StartDate = info.StartDate.AddYears(10);
                                created = false;
                            }
                        }
                        if (created == true)
                        {
                            info.StartDate = date;
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(info);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();

                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;
                        }

                        else return null;
                    }
                }
            }
            if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Monthly))
            {
                return null;
            }
            if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Yearly))
            {
                return null;
            }
            else return null;
        }
        public BookingRecurrence CreateBookingRecurrenceByDays(BookingRecurrenceCreateDTO info)
        {
            using (var db = new AppointmentContext())
            {
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Daily))
                {
                    BookingRecurrenceCreateDTO response = info;
                    if (info.CustomDay == true)//Every X days
                    {
                        bool created = true;
                        while (info.EndAfter > 0)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                            {
                                Booking booking = new Booking()
                                {
                                    UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                    RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                    StartingDateTime = meetingStart,
                                    EndingDateTime = meetingEnd,
                                };

                                db.Booking.Add(booking);
                                db.SaveChanges();

                                info.StartDate = info.StartDate.AddDays(Convert.ToInt32(info.Value));

                                info.EndAfter--;

                            }
                            else
                            {
                                info.EndAfter = 0;
                                created = false;
                            }
                        }
                        if (created == true)
                        {
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(response);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();


                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;

                        }
                        else return null;
                    }
                    else
                    {
                        bool created = true;
                        while (info.EndAfter > 0)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (Convert.ToString(meetingStart.DayOfWeek) == "Sunday" || Convert.ToString(meetingStart.DayOfWeek) == "Saturday")
                            {
                                info.StartDate = info.StartDate.AddDays(1);
                            }
                            else
                            {
                                if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                                {
                                    Booking booking = new Booking()
                                    {
                                        UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                        RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                        StartingDateTime = meetingStart,
                                        EndingDateTime = meetingEnd,
                                    };

                                    db.Booking.Add(booking);
                                    db.SaveChanges();

                                    info.StartDate = info.StartDate.AddDays(1);
                                    info.EndAfter--;
                                }
                                else
                                {
                                    info.EndAfter = 0;
                                    created = false;
                                }
                            }
                        }
                        if (created == true)
                        {
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(response);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();


                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;

                        }
                        else return null;
                    }

                }
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Weekly))
                {
                    DateTime date = info.StartDate;
                    bool created = true;
                    foreach (BookingRecurrenceDaysCreate day in info.WeekDays)
                    {
                        info.StartDate = date;
                        PlatformConfiguration days = db.PlatformConfiguration.Where(b => b.Value == Convert.ToInt32(DataEncryption.Decrypt(day.Weekday))).FirstOrDefault();
                        while (Convert.ToString(info.StartDate.DayOfWeek) != days.Name)
                        {
                            info.StartDate = info.StartDate.AddDays(1);
                        }
                        info.EndAfter = info.EndAfter / info.WeekDays.Count();
                        while (info.EndAfter > 0)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                            {
                                Booking booking = new Booking()
                                {
                                    UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                    RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                    StartingDateTime = meetingStart,
                                    EndingDateTime = meetingEnd,
                                };

                                db.Booking.Add(booking);
                                db.SaveChanges();
                                info.StartDate = info.StartDate.AddDays(Convert.ToInt32(info.Value) * 7);
                                info.EndAfter--;
                            }
                            else
                            {
                                info.EndAfter = 0;
                                created = false;
                            }
                        }
                        if (created == true)
                        {
                            info.StartDate = date;
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(info);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();

                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;
                        }

                        else return null;
                    }
                }
            }
            if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Monthly))
            {
                return null;
            }
            if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Yearly))
            {
                return null;
            }
            else return null;
        }

        public BookingRecurrence CreateBookingRecurrenceByNoDate(BookingRecurrenceCreateDTO info)
        {
            using (var db = new AppointmentContext())
            {
                BookingRecurrenceCreateDTO response = info;
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Daily))
                {
                    if (info.CustomDay == true)//Every X days
                    {
                        bool created = true;
                        DateTime oneWeek = info.StartDate.AddDays(7);
                        while (oneWeek >= info.StartDate)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                            {
                                Booking booking = new Booking()
                                {
                                    UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                    RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                    StartingDateTime = meetingStart,
                                    EndingDateTime = meetingEnd,
                                };

                                db.Booking.Add(booking);
                                db.SaveChanges();

                                info.StartDate = info.StartDate.AddDays(Convert.ToInt32(info.Value));

                            }
                            else
                            {
                                info.StartDate = info.StartDate.AddDays(8);
                                created = false;
                            }
                        }
                        if (created == true)
                        {
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(response);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();

                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;
                        }
                        else return null;
                    }
                    else
                    {
                        bool created = true;
                        DateTime oneWeek = info.StartDate.AddDays(7);
                        while (oneWeek >= info.StartDate)
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (Convert.ToString(meetingStart.DayOfWeek) == "Sunday" || Convert.ToString(meetingStart.DayOfWeek) == "Saturday")
                            {
                                info.StartDate = info.StartDate.AddDays(1);
                            }
                            else
                            {
                                if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                                {
                                    Booking booking = new Booking()
                                    {
                                        UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                        RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                        StartingDateTime = meetingStart,
                                        EndingDateTime = meetingEnd,
                                    };

                                    db.Booking.Add(booking);
                                    db.SaveChanges();

                                    info.StartDate = info.StartDate.AddDays(1);
                                }
                                else
                                {
                                    info.StartDate = info.StartDate.AddDays(8);
                                    created = false;
                                }
                            }
                        }
                        if (created == true)
                        {
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(response);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();

                            BookingRecurrence helper = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            int i = 111;
                            while (i < 116)
                            {
                                BookingRecurrenceDays recurrenceDays = new BookingRecurrenceDays()
                                {
                                    RecurrenceId = helper.RecurrenceId,
                                    Weekday = i
                                };
                                i++;
                                db.BookingRecurrenceDays.Add(recurrenceDays);
                                db.SaveChanges();

                            }
                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;
                        }
                        else return null;
                    }

                }
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Weekly))
                {
                    bool created = true;
                    DateTime date = info.StartDate.Date;
                    foreach (BookingRecurrenceDaysCreate day in info.WeekDays)
                    {
                        info.StartDate = date;
                        PlatformConfiguration days = db.PlatformConfiguration.Where(b => b.Value == Convert.ToInt32(DataEncryption.Decrypt(day.Weekday))).FirstOrDefault();
                        while (Convert.ToString(info.StartDate.DayOfWeek) != days.Name)
                        {
                            info.StartDate = info.StartDate.AddDays(1);
                        }
                        while (info.StartDate <= date.AddDays(7))
                        {
                            DateTime meetingStart = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.StartTime);
                            DateTime meetingEnd = DateTime.Parse(info.StartDate.ToString("dd-MM-yyyy") + " " + info.EndTime);

                            if (CheckAvailability(Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)), meetingStart, meetingEnd) == true)
                            {
                                Booking booking = new Booking()
                                {
                                    UserId = Convert.ToInt32(DataEncryption.Decrypt(info.UserId)),
                                    RoomId = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId)),
                                    StartingDateTime = meetingStart,
                                    EndingDateTime = meetingEnd,
                                };

                                db.Booking.Add(booking);
                                db.SaveChanges();
                                info.StartDate = info.StartDate.AddDays(Convert.ToInt32(info.Value) * 7);
                            }
                            else
                            {
                                info.StartDate = info.StartDate.AddYears(10);
                                created = false;
                            }
                        }
                        if (created == true)
                        {
                            info.StartDate = date;
                            BookingRecurrence recurrence = mapper.Map<BookingRecurrence>(info);

                            db.BookingRecurrence.Add(recurrence);
                            db.SaveChanges();

                            recurrence = db.BookingRecurrence.Where(b => b.StartDate == recurrence.StartDate).FirstOrDefault();

                            return recurrence;
                        }

                        else return null;
                    }
                }
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Monthly))
                {
                    return null;
                }
                if (Convert.ToInt32(DataEncryption.Decrypt(info.Type)) == Convert.ToInt32(RECURRENCE_TYPE.Yearly))
                {
                    return null;
                }
                else return null;
            }
        }
        public BookingRecurrence DeleteBookingRecurrence(int Id)
        {
            using (var db = new AppointmentContext())
            {
                BookingRecurrence recurrence = db.BookingRecurrence.Where(b => b.RecurrenceId == Id).FirstOrDefault();

                BookingRecurrence response = recurrence;

                db.BookingRecurrence.Remove(recurrence);
                db.SaveChanges();

                return response;
            }
        }
        public BookingRecurrence GetBookingRecurrence(int Id)
        {
            using (var db = new AppointmentContext())
            {
                BookingRecurrence recurrence = db.BookingRecurrence.Where(b => b.RecurrenceId == Id).FirstOrDefault();

                return recurrence;
            }
        }
        public List<BookingRecurrence> ListBookingRecurrences(RecurrencePaginationFilter info)
        {
            using (var db = new AppointmentContext())
            {
                List<BookingRecurrence> recurrences = db.BookingRecurrence.Include(u => u.TypeNavigation).ToList();

                if (info.RoomId != "string" && !string.IsNullOrEmpty(info.RoomId))
                {
                    int helper = Convert.ToInt32(DataEncryption.Decrypt(info.RoomId));

                    recurrences = recurrences.Where(b => b.RoomId == helper).ToList();
                }
                recurrences = recurrences.Skip((info.PageNumber - 1) * info.PageSize)
                .Take(info.PageSize)
                .ToList();

                return recurrences;
            }
        }
    }
}