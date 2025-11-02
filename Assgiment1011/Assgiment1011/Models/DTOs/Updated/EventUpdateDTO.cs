using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assgiment1011.Models.DTOs.Updated
{
    public class EventUpdateDTO
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public string? Description { get; set; }

        
        public DateTime BeginDate { get; set; }

        
        public DateTime EndDate { get; set; }

        
        

        
    }
}
