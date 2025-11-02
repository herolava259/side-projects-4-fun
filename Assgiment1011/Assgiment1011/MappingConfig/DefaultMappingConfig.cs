using Assgiment1011.Models;
using Assgiment1011.Models.DTOs;
using Assgiment1011.Models.DTOs.Updated;
using AutoMapper;

namespace Assgiment1011.MappingConfig
{
    public class DefaultMappingConfig: Profile
    {
        public DefaultMappingConfig()
        {
            CreateMap<Announcement, AnnouncementDTO>().ReverseMap();
            CreateMap<Announcement, AnnouncementUpdateDTO>().ReverseMap();

            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Announcement, AnnouncementUpdateDTO>().ReverseMap();

            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<Document, DocumentUpdateDTO>().ReverseMap();

            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, EventUpdateDTO>().ReverseMap();

            CreateMap<ImageGallery, ImageGalleryDTO>().ReverseMap();
            CreateMap<ImageGallery, ImageGalleryUpdateDTO>().ReverseMap();

            CreateMap<New, NewDTO>().ReverseMap();
            CreateMap<New, NewUpdateDTO>().ReverseMap();

            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Question, QuestionUpdateDTO>().ReverseMap();

            CreateMap<TopicGallery, TopicGalleryDTO>().ReverseMap();
            CreateMap<TopicGallery, TopicGalleryUpdateDTO>().ReverseMap();

            CreateMap<VideoGallery,  VideoGalleryDTO > ().ReverseMap();
            CreateMap<VideoGallery, VideoGalleryUpdateDTO>().ReverseMap();

            CreateMap<DocumentGallery, DocumentGalleryDTO>().ReverseMap();
            CreateMap<DocumentGallery, DocumentGalleryUpdateDTO>().ReverseMap();

        }
    }
}
