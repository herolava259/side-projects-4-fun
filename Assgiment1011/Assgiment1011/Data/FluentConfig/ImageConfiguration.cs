using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                   .IsRequired(false)
                   .HasMaxLength(4096);

            builder.Property(c => c.Url)
                   .IsRequired()
                   ;

            builder.Property(c => c.CreatedDate).IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(b => b.Images)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(b => b.ImageGallery)
                   .WithMany(b => b.Images)
                   .HasForeignKey(b => b.ImageGalleryId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
