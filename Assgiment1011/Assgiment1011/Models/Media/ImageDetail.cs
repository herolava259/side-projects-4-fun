using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models.Media
{
    public enum ImageType : int
    {
        png = 1,
        jpg,

    }

    [Table("ImageDetail")]
    public class ImageDetail
    {


        [Key]
        public int Id { get; set; }

        public byte[] Content { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(24)")]
        public ImageType FormatType { get; set; } = ImageType.png;

        [ForeignKey("Image")]
        public int ImageId { get; set; }

        private ILazyLoader LazyLoader { get; set; }
        private Image _img;
        private ImageDetail(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        [BackingField(nameof(_img))]
        public Image Image
        {
            get => LazyLoader.Load(this, ref _img);
            set => _img = value;
        }


    }
}
