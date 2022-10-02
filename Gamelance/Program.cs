global using Microsoft.EntityFrameworkCore;
using Gamelance.Data;
using Gamelance.Services;
using Gamelance.Services.PagesServices;
using Gamelance.Services.GameServices;
using Gamelance.Services.OfferServices;
using Newtonsoft.Json;
using Gamelance.Services.AppsService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using AuthCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

var authOptionsConfiguration = builder.Configuration.GetSection("Auth").Get<AuthOptions>();

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptionsConfiguration.Issuer,

            ValidateAudience = true,
            ValidAudience = authOptionsConfiguration.Audience,

            ValidateLifetime = true,

            IssuerSigningKey = authOptionsConfiguration.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
        };
    });
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

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
