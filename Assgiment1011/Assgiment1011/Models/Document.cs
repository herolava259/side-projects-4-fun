using Assgiment1011.Models.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models
{
    public enum DocumentType: int
    {
        Word = 0,
        PowerPoint = 1,
        Pdf = 2,
        Excel = 3,
    }

    [Table("Document")]
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Column("DocumentType", TypeName ="nvarchar(24)")]
        [Required]
        public DocumentType DocumentType { get; set; } = DocumentType.Word;

        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(4096)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [ForeignKey("TopicGallery")]
        public int TopicId { get; set; }

        public TopicGallery TopicGallery { get; set; }

        // Lazy loading with 
        private ILazyLoader LazyLoader { get; set; }
        private DocumentDetail _detail;
        private Document(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        [BackingField(nameof(_detail))]
        public DocumentDetail DocumentDetail
        {
            get => LazyLoader.Load(this, ref _detail);
            set => _detail = value;
        }

        public Document()
        {
            
        }

    }
}
