using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

    }
}
