using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Videos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                   .IsRequired(false)
                   .HasMaxLength(4096);

            builder.Property(x => x.Url)
                   .IsRequired()
                   ;

            builder.Property(x => x.CreatedDate).IsRequired();

            builder.Property(x => x.ImageUrl) .IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(b => b.Videos)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
