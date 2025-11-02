using Assgiment1011.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Assgiment1011.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ApplicationDBContext Context { get; }
        IAnnouncementRepository AnnouncementRepository{get;}
        IAnswerRepository AnswerRepository { get;}

        IAuthorRepository AuthorRepository { get;}
        IDocumentGalleryRepository DocumentGalleryRepository { get;}
        IDocumentRepository DocumentRepository { get;}
        IEventRepository EventRepository { get;}
        IImageGalleryRepository ImageGalleryRepository { get;}
        INewRepository NewRepository { get;}
        IQuestionRepository QuestionRepository { get;}
        ITopicGalleryRepository TopicGalleryRepository { get;}
        IVideoRepository VideoRepository { get;}

        IImageRepository ImageRepository { get;}

        IVideoGalleryRepository VideoGalleryRepository { get;}

        IDbContextTransaction CreateTransaction();

        void Commit();

        void Rollback();

        Task SaveAsync();

    }
}
