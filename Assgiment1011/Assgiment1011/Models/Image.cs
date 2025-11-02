using Assgiment1011.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        [Required]
        public string Url { get; set; }


        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [ForeignKey("ImageGallery")]
        public int ImageGalleryId { get; set; } 
        public ImageGallery ImageGallery { get; set;}

        // Lazy loading with 
        private ILazyLoader LazyLoader { get; set; }
        private ImageDetail _detail;
        private Image(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        [BackingField(nameof(_detail))]
        public ImageDetail ImageDetail 
        { 
            get => LazyLoader.Load(this, ref _detail); 
            set => _detail = value; 
        }

    }
}
