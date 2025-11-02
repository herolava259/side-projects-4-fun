namespace Assgiment1011.Models.FilterModels
{
    public class AnnouncementFilterModel
    {
        public int? Id { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? Title { get; set; }

        public string? Slug { get; set; }
    }
}
