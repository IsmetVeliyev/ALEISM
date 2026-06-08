using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.DataAccess.Configurations
{
    public class SubRoomConfiguration : IEntityTypeConfiguration<SubRoom>
    {
        public void Configure(EntityTypeBuilder<SubRoom> builder)
        {
            builder.ToTable("SubRooms");
            builder.HasKey(sr => sr.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(sr => sr.SubRoomName)
                   .IsRequired()
                   .HasMaxLength(100);
            
            builder.Property(sr => sr.RoomId)
                   .IsRequired();


            builder.HasOne(sr => sr.room)
                   .WithMany(r => r.SubRooms)
                   .HasForeignKey(sr => sr.RoomId).OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}