using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs.Updated
{
    public class NewUpdateDTO
    {
        
        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; }
       
       
        public string ImageUrl { get; set; }

        
        public int AuthorId { get; set; }

        
    }
}
