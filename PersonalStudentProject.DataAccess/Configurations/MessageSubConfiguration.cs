using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalStudentProject.DataAccess.Models;

namespace PersonalStudentProject.DataAccess.Configurations
{
    public class MessageSubConfiguration : IEntityTypeConfiguration<MessageSub>
    {
        public void Configure(EntityTypeBuilder<MessageSub> builder)
        {
            builder.ToTable("SubMessages");

            builder.HasKey(e => e.id);
            builder.Property(e => e.id).ValueGeneratedOnAdd();

            builder.Property(e => e.subRoomId)
                   .IsRequired();

            builder.Property(e => e.senderEmail)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.content)
                   .IsRequired();

            builder.Property(e => e.DateSent)
                   .IsRequired();
        }
    }
}