using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DataContext>(opt =>{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.file:///C:/Users/farrer.le/Desktop/Intern%20Homework/OnlineCalculator/frontend/caculator.html

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://127.0.0.1:5500"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
