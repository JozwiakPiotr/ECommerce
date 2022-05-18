using ECommerce.Entities;
using ECommerce.Models.DTO;
using ECommerce.Middleware;
using ECommerce.Infrastructure;
using ECommerce.MappingProfiles;
using ECommerce.Infrastructure.EF;
using ECommerce.Models.Validators;
using ECommerce.Models.Settings;
using ECommerce.Authorization;
using ECommerce.Extensions;
using ECommerce.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAutoMapper(typeof(ECommerceProfile).Assembly);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Seeder>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAdressService, AdressService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<BrowseProducts>, BrowseProductsValidator>();
builder.Services.AddScoped<IValidator<RegisterUser>, UserRegisterValidator>();
builder.Services.AddScoped<IAuthorizationHandler, OrderAuthorizationHandler>();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerce"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", corsBuilder =>
    {
        corsBuilder.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(builder.Configuration["AllowedOrigins"]);
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:JwtIssuer"],
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtKey"]))
        };
    });

builder.Host.UseNLog();

var app = builder.Build();

//pipeline
app.UseSeeder();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("FrontEndClient");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();