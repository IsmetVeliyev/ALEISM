using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalStudentProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalStudentProject.DataAccess.Configurations;

namespace PersonalStudentProject.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<SubRoom> SubRooms { get; set; }
        public DbSet<UserSubRoom> UserSubRooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageSub> SubMessages {get; set;}

        public DbSet<DirectMessage> DirectMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new SubRoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserSubRoomConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


    }
}
