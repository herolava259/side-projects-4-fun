using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("Annoucements");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Title)
                   .IsRequired()
                   .HasMaxLength(256)
                   .HasColumnName("Title");

            builder.HasOne(b => b.Author)
                   .WithMany(b => b.Announcements)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(b => b.ImageUrl).IsRequired();
            builder.Property(b => b.Description).IsRequired(false);

            builder.Property(b => b.State)
                   .IsRequired()
                   .HasDefaultValue(Models.EntityState.Active)
                   .HasConversion(c => c.ToString(),
                                  c => (Models.EntityState)Enum.Parse(typeof(Models.EntityState), c));

            builder.HasIndex(a => new { a.Title, a.Slug })
                   .IsDescending(false, true)
                   .IsUnique(true)
                   ;

            builder.Ignore(b => b.AdditionalInfo);

        }
    }
}
