using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs.Updated
{
    public class AnnouncementUpdateDTO
    {

        public int Id { get; set; }

        public EntityState State { get; set; }

        
        public string? Title { get; set; }

        
        public string? Description { get; set; }

        public string Slug { get; set; }

        
        public string ImageUrl { get; set; }

        
        

    }
}
