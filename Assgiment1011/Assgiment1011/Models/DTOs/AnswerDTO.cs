using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Assgiment1011.Models.DTOs
{
    public class AnswerDTO
    {
        
        public int Id { get; set; }
       
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public int QuestionId { get; set; }

        public int AuthorId { get; set; }
    }
}
