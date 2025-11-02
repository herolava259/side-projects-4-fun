using Assgiment1011.Data.FluentConfig;
using Assgiment1011.Models;
using Assgiment1011.Models.Media;
using Microsoft.EntityFrameworkCore;

namespace Assgiment1011.Data
{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentGallery> DocumentGalleries { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ImageGallery> ImageGalleries { get; set; }

        public DbSet<New> News { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<TopicGallery> TopicGalleries { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<VideoGallery> VideoGalleries { get; set; }

        public DbSet<VideoDetail> VideoDetails { get; set; }

        public DbSet<ImageDetail> ImageDetails { get; set; }

        public DbSet<DocumentDetail> DocumentDetails { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            try
            {
                modelBuilder.ApplyConfiguration(new AnnouncementConfiguration());
                modelBuilder.ApplyConfiguration(new AnswerConfiguration());
                modelBuilder.ApplyConfiguration(new AuthorConfiguration());

                modelBuilder.ApplyConfiguration(new DocumentConfiguration());
                
                modelBuilder.ApplyConfiguration(new DocumentGalleryConfiguration());

                modelBuilder.ApplyConfiguration(new EventConfiguration());

                modelBuilder.ApplyConfiguration(new ImageConfiguration());
                
                modelBuilder.ApplyConfiguration(new ImageGalleryConfiguration());

                modelBuilder.ApplyConfiguration(new NewConfiguration());

                modelBuilder.ApplyConfiguration(new QuestionConfiguration());

                modelBuilder.ApplyConfiguration(new TopicGalleryConfiguration());

                modelBuilder.ApplyConfiguration(new UserConfiguration());

                modelBuilder.ApplyConfiguration(new VideoConfiguration());
                modelBuilder.ApplyConfiguration(new VideoGalleryConfiguration());

                modelBuilder.ApplyConfiguration(new VideoDetailConfiguration());
                modelBuilder.ApplyConfiguration(new DocumentDetailConfiguration());
                modelBuilder.ApplyConfiguration(new ImageDetailConfiguration());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
