using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SignalRDemo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SignalRDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }

        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Participant>(entity => {
                entity.ToTable("Participants");

                entity.HasOne(p => p.User)
                    .WithMany(d => d.Participants)
                    .HasForeignKey(p => p.UserId);
                entity.HasOne(p => p.Room)
                    .WithMany(d => d.Participants)
                    .HasForeignKey(p => p.RoomId);
            });


            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
