using KareMa.Domain.Core.Entities;
using KareMa.Infra.SqlServer.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.SqlServer.Common
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
       
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AdminConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new ExpertConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new SubCategoryConfiguration());
            builder.ApplyConfiguration(new SuggestionConfiguration());

            UserConfigurations.SeedUsers(builder);

            base.OnModelCreating(builder);
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
    }
}
