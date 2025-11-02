using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(z => z.Id);

            builder.Property(z => z.DocumentType)
                   .IsRequired()
                   .HasDefaultValue(DocumentType.Word)
                   .HasConversion(v => v.ToString(),
                                  v => (DocumentType)Enum.Parse(typeof(DocumentType), v))
                   ;

            builder.Property(z => z.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(b => b.CreatedDate).IsRequired();

            builder.HasOne(z => z.TopicGallery).WithMany(z => z.Documents)
                   .HasForeignKey(z => z.TopicId)
                   .HasConstraintName("FKManyDocumentWithOneTopicGallery")
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(z => z.Author)
                   .WithMany(z => z.Documents)
                   .HasForeignKey(z => z.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);



        }
    }
}
