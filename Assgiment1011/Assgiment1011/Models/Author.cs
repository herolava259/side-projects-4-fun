using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Author' name is required")]
        [MinLength(2,ErrorMessage = "The Name must have at least 2 chracter")]
        [MaxLength(100, ErrorMessage ="The name must have at most 100 chracter")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "The Name must have at least 2 chracter")]
        [MaxLength(100, ErrorMessage = "The name must have at most 100 character")]
        [Column("AdditionalName")]
        public string NickName { get; set; }

        public string Major { get; set; } 

        public User User { get; set; }

        public ICollection<Announcement> Announcements { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<Document>  Documents { get; set; }

        public ICollection<DocumentGallery> DocumentGalleries { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<ImageGallery> ImageGalleries { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<Video> Videos { get; set; }

        public ICollection<VideoGallery> VideoGalleries { get; set; }

        public ICollection<New> News { get; set; }

        /*public virtual IEnumerable<EntityBase> Entities { get; set; }*/
    }
}
