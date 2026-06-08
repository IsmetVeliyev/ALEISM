using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalStudentProject.DataAccess.Models;
namespace PersonalStudentProject.DataAccess.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.userId)
                   .IsRequired(); 

            builder.Property(e => e.RoomName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.RoomType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.isPasswordProtected);
            builder.Property(e => e.Password)
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .HasMaxLength(500);      
            
            builder.Property(e => e.DateCreated)
                   .IsRequired();

            builder.Property(e => e.DateUpdated)
                   .IsRequired();

            builder.Property(e => e.ExpiryDate);
            builder.Property(e => e.Capacity)
                   .IsRequired();
            builder.Property(e => e.IsAvailable)
                   .IsRequired();

            builder.Property(e => e.Location)
                   .HasMaxLength(200); 

            builder.HasOne(r => r.Owner)
               .WithMany(u => u.Rooms)
               .HasForeignKey(r => r.userId).OnDelete(DeleteBehavior.Cascade);
    }
}
}