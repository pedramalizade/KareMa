using KareMa.Domain.Core.Contracts.Repositories.Category;
using KareMa.Domain.Core.Contracts.Repositories;
using KareMa.Infra.DataAccess.Repo.Ef.Repository;
using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts.Service;
using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.Contracts.Service.BaseService;
using KareMa.Domain.Service;
using KareMa.Domain.Service.BaseService;
using KareMa.Domain.Service.Expect;
using KareMa.Domain.AppService;
using KareMa.Domain.Core.Contracts.AppService.BaseServices;
using KareMa.Domain.AppService.BaseAppServices;
using KareMa.Domain.Services;
using KareMa.Infra.SqlServer.Common;
using Microsoft.AspNetCore.Identity;
using KareMa.Domain.Core.Entities;
using Framework;
using KareMa.Domain.Core.Entities.Configs;
using Microsoft.EntityFrameworkCore;
using KareMa.EndPoint.RazorPages.Infrastructure;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Seq;
using KareMa.Domain.Core.Contracts.AppService.Account;
using KareMa.Domain.AppService.Account;
using KareMa.Domain.Core.Entities.Configs;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.AddJsonFile("appsettings.Development.json");

//string? Connectionstring = builder.Configuration.GetConnectionString("sql");
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Connectionstring));

//builder.Services.AddDbContext<AppDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionStringName")));

//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .Build();

//var siteSettings = builder.Configuration.GetSection("SiteSetting").Get<SiteSettings>();
var seq = builder.Configuration.GetSection("Seq").Get<Seq>();

var commentConfig = builder.Configuration.GetSection("CommentConfiguration").Get<CommentConfiguration>();
builder.Services.AddSingleton(commentConfig);

//var commentConfiguration = builder.Configuration.GetSection("CommentConfiguration").Get<CommentConfiguration>();


var connectionString = builder.Configuration.GetSection("ConnectionStrings").Value;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

//Admin Services
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<IAdminAppServices, AdminAppServices>();


//Order Services
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderAppServices, OrderAppServices>();

//Service Services
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceServices, ServiceServices>();
builder.Services.AddScoped<IServiceAppServices, ServiceAppServices>();

//Category Services
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICategoryAppServices, CategoryAppServices>();

//SubCategory Services
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
builder.Services.AddScoped<ISubCategoryAppServices, SubCategoryAppServices>();

//Comment Services
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentServices, CommentServices>();
builder.Services.AddScoped<ICommentAppServices, CommentAppServices>();

//Suggestion Services
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<ISuggestionServices, SuggestionServices>();
builder.Services.AddScoped<ISuggestionAppServices, SuggestionAppServices>();

//Address Services
builder.Services.AddScoped<IAddressRepository, AddressRepository>();


//City Services
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityServices, CityServices>();
builder.Services.AddScoped<ICityAppServices, CityAppServices>();

//Customer Services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<ICustomerAppServices, CustomerAppServices>();

//Expert Services
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IExpertServices, ExpertServices>();
builder.Services.AddScoped<IExpertAppServices, ExpertAppServices>();

//Base Services
builder.Services.AddScoped<IBaseSevices, BaseService>();
builder.Services.AddScoped<IBaseAppServices, BaseAppServices>();

// Account Service
builder.Services.AddScoped<IAccountAppServices, AccountAppServices>();



builder.Services.AddIdentity<AppUser, IdentityRole<int>>
    (options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<PersianIdentityErrorDescriber>();



builder.Host.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();

}).UseSerilog((context, config) =>
{
    config.WriteTo.Console();
    config.WriteTo.Seq(seq.ServerUrl, LogEventLevel.Information, apiKey: seq.ApiKey);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.CustomExceptionHandlingMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
