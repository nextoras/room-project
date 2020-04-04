using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
// using server.Entities;

namespace server
{
    public partial class weatherContext : DbContext
    {
        public weatherContext()
        {
        }

        public weatherContext(DbContextOptions<weatherContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Seconds> Seconds { get; set; }
        public virtual DbSet<Minutes> Minutes { get; set; }
        public virtual DbSet<Hours> Hours { get; set; }
        public virtual DbSet<Days> Days { get; set; }
        public virtual DbSet<Weeks> Weeks { get; set; }
        public virtual DbSet<Mounths> Mounths { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatus { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Meterings> Meterings { get; set; }
        public virtual DbSet<MeteringTypes> MeteringTypes { get; set; }
        public virtual DbSet<Sensors> Sensors { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserSensors> UserSensors { get; set; }
        public virtual DbSet<UserDevices> UserDevices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=213.226.112.163;Database=weather;Username=postgres;Password=ser241199");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seconds>(entity =>
            {
                entity.ToTable("seconds");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.Temperature).HasColumnName("temperature");
            });
            modelBuilder.Entity<UserDevices>()
            .HasKey(o => new { o.UserId, o.DeviceId });
            modelBuilder.Entity<UserSensors>()
            .HasKey(o => new { o.UserId, o.SensorId });
        }
    }
}
