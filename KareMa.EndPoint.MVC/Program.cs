using Framework;
using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Domain.Core.Contracts.Repositories.Category;
using KareMa.Domain.Core.Entities;
using KareMa.Domain.Core.Entities.Configs;
using KareMa.Infra.DataAccess.Repo.Ef.Repository;
using KareMa.Infra.SqlServer.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

//Admin Services
builder.Services.AddScoped<IAdminRepository, AdminRepository>();


//Order Services
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


//Service Services
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();


//Category Services
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


//SubCategory Services
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();


//Comment Services
builder.Services.AddScoped<ICommentRepository, CommentRepository>();


//Suggestion Services
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();


//Address Services
builder.Services.AddScoped<IAddressRepository, AddressRepository>();


//City Services
builder.Services.AddScoped<ICityRepository, CityRepository>();


//Customer Services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


//Expert Services
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();


//builder.Services.AddIdentity<AppUser, IdentityRole<int>>
//    (options =>
//    {
//        options.SignIn.RequireConfirmedAccount = false;
//        options.Password.RequireDigit = false;
//        options.Password.RequiredLength = 6;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireUppercase = false;
//        options.Password.RequireLowercase = false;
//    })
//    .AddRoles<IdentityRole<int>>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddErrorDescriber<PersianIdentityErrorDescriber>();



var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
builder.Services.AddSingleton(siteSettings);

builder.Services.AddDbContext<AppDbContext>(options
    => options.UseSqlServer(siteSettings.ConnectionStrings.SqlConnection));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
