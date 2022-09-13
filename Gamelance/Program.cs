global using Microsoft.EntityFrameworkCore;
using Gamelance.Data;
using Gamelance.Services;
using Gamelance.Services.PagesServices;
using Gamelance.Services.GameServices;
using Gamelance.Services.OfferServices;
using Newtonsoft.Json;
using Gamelance.Services.AppsService;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IOfferAdminService, OfferService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUserPagesService, UserPagesService>();
builder.Services.AddScoped<IChangeUserNameAppsService, ChangeUserNameAppsService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

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
