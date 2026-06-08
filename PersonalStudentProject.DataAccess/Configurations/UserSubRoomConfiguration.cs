using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalStudentProject.DataAccess.Models;


namespace PersonalStudentProject.DataAccess.Configurations
{
    public class UserSubRoomConfiguration : IEntityTypeConfiguration<UserSubRoom>
    {
        public void Configure(EntityTypeBuilder<UserSubRoom> builder)
        {
            builder.ToTable("UserSubRooms");
            builder.HasKey(usr => new { usr.UserId, usr.SubRoomId });

            builder.HasOne(usr => usr.User)
                   .WithMany(u => u.UserSubRooms)
                   .HasForeignKey(usr => usr.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(usr => usr.SubRoom)
                   .WithMany(sr => sr.UserSubRooms)
                   .HasForeignKey(usr => usr.SubRoomId).OnDelete(DeleteBehavior.Cascade);
        }

        
        
    }
}