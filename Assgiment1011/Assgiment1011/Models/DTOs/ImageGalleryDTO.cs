using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs
{
    public class ImageGalleryDTO
    {
        
        public int Id { get; set; }

        
        public string Description { get; set; }

        
        public string Name { get; set; }

        
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        
        public int AuthorId { get; set; }
        

    }
}
