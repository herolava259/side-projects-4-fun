using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs
{
    public class QuestionDTO
    {
        
        public int Id { get; set; }

        
        public string Content { get; set; }


        
        public int AuthorId { get; set; }

        public DateTime CreatedDate { get; set; }

        

        
    }
}
