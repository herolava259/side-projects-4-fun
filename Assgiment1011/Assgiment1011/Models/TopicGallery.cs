using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    public class TopicGallery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }

        public ICollection<Document> Documents { get; set; }

        [ForeignKey("DocumentGallery")]
        public int DocumentGalleryId { get; set; }

        public DocumentGallery DocumentGallery { get; set; }

    }
}
