using Assgiment1011.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class ImageDetailConfiguration : IEntityTypeConfiguration<ImageDetail>
    {
        public void Configure(EntityTypeBuilder<ImageDetail> builder)
        {
            builder.ToTable("ImageDetails");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.FormatType)
                   .HasColumnName("ImageType")
                   .IsRequired()
                   .HasConversion(v => v.ToString(),
                                  c => (ImageType)Enum.Parse(typeof(ImageType), c))
                   .HasDefaultValue(ImageType.png);

            builder.HasOne(c => c.Image)
                   .WithOne(c => c.ImageDetail)
                   .HasForeignKey<ImageDetail>(c => c.ImageId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
