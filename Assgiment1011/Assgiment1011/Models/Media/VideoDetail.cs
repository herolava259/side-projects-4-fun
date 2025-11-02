using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models.Media
{
    public enum VideoType
    {
        mp4 = 1, mp5, mp6, mp7, mp8, mp9, mp10, mp11, mp12, mp13, mp14, mp15
    }
    public class VideoDetail
    {
        
        [Key]
        public int Id { get; set; }

        public byte[] Content { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(24)")]
        public VideoType FormatType { get; set; } = VideoType.mp4;

        [ForeignKey("Video")]
        public int VideoId { get; set; }

        private ILazyLoader LazyLoader { get; set; }
        private Video _vid;
        private VideoDetail(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        [BackingField(nameof(_vid))]
        public Video Video
        {
            get => LazyLoader.Load(this, ref _vid);
            set => _vid = value;
        }

    }
}
