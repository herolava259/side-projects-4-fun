

using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");

            builder.HasKey(x => x.Id);

            builder.Property(b => b.Content)
                   .IsRequired()
                   .HasMaxLength(4096);

            builder.Property(b => b.CreatedDate).IsRequired();

            builder.HasOne(c => c.Author)
                   .WithMany(c => c.Questions)
                   .HasForeignKey(c => c.AuthorId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            

        }
    }
}
