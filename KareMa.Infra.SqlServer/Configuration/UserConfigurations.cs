using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KareMa.Infra.SqlServer.Configuration
{
    public static class UserConfigurations
    {
        public static void SeedUsers(ModelBuilder builder)
        {
            var users = new List<AppUser>
            {
                new AppUser()
                {
                Id = 1,
                UserName = "Admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
                PhoneNumber = "09124545786",
                SecurityStamp = "5696d88e-c49d-4804-8721-7571f4f7bc57", 
                ConcurrencyStamp = "686fc046-614e-491d-a647-51ae4fe0a8d4", 
                PasswordHash = "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg==" 
                },
                new AppUser()
                {
                    Id = 2,
                    UserName = "Ali@gmail.com",
                    NormalizedUserName = "ALI@GMAIL.COM",
                    Email = "Ali@gmail.com",
                    NormalizedEmail = "ALI@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "09377507920",
                    SecurityStamp = "b1e29a80-1111-1111-1111-111111111111", 
                    ConcurrencyStamp = "c1e29a80-2222-2222-2222-222222222222", 
                    PasswordHash = "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg=="
                },
                new AppUser()
                {
                    Id = 3,
                    UserName = "Sahar@gmail.com",
                    NormalizedUserName = "SAHAR@GMAIL.COM",
                    Email = "Sahar@gmail.com",
                    NormalizedEmail = "SAHAR@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "09377507920",
                    SecurityStamp = "d2e29a80-3333-3333-3333-333333333333", 
                    ConcurrencyStamp = "e2e29a80-4444-4444-4444-444444444444", 
                    PasswordHash = "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg=="
                },
                new AppUser()
                {
                    Id = 4,
                    UserName = "Sara@gmail.com",
                    NormalizedUserName = "SARA@GMAIL.COM",
                    Email = "Sara@gmail.com",
                    NormalizedEmail = "SARA@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "09377507920",
                    SecurityStamp = "f3e29a80-5555-5555-5555-555555555555", 
                    ConcurrencyStamp = "g3e29a80-6666-6666-6666-666666666666", 
                    PasswordHash = "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg=="
                },
                new AppUser()
                {
                    Id = 5,
                    UserName = "Mohammad@gmail.com",
                    NormalizedUserName = "MOHAMMAD@GMAIL.COM",
                    Email = "Mohammad@gmail.com",
                    NormalizedEmail = "MOHAMMAD@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "09377507920",
                    SecurityStamp = "h4e29a80-7777-7777-7777-777777777777",
                    ConcurrencyStamp = "i4e29a80-8888-8888-8888-888888888888", 
                    PasswordHash = "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg=="
                }

            };






            builder.Entity<AppUser>().HasData(users);

            // تعریف نقش‌ها
            var roles = new List<IdentityRole<int>>
            {
                new() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new() { Id = 2, Name = "Expert", NormalizedName = "EXPERT" },
                new() { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER" }
            };

            builder.Entity<IdentityRole<int>>().HasData(roles);

            // تخصیص نقش Admin به کاربر
            builder.Entity<IdentityUserRole<int>>().HasData(
                    new IdentityUserRole<int> { RoleId = 1, UserId = 1 },
                new IdentityUserRole<int>() { RoleId = 2, UserId = 2 },
                new IdentityUserRole<int>() { RoleId = 3, UserId = 3 },
                new IdentityUserRole<int>() { RoleId = 2, UserId = 4 },
                new IdentityUserRole<int>() { RoleId = 3, UserId = 5 }
                );
        }
    }
}



//using KareMa.Domain.Core.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KareMa.Infra.SqlServer.Configuration
//{
//    public class UserConfigurations
//    {
//        public static void SeedUsers(ModelBuilder builder)
//        {
//            var hasher = new PasswordHasher<AppUser>();


//            var users = new List<AppUser>
//            {
//                      new AppUser()
//                      {
//                          Id = 1,
//                          UserName = "Admin@gmail.com",
//                          NormalizedUserName = "ADMIN@GMAIL.COM",
//                          Email = "Admin@gmail.com",
//                          NormalizedEmail = "ADMIN@GMAIL.COM",
//                          LockoutEnabled = false,
//                          PhoneNumber = "09124545786",
//                          //SecurityStamp = Guid.NewGuid().ToString(),
//                          SecurityStamp = "5696d88e-c49d-4804-8721-7571f4f7bc57",
//                          PasswordHash = "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg==",
//                          ConcurrencyStamp = "686fc046-614e-491d-a647-51ae4fe0a8d4"
//                      }

//           };

//            //var user = new AppUser()
//            //{
//            //    Id = 1,
//            //    UserName = "Admin@gmail.com",
//            //    NormalizedUserName = "ADMIN@GMAIL.COM",
//            //    Email = "Admin@gmail.com",
//            //    NormalizedEmail = "ADMIN@GMAIL.COM",
//            //    LockoutEnabled = false,
//            //    PhoneNumber = "09124545786",
//            //    //SecurityStamp = Guid.NewGuid().ToString(),
//            //    //SecurityStamp = "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
//            //    //PasswordHash = "1234",
//            //    //ConcurrencyStamp = ""
//            //};

//            foreach (var user in users)
//            {
//                //var passwordHasher = new PasswordHasher<AppUser>();
//                //user.PasswordHash = passwordHasher.HashPassword(user, "1234");

//                builder.Entity<AppUser>().HasData(user);
//            }

//            //builder.Entity<AppUser>().HasData(users);

//            // Seed Roles
//            builder.Entity<IdentityRole<int>>().HasData(
//                new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
//                new IdentityRole<int>() { Id = 2, Name = "Expert", NormalizedName = "EXPERT" },
//                new IdentityRole<int>() { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER" }
//            );

//            //Seed Role To Users
//            builder.Entity<IdentityUserRole<int>>().HasData(
//                new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
//            //new IdentityUserRole<int>() { RoleId = 2, UserId = 2 },
//            //new IdentityUserRole<int>() { RoleId = 3, UserId = 3 },
//            //new IdentityUserRole<int>() { RoleId = 2, UserId = 4 },
//            //new IdentityUserRole<int>() { RoleId = 3, UserId = 5 }
//            );
//        }
//    }
//}
