using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Marketplace.Core.Services;
using Marketplace.Storage.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MarketplaceContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => // <<<<< УКАЗЫВАЕМ НАШ ApplicationUser
{
    // Опциональные настройки Identity (для безопасности и функциональности)
    options.SignIn.RequireConfirmedAccount = true; // Для простоты, в продакшене лучше true (требовать подтверждение email)
    options.Password.RequireDigit = true;           // Требовать цифры
    options.Password.RequiredLength = 8;            // Минимальная длина
    options.Password.RequireNonAlphanumeric = false; // Требовать спецсимволы
    options.Password.RequireUppercase = true;       // Требовать заглавные буквы
    options.Password.RequireLowercase = true;       // Требовать строчные буквы
    options.Password.RequiredUniqueChars = 0;       // Количество уникальных символов

    options.User.RequireUniqueEmail = true; // Важно: email должен быть уникальным для каждого пользователя

    // Настройки блокировки учетных записей при неудачных попытках входа
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<MarketplaceContext>() // Указывает Identity использовать EF Core и ваш DbContext
//.AddDefaultUI() // Добавляет стандартный UI для Identity (страницы регистрации, логина и т.д.)
.AddDefaultTokenProviders(); // Для генерации токенов (сброс пароля, подтверждение почты и т.д.)
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICustomListService, CustomListService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseRouting(); для мидлваров

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/

app.Run();

//TEST
