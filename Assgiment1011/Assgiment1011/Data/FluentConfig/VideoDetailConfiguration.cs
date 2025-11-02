using Assgiment1011.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class VideoDetailConfiguration : IEntityTypeConfiguration<VideoDetail>
    {
        public void Configure(EntityTypeBuilder<VideoDetail> builder)
        {
            builder.ToTable("VideoDetails");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.FormatType).HasColumnName("VideoType")
                   .IsRequired()
                   .HasConversion(v => v.ToString(),
                                  c => (VideoType)Enum.Parse(typeof(VideoType), c))
                   .HasDefaultValue(VideoType.mp4);
            builder.HasOne(c => c.Video)
                   .WithOne(c => c.VideoDetail)
                   .HasForeignKey<VideoDetail>(c => c.VideoId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
