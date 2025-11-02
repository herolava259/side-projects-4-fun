using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class DocumentGalleryConfiguration : IEntityTypeConfiguration<DocumentGallery>
    {
        public void Configure(EntityTypeBuilder<DocumentGallery> builder)
        {
            builder.ToTable("DocumentGallries");

            builder.HasKey(e => e.Id);

            builder.Property(c => c.Title)
                   .IsRequired()
                   .HasMaxLength(256)
                   ;
            builder.Property(c => c.Description)
                   .HasMaxLength(4096);

            builder.Property(c => c.CreatedDate)
                   .IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(b => b.DocumentGalleries)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasForeignKey(b => b.AuthorId)
                   ;


        }
    }
}
