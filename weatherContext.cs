using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using server.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using server.Enteties;
using Microsoft.Extensions.Configuration;

namespace server
{
    public partial class weatherContext : IdentityDbContext<User>
    {
        

        public weatherContext(DbContextOptions<weatherContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Meterings> Meterings { get; set; }
        public virtual DbSet<MeteringTypes> MeteringTypes { get; set; }
        public virtual DbSet<Sensors> Sensors { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserSensors> UserSensors { get; set; }
        public virtual DbSet<UserDevices> UserDevices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if (!optionsBuilder.IsConfigured)
            // {
            //     optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            // }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserDevices>()
            .HasKey(o => new { o.UserId, o.DeviceId });
            modelBuilder.Entity<UserSensors>()
            .HasKey(o => new { o.UserId, o.SensorId });
        }
    }
}
