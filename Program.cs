using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Inject the DbContext
builder.Services.AddDbContext<VideoGameDbContext>( options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) );

//Inject Services
builder.Services.AddScoped<IVideoGame, VideoGameServices>();
builder.Services.AddScoped<IDeveloper, DeveloperServices>();
builder.Services.AddScoped<IPublisher, PublisherServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapScalarApiReference();
    app.MapOpenApi();

    app.UseSwaggerUI(c => c.SwaggerEndpoint("/openapi/v1.json", "GameApi"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
