using Assgiment1011.Contracts;
using Assgiment1011.Data;
using Assgiment1011.MappingConfig;
using Assgiment1011.Repository;
using Assgiment1011.Repository.IRepository;
using Assgiment1011.Repository.MediaRepository;
using Assgiment1011.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add repositories, services interact with dbContext 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMediaUnitOfWork, MediaUnitOfWork>();

builder.Services.AddScoped<IAnnouncementRepository, AnnoucementRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IDocumentRepository,DocumentRepository>();
builder.Services.AddScoped<IDocumentDetailRepository, DocumentDetailRepository>();
builder.Services.AddScoped<IDocumentGalleryRepository, DocumentGalleryRepository>();

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageGalleryRepository, ImageGalleryRepository>();
builder.Services.AddScoped<IImageDetailRepository, ImageDetailRepository>();

builder.Services.AddScoped<INewRepository, NewRepository>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

builder.Services.AddScoped<ITopicGalleryRepository, TopicGalleryRepository>();

builder.Services.AddScoped<IVideoGalleryRepository, VideoGalleryRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoDetailRepository, VideoDetailRepository>();

// end 
// Use DbContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"))
           ;
}
);

builder.Services.AddAutoMapper(typeof(DefaultMappingConfig));

// provide LoggerService
builder.Services.AddSingleton<ILoggerService, LoggerService>();

builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
