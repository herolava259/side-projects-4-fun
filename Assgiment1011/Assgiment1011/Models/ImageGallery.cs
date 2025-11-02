using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    [Table("ImageGalleries")]
    public class ImageGallery
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
