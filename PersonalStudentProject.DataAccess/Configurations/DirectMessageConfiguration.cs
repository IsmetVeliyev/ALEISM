using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.DataAccess.Configurations
{
    public class DirectMessageConfiguration : IEntityTypeConfiguration<DirectMessage>
    {
        public void Configure(EntityTypeBuilder<DirectMessage> builder)
        {
            builder.ToTable("DirectMessages");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.SenderId).IsRequired();

            builder.Property(e => e.SenderEmail)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.ReceiverId).IsRequired();

            builder.Property(e => e.Content).IsRequired();

            builder.Property(e => e.DateSent).IsRequired();
        }
    }
}
