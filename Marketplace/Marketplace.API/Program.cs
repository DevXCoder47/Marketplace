using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Marketplace.Core.Services;
using Marketplace.Storage.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MarketplaceContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => // <<<<< ��������� ��� ApplicationUser
{
    // ������������ ��������� Identity (��� ������������ � ����������������)
    options.SignIn.RequireConfirmedAccount = true; // ��� ��������, � ���������� ����� true (��������� ������������� email)
    options.Password.RequireDigit = true;           // ��������� �����
    options.Password.RequiredLength = 8;            // ����������� �����
    options.Password.RequireNonAlphanumeric = false; // ��������� �����������
    options.Password.RequireUppercase = true;       // ��������� ��������� �����
    options.Password.RequireLowercase = true;       // ��������� �������� �����
    options.Password.RequiredUniqueChars = 0;       // ���������� ���������� ��������

    options.User.RequireUniqueEmail = true; // �����: email ������ ���� ���������� ��� ������� ������������

    // ��������� ���������� ������� ������� ��� ��������� �������� �����
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<MarketplaceContext>() // ��������� Identity ������������ EF Core � ��� DbContext
//.AddDefaultUI() // ��������� ����������� UI ��� Identity (�������� �����������, ������ � �.�.)
.AddDefaultTokenProviders(); // ��� ��������� ������� (����� ������, ������������� ����� � �.�.)
// ************************************************************ JWT **************************************************************
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
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

//app.UseRouting(); ��� ���������

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/

app.Run();

//TEST
