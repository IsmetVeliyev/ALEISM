using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(200);


            builder.Property(e => e.Role)
           .IsRequired() 
           .HasMaxLength(50); 

            builder.Property(e => e.Age); 

            builder.Property(e => e.Location)
                   .HasMaxLength(200);

            builder.Property(e => e.Password)
                   .IsRequired()
                   .HasMaxLength(200);



        }

    }

}




