using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs
{
    public class DocumentDTO
    {
        
        public int Id { get; set; }

        
        public DocumentType DocumentType { get; set; } = DocumentType.Word;


        public string Name { get; set; } = String.Empty;

        
        public string? Description { get; set; }


        
        public int AuthorId { get; set; }

        //public Author Author { get; set; }

        
        public int TopicId { get; set; }

        //public TopicGallery TopicGallery { get; set; }
    }
}
