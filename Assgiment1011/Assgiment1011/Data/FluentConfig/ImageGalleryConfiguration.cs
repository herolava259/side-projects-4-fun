using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class ImageGalleryConfiguration : IEntityTypeConfiguration<ImageGallery>
    {
        public void Configure(EntityTypeBuilder<ImageGallery> builder)
        {
            builder.ToTable("ImageGalleries");

            builder.HasKey(x => x.Id);

            builder.Property<string>("Description")
                   .HasMaxLength(4096);

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(255)
                   ;

            builder.Property(b => b.CreatedDate)
                   .IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(b => b.ImageGalleries)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
