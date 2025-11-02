using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    [Table("DocumentGallery")]
    public class DocumentGallery
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<TopicGallery> Topics { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }


    }
}
