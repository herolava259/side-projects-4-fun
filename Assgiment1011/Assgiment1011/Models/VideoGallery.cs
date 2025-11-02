using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    public class VideoGallery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Video> Videos { get; set; }
    }
}
