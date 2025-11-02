using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Assgiment1011.Models
{
    [Table("News")]
    public class New
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }


        public DateTime CreatedDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }


    }
}
