using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public abstract class AEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(z => z.State)
                   .IsRequired()
                   .HasConversion(v => v.ToString(),
                                  v => (Models.EntityState)Enum.Parse(typeof(Models.EntityState), v));

            /*builder.HasOne(b => b.Author).WithMany(b => b.Entities.Select(a => a as T)).HasForeignKey(b => b.AuthorId);*/
        }
    }
}
