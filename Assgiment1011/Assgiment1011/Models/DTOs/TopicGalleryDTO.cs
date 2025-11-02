using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs
{
    public class TopicGalleryDTO
    {
        
        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; }

        

        
        public int DocumentGalleryId { get; set; }

       
    }
}
