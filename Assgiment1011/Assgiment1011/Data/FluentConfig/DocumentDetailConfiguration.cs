using Assgiment1011.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class DocumentDetailConfiguration : IEntityTypeConfiguration<DocumentDetail>
    {
        public void Configure(EntityTypeBuilder<DocumentDetail> builder)
        {
            builder.ToTable("DocumentDetails");

            builder.HasKey(x => x.Id);

            
            builder.HasOne(x => x.Document)
                   .WithOne(x => x.DocumentDetail)
                   .HasForeignKey<DocumentDetail>(x => x.DocumentId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasConversion(v => v.ToString(),
                                  v => (DocumentType)Enum.Parse(typeof(DocumentType), v))
                   .HasDefaultValue(DocumentType.Word);
        }
    }
}
