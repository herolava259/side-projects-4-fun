using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace Assgiment1011.Data.FluentConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(t => t.Email)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(t => t.Password)
                   .IsRequired();
            builder.Property(t => t.Phone)
                   .IsRequired()
                   .HasMaxLength(11);

            builder.Property(t => t.Address)
                   .IsRequired();

            builder.HasOne(t => t.Author)
                   .WithOne(t => t.User)
                   .HasForeignKey<User>("AuthorId")
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
