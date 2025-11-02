using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    [Table("Announcement")]
    [Index(nameof(Title), nameof(Slug), AllDescending = true)]
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(24)")]
        public EntityState State { get; set; }

        [Required]
        public string? Title { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        public string Slug { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [NotMapped]
        public string AdditionalInfo { get; set; }
    }
}
