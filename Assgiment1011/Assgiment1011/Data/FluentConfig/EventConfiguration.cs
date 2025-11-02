using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(z => z.Description).IsRequired(false).HasMaxLength(4096);

            builder.Property(z => z.BeginDate).IsRequired();
            builder.Property(z => z.EndDate).IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(b => b.Events)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
