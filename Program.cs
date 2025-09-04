using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Text;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddAuthorization();
//Register the NSwag Services
builder.Services.AddOpenApiDocument(options =>
{
    options.AddSecurity("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer Token Authorization header",
        Type = OpenApiSecuritySchemeType.Http,
        In = OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer"
    });
    options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

//Inject the DbContext //UseSqlServer
builder.Services.AddDbContext<VideoGameDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Authentication Scheme
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!)),
            ValidateIssuerSigningKey = true
        });



//Inject Services
builder.Services.AddScoped<IVideoGame, VideoGameServices>();
builder.Services.AddScoped<IDeveloper, DeveloperServices>();
builder.Services.AddScoped<IPublisher, PublisherServices>();
builder.Services.AddScoped<IGenre, GenreServices>(); 
builder.Services.AddScoped<IUser, UserServices>();

var app = builder.Build();

// ?? Run migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VideoGameDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.MapScalarApiReference();
//    //app.MapOpenApi();
//    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/openapi/v1.json", "GameApi"));

//}

app.UseOpenApi();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
