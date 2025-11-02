using Assgiment1011.Data;
using Assgiment1011.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Assgiment1011.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDBContext? _db;
        private IAnnouncementRepository? _announcementRepo;
        private IAnswerRepository? _answerRepo;
        private IQuestionRepository? _questionRepo;
        private IAuthorRepository? _authorRepo;
        private IDocumentGalleryRepository? _documentGalleryRepository;
        private IDocumentRepository? _documentRepository;
        private IEventRepository? _eventRepo;
        private IImageGalleryRepository? _imageGalleryRepo;
        private INewRepository? _newRepository;
        private ITopicGalleryRepository? _topicGalleryRepo;
        private IVideoRepository? _vidRepo;
        private IImageRepository? _imageRepo;
        private IVideoGalleryRepository? _vidGalleryRepo;

        private IDbContextTransaction? _objTran;
        public UnitOfWork(ApplicationDBContext db) {
            _db = db;
        }
        public ApplicationDBContext Context
        {
            get
            {
                return _db;
            }

        }

        public IAnnouncementRepository AnnouncementRepository
        {
            get
            {
                if (_announcementRepo == null)
                    _announcementRepo = new AnnoucementRepository(_db);
                return _announcementRepo;
            }
        }

        public IAnswerRepository AnswerRepository
        {
            get
            {
                if (_answerRepo == null)
                    _answerRepo = new AnswerRepository(_db);

                return _answerRepo;
            }
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (_authorRepo == null)
                    _authorRepo = new AuthorRepository(_db);
                return _authorRepo;
            }
        }

        public IDocumentGalleryRepository DocumentGalleryRepository
        {
            get
            {
                if (_documentGalleryRepository == null)
                    _documentGalleryRepository = new DocumentGalleryRepository(_db);
                return _documentGalleryRepository;
            }
        }
        public IEventRepository EventRepository
        {
            get
            {
                if (_eventRepo == null)
                {
                    _eventRepo = new EventRepository(_db);
                }
                return _eventRepo;
            }
        }

        public IImageGalleryRepository ImageGalleryRepository
        {
            get
            {
                if (_imageGalleryRepo == null)
                    _imageGalleryRepo = new ImageGalleryRepository(_db);
                return _imageGalleryRepo;
            }
        }

        public INewRepository NewRepository
        {
            get
            {
                if (_newRepository == null)
                    _newRepository = new NewRepository(_db);
                return _newRepository;
            }
        }

        public IQuestionRepository QuestionRepository
        {
            get
            {
                if (_questionRepo == null)
                    _questionRepo = new QuestionRepository(_db);
                return _questionRepo;
            }
        }

        public ITopicGalleryRepository TopicGalleryRepository
        {
            get
            {
                if (_topicGalleryRepo == null)
                {
                    _topicGalleryRepo = new TopicGalleryRepository(_db);
                }
                return _topicGalleryRepo;
            }
        }

        public IVideoRepository VideoRepository
        {
            get
            {
                if (_vidRepo == null)
                {
                    _vidRepo = new VideoRepository(_db);
                }

                return _vidRepo;
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepo == null)
                {
                    _imageRepo = new ImageRepository(_db);
                }
                return _imageRepo;
            }
        }

        public IVideoGalleryRepository VideoGalleryRepository
        {
            get
            {
                if (_vidGalleryRepo == null)
                {
                    _vidGalleryRepo = new VideoGalleryRepository(_db);
                }

                return _vidGalleryRepo;
            }
        }

        public IDocumentRepository DocumentRepository
        {
            get
            {
                if(_documentRepository == null)
                {
                    _documentRepository = new DocumentRepository(_db);
                }

                return _documentRepository;
            }
        }

        public void Commit()
        {
            _objTran?.Commit();
        }

        public IDbContextTransaction CreateTransaction()
        {
            _objTran = _db.Database.BeginTransaction();
            return _objTran;
        }

        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
