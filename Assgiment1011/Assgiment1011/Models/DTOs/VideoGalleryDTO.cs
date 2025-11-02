using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs
{
    public class VideoGalleryDTO
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public string? Description { get; set; }

        
        public DateTime CreatedDate { get; set; }

        
        public int AuthorId { get; set; }

        

        
    }
}
