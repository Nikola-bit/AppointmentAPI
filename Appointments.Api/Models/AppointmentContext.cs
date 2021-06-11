using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Appointments.Api.Models
{
    public partial class AppointmentContext : DbContext
    {
        public AppointmentContext()
        {
        }

        public AppointmentContext(DbContextOptions<AppointmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingParticipants> BookingParticipants { get; set; }
        public virtual DbSet<BookingRecurrence> BookingRecurrence { get; set; }
        public virtual DbSet<BookingRecurrenceDays> BookingRecurrenceDays { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<PlatformConfiguration> PlatformConfiguration { get; set; }
        public virtual DbSet<Reminder> Reminder { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomAttribute> RoomAttribute { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=Appointments; Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.EndingDateTime).HasColumnType("datetime");

                entity.Property(e => e.StartingDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Booking");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlannedBy_Booking");
            });

            modelBuilder.Entity<BookingParticipants>(entity =>
            {
                entity.HasKey(e => e.ParticipantId)
                    .HasName("PK_Participant");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingParticipants)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Participant");

                entity.HasOne(d => d.InvitationStatusNavigation)
                    .WithMany(p => p.BookingParticipants)
                    .HasForeignKey(d => d.InvitationStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Value_InvitationStatus");
            });

            modelBuilder.Entity<BookingRecurrence>(entity =>
            {
                entity.HasKey(e => e.RecurrenceId);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.BookingRecurrence)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingRecurrence_Room");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.BookingRecurrence)
                    .HasForeignKey(d => d.Type)
                    .HasConstraintName("FK_BookingRecurrence_PlatformConfiguration");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookingRecurrence)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingRecurrence_User");
            });

            modelBuilder.Entity<BookingRecurrenceDays>(entity =>
            {
                entity.HasOne(d => d.Recurrence)
                    .WithMany(p => p.BookingRecurrenceDays)
                    .HasForeignKey(d => d.RecurrenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingRecurrenceDays_BookingRecurrence");

                entity.HasOne(d => d.WeekdayNavigation)
                    .WithMany(p => p.BookingRecurrenceDays)
                    .HasForeignKey(d => d.Weekday)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingRecurrenceDays_PlatformConfiguration");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Location");
            });

            modelBuilder.Entity<PlatformConfiguration>(entity =>
            {
                entity.HasKey(e => e.Value);

                entity.Property(e => e.Value).ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ProgramCode)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ReminderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Reminder)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_Reminder_Booking");

                entity.HasOne(d => d.BookingRecurrence)
                    .WithMany(p => p.Reminder)
                    .HasForeignKey(d => d.BookingRecurrenceId)
                    .HasConstraintName("FK_Reminder_BookingRecurrence");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Reminder)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reminder_Type");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.IsUsable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Room");
            });

            modelBuilder.Entity<RoomAttribute>(entity =>
            {
                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.RoomAttribute)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomAttribute_PlatformConfiguration");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomAttribute)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_RoomAttribute");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
