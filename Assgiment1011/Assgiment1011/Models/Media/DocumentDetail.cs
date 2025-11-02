using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assgiment1011.Models.Media
{
    public enum DocumentType
    {
        Word =1,
        Powerpoint,
        Pdf,
        Excel
    }
    [Table("DocumentDetails")]
    public class DocumentDetail
    {
        [Key]
        public int Id { get; set; }

        public byte[] Data { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        [Required]
        public DocumentType Type { get; set; } = DocumentType.Word;

        [ForeignKey("Document")]
        public int DocumentId { get; set; }

        private ILazyLoader LazyLoader { get; set; }
        private Document _doc;
        private DocumentDetail(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        [BackingField(nameof(_doc))]
        public Document Document
        {
            get => LazyLoader.Load(this, ref _doc);
            set => _doc = value;
        }


    }
}
