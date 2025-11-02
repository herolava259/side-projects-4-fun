using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class TopicGalleryConfiguration : IEntityTypeConfiguration<TopicGallery>
    {
        public void Configure(EntityTypeBuilder<TopicGallery> builder)
        {
            builder.ToTable("TopicGalleries");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Description)
                   .IsRequired(false);

            builder.HasOne(b => b.DocumentGallery)
                   .WithMany(b => b.Topics)
                   .HasForeignKey(c => c.DocumentGalleryId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
