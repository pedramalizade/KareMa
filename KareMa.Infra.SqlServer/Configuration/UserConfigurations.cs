using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareMa.Infra.SqlServer.Configuration
{
    public class UserConfigurations
    {
        public static void SeedUsers(ModelBuilder builder)
        {
            //var hasher = new PasswordHasher<AppUser>();

            // SeedUsers
            //          var users = new List<AppUser>
            //      {
            //          new AppUser()
            //          {
            //              Id = 1,
            //              UserName = "Admin@gmail.com",
            //              NormalizedUserName = "ADMIN@GMAIL.COM",
            //              Email = "Admin@gmail.com",
            //              NormalizedEmail = "ADMIN@GMAIL.COM",
            //              LockoutEnabled = false,
            //              PhoneNumber = "09124545786",
            //              //SecurityStamp = Guid.NewGuid().ToString(),
            //              SecurityStamp = "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
            //              PasswordHash = "1234"
            //          }
            //};

            var user = new AppUser()
            {
                Id = 1,
                UserName = "Admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
                PhoneNumber = "09124545786",
                //SecurityStamp = Guid.NewGuid().ToString(),
                SecurityStamp = "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
                PasswordHash = "1234",
                ConcurrencyStamp = ""
            };

            //foreach (var user in users)
            //{
            //    var passwordHasher = new PasswordHasher<AppUser>();
            //    user.PasswordHash = passwordHasher.HashPassword(user, "1234");

            //    builder.Entity<AppUser>().HasData(user);
            //}

            builder.Entity<AppUser>().HasData(user);

            // Seed Roles
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int>() { Id = 2, Name = "Expert", NormalizedName = "EXPERT" },
                new IdentityRole<int>() { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            //Seed Role To Users
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
            //new IdentityUserRole<int>() { RoleId = 2, UserId = 2 },
            //new IdentityUserRole<int>() { RoleId = 3, UserId = 3 },
            //new IdentityUserRole<int>() { RoleId = 2, UserId = 4 },
            //new IdentityUserRole<int>() { RoleId = 3, UserId = 5 }
            );
        }
    }
}
