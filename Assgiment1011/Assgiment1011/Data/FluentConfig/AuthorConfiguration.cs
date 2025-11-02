using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(x => x.Id);

            builder.Property(z => z.Name).IsRequired()
                                         .HasMaxLength(100);

            builder.Property(z => z.NickName).HasMaxLength(100)
                                             .HasColumnName("AdditionalName");

            builder.Property(z => z.Major).HasDefaultValue("Software Engineer").IsRequired(false);

            builder.HasData(new
            {
                Id = 1, 
                Name = "Admin",
                NickName = "Adsminstrator",
                Major = "SystemAdmin"
            });
        }
    }
}
