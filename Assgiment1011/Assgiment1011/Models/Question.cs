using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(4096)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
