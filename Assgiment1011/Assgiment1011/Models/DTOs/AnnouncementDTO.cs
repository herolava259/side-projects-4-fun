using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs
{
    public class AnnouncementDTO
    {
        
        public int Id { get; set; }

        
        public DateTime CreatedDate { get; set; }

        public string? Title { get; set; }

        
        public string? Description { get; set; }

        public string Slug { get; set; }

        
        public string ImageUrl { get; set; }

        public int AuthorId { get; set; }


        public static bool TryParse(string value, out AnnouncementDTO answer)
        {
            answer = null;
            var parts = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
            
            if(parts.Length != 7)
            {
                return false;
            }

            int id, authorId;
            string title, description, slug, imageurl;
            DateTime createdDate;
            if (! Int32.TryParse(parts[0], out id))
            {
                return false;
            }

            if (!Int32.TryParse(parts[6], out authorId))
            {
                return false;
            }

            if (!DateTime.TryParse(parts[1], out createdDate))
            {
                return false;
            }

            title = parts[2];

            description = parts[3];
            slug = parts[4];
            imageurl = parts[5];

            answer = new AnnouncementDTO
            {
                Id = id,
                CreatedDate = createdDate,
                Title = title,
                Description = description,
                Slug = slug,
                ImageUrl = imageurl,
                AuthorId = authorId
            };

            return true;

        }
    }   
}
