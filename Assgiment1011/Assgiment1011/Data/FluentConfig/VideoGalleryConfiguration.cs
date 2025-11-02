using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class VideoGalleryConfiguration : IEntityTypeConfiguration<VideoGallery>
    {
        public void Configure(EntityTypeBuilder<VideoGallery> builder)
        {
            builder.ToTable("VideoGalleries");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Description)
                   .IsRequired(false)
                   .HasMaxLength(4096);

            builder.Property(c => c.CreatedDate)
                   .IsRequired();

            builder.HasOne(c => c.Author)
                   .WithMany(c => c.VideoGalleries)
                   .HasForeignKey(c => c.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
