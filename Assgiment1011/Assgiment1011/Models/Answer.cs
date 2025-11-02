using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    [Table("Answer")]
    [Index(nameof(Content))]
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(4096,ErrorMessage ="The answer should not exceed 4096 chracters")]
        [Required]
        public string Content { get; set; }

        [Required(ErrorMessage = "Created Date is not null")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }


    }
}
