using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class NewConfiguration : IEntityTypeConfiguration<New>
    {
        public void Configure(EntityTypeBuilder<New> builder)
        {
            builder.ToTable("News");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                   .IsRequired(false)
                   .HasMaxLength(4096);

            builder.Property(x => x.CreatedDate)
                   .IsRequired();

            builder.Property(x => x.ImageUrl)
                   .IsRequired();

            builder.HasOne(c => c.Author)
                    .WithMany(c => c.News)
                    .HasForeignKey(c => c.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
