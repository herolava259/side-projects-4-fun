using Assgiment1011.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }


        // Lazy loading with media
        private VideoDetail _detail;

        public Video()
        {
            
        }

        private ILazyLoader LazyLoader { get; set; }

        private Video(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        [BackingField(nameof(_detail))]
        public VideoDetail VideoDetail { 
            get => LazyLoader.Load(this, ref _detail); 
            set => _detail = value; 
        }
    }
}
