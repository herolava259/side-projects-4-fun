using Assgiment1011.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assgiment1011.Data.FluentConfig
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answer");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Content).IsRequired().HasMaxLength(4096);
            builder.Property(b => b.CreatedDate).IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(b => b.Answers)
                   .HasForeignKey(b => b.AuthorId)
                   .HasConstraintName("FKManyAnswerWithOneAuthor")
                   .OnDelete(DeleteBehavior.ClientSetNull);


            builder.HasOne(c => c.Question)
                   .WithMany(c => c.Answers)
                   .HasForeignKey(c => c.QuestionId)
                   .HasConstraintName("FKOneQuestionHasManyAnswer")
                   .OnDelete(DeleteBehavior.ClientSetNull);

            
        }
    }
}
