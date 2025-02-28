using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KareMa.Infra.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    BankCardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankCardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ExpertId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpertService",
                columns: table => new
                {
                    ExpertsId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertService", x => new { x.ExpertsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ExpertService_Experts_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequesteForTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoneAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    SuggestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Expert", "EXPERT" },
                    { 3, null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "686fc046-614e-491d-a647-51ae4fe0a8d4", "Admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg==", "09124545786", false, "5696d88e-c49d-4804-8721-7571f4f7bc57", false, "Admin@gmail.com" },
                    { 2, 0, "c1e29a80-2222-2222-2222-222222222222", "Ali@gmail.com", false, false, null, "ALI@GMAIL.COM", "ALI@GMAIL.COM", "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg==", "09377507920", false, "b1e29a80-1111-1111-1111-111111111111", false, "Ali@gmail.com" },
                    { 3, 0, "e2e29a80-4444-4444-4444-444444444444", "Sahar@gmail.com", false, false, null, "SAHAR@GMAIL.COM", "SAHAR@GMAIL.COM", "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg==", "09377507920", false, "d2e29a80-3333-3333-3333-333333333333", false, "Sahar@gmail.com" },
                    { 4, 0, "g3e29a80-6666-6666-6666-666666666666", "Sara@gmail.com", false, false, null, "SARA@GMAIL.COM", "SARA@GMAIL.COM", "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg==", "09377507920", false, "f3e29a80-5555-5555-5555-555555555555", false, "Sara@gmail.com" },
                    { 5, 0, "i4e29a80-8888-8888-8888-888888888888", "Mohammad@gmail.com", false, false, null, "MOHAMMAD@GMAIL.COM", "MOHAMMAD@GMAIL.COM", "AQAAAAIAAYagAAAAEA1fG2ymdTI5QNpS5QaRSW8d7xQubl5bv246a69tsF9zUnp/2r22+KrUG51REjLJLg==", "09377507920", false, "h4e29a80-7777-7777-7777-777777777777", false, "Mohammad@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Image", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\Category\\1.jpg", false, "تمیزکاری" },
                    { 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\Category\\2.jpg", false, "ساختمان" },
                    { 3, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\Category\\3.jpg", false, "تعمیرات اشیاء" },
                    { 4, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\Category\\4.jpg", false, "اسباب‌کشی و حمل بار" },
                    { 5, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\Category\\5.jpg", false, "خودرو" },
                    { 6, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\Category\\6.jpg", false, "سازمان‌ها و مجتمع‌ها" },
                    { 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\Category\\7.jpg", false, "سلامت و زیبایی" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهران" },
                    { 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "آذربایجان غربی" },
                    { 3, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "اردبیل" },
                    { 4, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "اصفهان" },
                    { 5, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "البرز" },
                    { 6, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ایلام" },
                    { 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "بوشهر" },
                    { 8, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "آذربایجان شرقی" },
                    { 9, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "چهارمحال و بختیاری" },
                    { 10, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "خراسان جنوبی" },
                    { 11, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "خراسان رضوی" },
                    { 12, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "خراسان شمالی" },
                    { 13, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "خوزستان" },
                    { 14, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "زنجان" },
                    { 15, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "سمنان" },
                    { 16, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "سیستان و بلوچستان" },
                    { 17, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "فارس" },
                    { 18, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "قزوین" },
                    { 19, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "قم" },
                    { 20, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "کردستان" },
                    { 21, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "کرمان" },
                    { 22, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "کرمانشاه" },
                    { 23, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "کهگیلویه و بویراحمد" },
                    { 24, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "گلستان" },
                    { 25, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "گیلان" },
                    { 26, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "لرستان" },
                    { 27, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "مازندران" },
                    { 28, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "مرکزی" },
                    { 29, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "هرمزگان" },
                    { 30, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "همدان" },
                    { 31, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "یزد" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId", "CreatedAt", "FirstName", "Gender", "IsDeleted", "LastName" },
                values: new object[] { 1, 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "پدرام", 2, false, "علیزاده" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 2, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AppUserId", "BankCardNumber", "CreatedAt", "FirstName", "Gender", "IsDeleted", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 3, "1234123412341234", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "تارا", 1, false, "بابایی", "09192365988" },
                    { 2, 5, "1239684412341234", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "امیر", 2, false, "تقوایی", "09014839264" }
                });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "AppUserId", "BankCardNumber", "BirthDate", "CreatedAt", "FirstName", "Gender", "IsConfirm", "IsDeleted", "LastName", "PhoneNumber", "ProfileImage" },
                values: new object[,]
                {
                    { 1, 2, "1234123412341234", new DateTime(1999, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "علی", 2, true, false, "کریمی", "09362356998", null },
                    { 2, 4, "1234123412341234", new DateTime(1999, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "سارا", 1, true, false, "خاتمی", "09362357998", null }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Image", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\1.jpg", false, "نظافت و پذیرایی" },
                    { 2, 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\2.jpg", false, "شستشو" },
                    { 3, 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\3.jpg", false, "کارواش و دیتیلینگ" },
                    { 4, 3, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\4.jpg", false, "سرمایش و گرمایش" },
                    { 5, 3, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\5.jpg", false, "نصب وتعمیر لوازم خانگی" },
                    { 7, 3, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\7.jpg", false, "خذمات کامپیوتری" },
                    { 8, 3, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\8.jpg", false, "تعمیرات موبایل" },
                    { 9, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\9.jpg", false, "سرمایش و گرمایش" },
                    { 10, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\10.jpg", false, "تعمیرا ساختمان" },
                    { 11, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\11.jpg", false, "لوله کشی" },
                    { 12, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\12.jpg", false, "طراحی و بازسازی ساختمان" },
                    { 13, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\13.jpg", false, "برق کاری" },
                    { 14, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\14.jpg", false, "چوب و کابینت" },
                    { 15, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\15.jpg", false, "خدمات شیشه ای ساختمان" },
                    { 16, 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\16.jpg", false, "باغبانی و فضای سبز" },
                    { 17, 4, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\17.jpg", false, "باربری و جا به جایی" },
                    { 18, 5, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\18.jpg", false, "خدمات و تعمیرات خودرو" },
                    { 19, 5, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\19.jpg", false, "کارواش و دیتیلینگ" },
                    { 20, 6, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\20.jpg", false, "خدمات شرکتی" },
                    { 21, 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\21.jpg", false, "زیبایی بانوان" },
                    { 22, 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\22.jpg", false, "پیرایش و زیبایی آقایان" },
                    { 23, 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\23.jpg", false, "پزشکی و پرستاری" },
                    { 24, 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\24.jpg", false, "حیوانات خانگی" },
                    { 25, 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "\\Images\\SubCategory\\25.jpg", false, "تندرستی و ورزش" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "Description", "ExpertId", "IsAccept", "IsDeleted", "Score", "Title" },
                values: new object[] { 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "کارش بی نظیر بود", 1, false, false, 4, "عالی" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CreatedAt", "Image", "IsDeleted", "Name", "Price", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس عادی نظافت", 700000, 1 },
                    { 2, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس لوکس نظافت", 850000, 1 },
                    { 3, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس ویژه نظافت", 1000000, 1 },
                    { 5, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نظافت راه‌پله", 1000000, 1 },
                    { 6, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس نظافت فوری", 1000000, 1 },
                    { 7, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پذیرایی", 1000000, 1 },
                    { 8, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کارگر ساده", 1000000, 1 },
                    { 9, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سمپاشی فضای داخلی", 1000000, 1 },
                    { 10, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ضدعفونی منزل و محل کار", 1000000, 1 },
                    { 11, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شستشوی مبل ،فرش و موکت در محل", 1000000, 2 },
                    { 12, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خشکشویی", 1000000, 2 },
                    { 13, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پرده شویی", 1000000, 2 },
                    { 25, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و سرویس کولر آبی", 1000000, 9 },
                    { 26, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر کولر گازی و داکت اسپلیت", 1000000, 9 },
                    { 27, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و سرویس پکیج", 1000000, 9 },
                    { 28, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و سرویس ابگرمکن", 1000000, 9 },
                    { 29, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کانال سازی کولر", 1000000, 9 },
                    { 32, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس و تعمیر چیلر", 1000000, 9 },
                    { 33, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و سرویس فن کویل", 1000000, 9 },
                    { 34, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر بخاری گازی و شومینه", 1000000, 9 },
                    { 36, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "بهبود آلاینده‌های موتورخانه با دستگاه آنالیز", 1000000, 9 },
                    { 37, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ساخت و نصب توری", 1000000, 10 },
                    { 38, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "جوشکاری و آهنگری ، درب و پنجره آهنی", 1000000, 10 },
                    { 39, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "دوخت و نصب پرده", 1000000, 10 },
                    { 40, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کاشی کاری و سرامیک", 1000000, 10 },
                    { 41, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "بنایی", 1000000, 10 },
                    { 42, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کلید سازی", 1000000, 10 },
                    { 43, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر انواع کفپوش و دیوارپوش", 1000000, 10 },
                    { 44, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کچ کاری و رابیتس کاری", 1000000, 10 },
                    { 45, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "آچار فرانسه", 1000000, 10 },
                    { 46, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "دریل کاری", 1000000, 10 },
                    { 47, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب ایزوگام ، قیرگونی و آسفالت", 1000000, 10 },
                    { 48, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیرات نما و نماشویی", 1000000, 10 },
                    { 49, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کفسابی", 1000000, 10 },
                    { 50, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تخریب", 1000000, 10 },
                    { 51, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سقف و دیوار PVC و اسمان مجازی", 1000000, 10 },
                    { 52, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شیروانی و ایرانیت", 1000000, 10 },
                    { 53, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و نگهداری استخر", 1000000, 10 },
                    { 54, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کپسول آتش‌ نشانی", 1000000, 10 },
                    { 55, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کناف کاری", 1000000, 10 },
                    { 56, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر شیرآلات", 1000000, 11 },
                    { 57, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تخلیه چاه و لوله بازکنی", 1000000, 11 },
                    { 58, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "لوله کشی آب و فاضلاب", 1000000, 11 },
                    { 59, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و سرویس توالت فرنگی و ایرانی", 1000000, 11 },
                    { 60, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تشخیص و ترمیم ترکیدگی لوله", 1000000, 11 },
                    { 61, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پمپ و منبع آب", 1000000, 11 },
                    { 62, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر دستگاه تصفیه آب", 1000000, 11 },
                    { 63, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر فلاش تانک و سیفون", 1000000, 11 },
                    { 64, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "لوله کشی گاز", 1000000, 11 },
                    { 65, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب سینک ظرفشویی", 1000000, 11 },
                    { 66, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب روشویی", 1000000, 11 },
                    { 67, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر وال هنگ", 1000000, 11 },
                    { 68, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اتصال به شبکه فاضلاب شهری", 1000000, 11 },
                    { 69, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مشاوره و بازسازی ساختمان", 1000000, 12 },
                    { 70, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "دکوراسیون و طراحی ساختمان", 1000000, 12 },
                    { 71, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "رفع اتصالی", 1000000, 13 },
                    { 72, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر آیفون صوتی و تصویری", 1000000, 13 },
                    { 73, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب لوستر و چراغ", 1000000, 13 },
                    { 74, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سیم کشی تلفن و نصب سانترال", 1000000, 13 },
                    { 75, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر آنتن تلویزیون", 1000000, 13 },
                    { 76, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر دوربین مداربسته", 1000000, 13 },
                    { 77, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کلید و پریز", 1000000, 13 },
                    { 78, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و سرویس آسانسور", 1000000, 13 },
                    { 79, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر کرکره برقی", 1000000, 13 },
                    { 80, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر جک پارکینگ و آرام بند", 1000000, 13 },
                    { 81, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "هواکش و تهویه مطبوع", 1000000, 13 },
                    { 82, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ساخت و تعمیر تابلو روان و چلنیوم", 1000000, 13 },
                    { 83, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر بالابر", 1000000, 13 },
                    { 84, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر دزدگیر اماکن", 1000000, 13 },
                    { 85, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سیم پیجی", 1000000, 13 },
                    { 86, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات برق صنعتی و سه فاز", 1000000, 13 },
                    { 87, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب سنسور و تایمر", 1000000, 13 },
                    { 88, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب محافظ برق و استابلایزر", 1000000, 13 },
                    { 89, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ساهت و تعمیر تابلو برق", 1000000, 13 },
                    { 90, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "طراحی و اجرای نور مخفی", 1000000, 13 },
                    { 91, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب داکت و ترانکینگ", 1000000, 13 },
                    { 92, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر زنراتور و برق اضطراری", 1000000, 13 },
                    { 93, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر اگزاست‌فن و سانتریفیوژ", 1000000, 13 },
                    { 94, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "طراحی و علامت گذاری جعبه فیوز", 1000000, 13 },
                    { 95, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سیستم اعلام و اطفاء جریق", 1000000, 13 },
                    { 96, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سیم کشی ارت", 1000000, 13 },
                    { 97, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "هوشمندسازی ساختمان", 1000000, 13 },
                    { 98, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "طراحی و اجرای فنس الکتریکی", 1000000, 13 },
                    { 99, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر راه بند", 1000000, 13 },
                    { 100, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و سرویس پله برقی", 1000000, 13 },
                    { 101, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ساخت ، نصب و تعمیر کابینت", 1000000, 14 },
                    { 102, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نجاری", 1000000, 14 },
                    { 103, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیرا مبلمان", 1000000, 14 },
                    { 104, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات درب چوبی و ضدسرقت", 1000000, 14 },
                    { 105, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و ساخت کمد دیواری", 1000000, 14 },
                    { 106, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شیشه بری و آینه کاری", 1000000, 14 },
                    { 107, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ساخت ، رگلاژ درب و پنجره آلمینیومی و UPVC", 1000000, 15 },
                    { 108, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شیشه ریلی و جام بالکن", 1000000, 15 },
                    { 109, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر درب اتوماتیک", 1000000, 15 },
                    { 110, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شیشه ریلی اسلاید", 1000000, 15 },
                    { 111, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ساخت کابین دوش", 1000000, 15 },
                    { 112, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "باغبانی", 1000000, 16 },
                    { 113, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نگهداری از گیاهان آپارتمانی", 1000000, 16 },
                    { 114, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سمپاچی باغچه و فضای سبز", 1000000, 16 },
                    { 115, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مشاوره گل و گیاه", 1000000, 16 },
                    { 116, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "طراحی و اجرای فضای سبز", 1000000, 16 },
                    { 117, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "روف گاردن", 1000000, 16 },
                    { 118, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "هرس درختان", 1000000, 16 },
                    { 120, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر کولر گازی و اسپلیت", 1000000, 4 },
                    { 122, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و سرویس آبگرمکن", 1000000, 4 },
                    { 124, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر رادیاتور شوفاژ", 1000000, 4 },
                    { 125, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و نگهداری موتورخانه", 1000000, 4 },
                    { 127, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و سرویس فن کویل", 1000000, 4 },
                    { 128, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر بخاری و شومینه", 1000000, 4 },
                    { 129, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر VRF و DVR", 1000000, 4 },
                    { 130, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "بهبود الاینده‌های موتورخانه با دستگاه آنالیز", 1000000, 4 },
                    { 131, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر یخچال و فریزر", 1000000, 5 },
                    { 132, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر ماشین لباسشویی", 1000000, 5 },
                    { 133, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر اجاق گاز", 1000000, 5 },
                    { 134, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر ماشین ظرفشویی", 1000000, 5 },
                    { 135, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیرات تخصصی تلویزیون", 1000000, 5 },
                    { 136, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب تلویزیون و لوازم صوتی تصویری", 1000000, 5 },
                    { 137, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر مایکروویو و سولاردام", 1000000, 5 },
                    { 138, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر هود آشپزخانه", 1000000, 5 },
                    { 139, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر جاروبرقی", 1000000, 5 },
                    { 140, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر چرخ خیاطی", 1000000, 5 },
                    { 141, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر تردمیل", 1000000, 5 },
                    { 142, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر چایی ساز و قهوه ساز", 1000000, 5 },
                    { 143, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر دستگاه بخور", 1000000, 13 },
                    { 144, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر پنکه", 1000000, 5 },
                    { 145, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و تعمیر فر", 1000000, 5 },
                    { 146, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر اتو", 1000000, 5 },
                    { 147, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر آبمیوه گیری و مخلوط کن", 1000000, 5 },
                    { 148, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر ساندبار و اسپیکر", 1000000, 5 },
                    { 149, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر کنسول بازی", 1000000, 5 },
                    { 150, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر بخارشور", 1000000, 5 },
                    { 151, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر غذاساز و خردکن", 1000000, 5 },
                    { 152, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیرات ریش تراش و اپلیدی", 1000000, 5 },
                    { 153, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر سینمای خانگی", 1000000, 5 },
                    { 154, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر چرخ گوشت", 1000000, 5 },
                    { 155, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر رادیو و ضبط صوت", 1000000, 5 },
                    { 156, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر صندلی ماساژور", 1000000, 13 },
                    { 157, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر سرخ کن", 1000000, 5 },
                    { 158, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر بخارپز و پلوپز", 1000000, 5 },
                    { 159, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر سشوار", 1000000, 5 },
                    { 160, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر شوفاژ برقی", 1000000, 5 },
                    { 161, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ترمیم و بازسازی ظروف آشپزخانه", 1000000, 5 },
                    { 162, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر کامپیوتر و لپ تاپ", 1000000, 7 },
                    { 163, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر ماشین‌های اداری", 1000000, 7 },
                    { 164, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پشتیبانی شبکه و سرور", 1000000, 7 },
                    { 165, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مودم و اینترنت", 1000000, 7 },
                    { 166, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "طراحی سایت و لوگو", 1000000, 7 },
                    { 167, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب و راه‌اندازی VoIP", 1000000, 7 },
                    { 168, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر دستگاه کارتخوان و بارکدخوان", 1000000, 7 },
                    { 169, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات تاچ و ال سی دی", 1000000, 8 },
                    { 170, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات باتری", 1000000, 8 },
                    { 171, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات عیب‌یابی و تعمیر برد", 1000000, 8 },
                    { 172, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات نرم افزاری", 1000000, 8 },
                    { 173, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مشاوره خرید موبایل و کالاهای دیجیتال", 1000000, 8 },
                    { 174, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات اسپیکر", 1000000, 8 },
                    { 175, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات فریم و قاب", 1000000, 8 },
                    { 176, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات دوربین", 1000000, 8 },
                    { 177, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات سنسور", 1000000, 8 },
                    { 178, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اسباب کشی با خاور و کامیون", 1000000, 17 },
                    { 179, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اسباب کشی و حمل بار بین شهری", 1000000, 17 },
                    { 180, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اسباب کشی با وانت و نیسان", 1000000, 17 },
                    { 181, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس بسته بندی", 1000000, 17 },
                    { 182, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کارگر جا به جایی", 1000000, 17 },
                    { 183, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "جا به جایی گاو صندوق", 1000000, 17 },
                    { 184, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "حمل نخاله و ضایعات ساختمانی", 1000000, 17 },
                    { 185, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اسباب کشی شرکت ها و سازمان ها", 1000000, 17 },
                    { 186, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خرید ملزومات بسته بندی", 1000000, 17 },
                    { 187, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شارژ گاز کولر ماشین", 1000000, 18 },
                    { 188, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعویض باتری خودرو", 1000000, 18 },
                    { 189, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "امداد خودرو", 1000000, 18 },
                    { 190, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "برق و باتری خودرو", 1000000, 18 },
                    { 191, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مکانیگی خودرو", 1000000, 18 },
                    { 192, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تست دیاگ و ریمپ ECU خودرو", 1000000, 18 },
                    { 193, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "حمل خودرو", 1000000, 18 },
                    { 194, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعویض روغن خودرو", 1000000, 18 },
                    { 195, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پنچرگیری و تعویض لاستیک", 1000000, 18 },
                    { 196, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کارشناسی خودرو", 1000000, 18 },
                    { 197, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعویض لنت خودرو", 1000000, 18 },
                    { 198, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعویض شمع و وایر خودرو", 1000000, 18 },
                    { 199, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کاهش مصرف سوخت", 1000000, 18 },
                    { 200, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس دوره ای گیربکس اتوماتیک", 1000000, 18 },
                    { 201, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب GPS خودرو", 1000000, 18 },
                    { 202, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب دزدگیر خودرو", 1000000, 18 },
                    { 203, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سپرسازی و جوش پلاستیک", 1000000, 18 },
                    { 204, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعویض شیشه خودرو", 1000000, 18 },
                    { 205, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر موتورسیکلت", 1000000, 18 },
                    { 206, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سوخت رسانی", 1000000, 18 },
                    { 207, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ساخت سوئیچ و ریموت خودرو در محل", 1000000, 18 },
                    { 208, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تعمیر و تعویض چراغ خودرو", 1000000, 18 },
                    { 209, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرامیک خودرو", 1000000, 19 },
                    { 210, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کارواش نانو", 1000000, 19 },
                    { 211, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کارواش با آب", 1000000, 19 },
                    { 212, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "واکس و پولیش خودرو", 1000000, 19 },
                    { 213, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "صفرشویی خودرو", 1000000, 19 },
                    { 214, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "موتورشویی خودرو", 1000000, 19 },
                    { 215, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پکیج کارواش VIP", 1000000, 19 },
                    { 216, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شفاف سازی چراغ خودرو", 1000000, 19 },
                    { 217, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "احیای رنگ خودرو", 1000000, 19 },
                    { 218, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "صافکاری و نقاشی خودرو", 1000000, 19 },
                    { 219, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "نصب شیشه دودی خودرو در محل", 1000000, 19 },
                    { 220, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات شرکتی ویژه شرکت ها کوچک و متوسط", 1000000, 20 },
                    { 221, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات شرکتی ویژه سازمان های بزرگ", 1000000, 20 },
                    { 222, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات ناخن", 1000000, 21 },
                    { 223, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات ویژه ناخن", 1000000, 21 },
                    { 224, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اصلاح صورت و ابرو بانوان", 1000000, 21 },
                    { 225, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اپیلاسیون بانوان در خانه", 1000000, 21 },
                    { 226, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "براشینگ مو بانوان", 1000000, 21 },
                    { 227, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "رنگ مو بانوان", 1000000, 21 },
                    { 228, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مش ، لایت ، بالیاژ و آمبره بانوان", 1000000, 21 },
                    { 229, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "لیفت و لمینت مژه و ابرو بانوان", 1000000, 21 },
                    { 230, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کاشت و اکستنشن مژه بانوان در خانه", 1000000, 21 },
                    { 231, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پاکسازی و لایه برداری پوست بانوان", 1000000, 21 },
                    { 232, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "شینیون مو بانوان در خانه", 1000000, 21 },
                    { 233, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "آرایش صورت بانوان در خانه", 1000000, 21 },
                    { 234, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "بافت مو بانوان در خانه", 1000000, 21 },
                    { 235, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اکستنشن مو بانوان در خانه", 1000000, 21 },
                    { 236, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "میکروپیمنتیشن و میکروبلیدینگ بانوان", 1000000, 21 },
                    { 237, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کوتاهی مو بانوان", 1000000, 21 },
                    { 238, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "درمان سرپایی در محل", 1000000, 23 },
                    { 239, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "تزریقات در منزل", 1000000, 23 },
                    { 240, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پرستاری و مراقبت بیمار", 1000000, 23 },
                    { 241, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پرستاری و مراقبت سالمند", 1000000, 23 },
                    { 242, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "آزمایش و نمونه گیری در منزل", 1000000, 23 },
                    { 243, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "ICU در منزل", 1000000, 23 },
                    { 244, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "معاینه پزشکی", 1000000, 23 },
                    { 245, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "فیزیوتراپی در منزل", 1000000, 23 },
                    { 246, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "اصلاح سر و صورت آقایان", 1000000, 22 },
                    { 247, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "سرویس ماهانه پیرایش اقایان", 1000000, 22 },
                    { 248, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات ناخن آقایان", 1000000, 22 },
                    { 249, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "مراقبت و زیبایی اقایان", 1000000, 22 },
                    { 250, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "گریم داماد", 1000000, 22 },
                    { 251, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "هتل های حیوانات خانگی", 1000000, 24 },
                    { 252, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات دامپزشکی در محل", 1000000, 24 },
                    { 253, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات تربیتی حیوانات خانگی", 1000000, 24 },
                    { 254, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "خدمات شستشو و آرایش حیوانات خانگی", 1000000, 24 },
                    { 255, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "پت شاپ", 1000000, 24 },
                    { 256, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کلاس سی ایکس در خانه", 1000000, 25 },
                    { 257, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "برنامه ورزشی و تغذیه", 1000000, 25 },
                    { 258, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کلاس یوگا در خانه", 1000000, 25 },
                    { 259, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کلاس پیلاتس در خانه", 1000000, 25 },
                    { 260, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "حرکات اصلاحی", 1000000, 25 },
                    { 261, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "کراتینه و ویتامینه مو بانوان", 1000000, 21 },
                    { 262, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "قالیشویی", 1000000, 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "Description", "DoneAt", "ExpertId", "Image", "IsDeleted", "RequesteForTime", "ServiceId", "Status", "Title" },
                values: new object[] { 1, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "نظافت خونه صد متری یه طور کامل", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, false, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "نظافت" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ExpertId",
                table: "Addresses",
                column: "ExpertId",
                unique: true,
                filter: "[ExpertId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AppUserId",
                table: "Admins",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExpertId",
                table: "Comments",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AppUserId",
                table: "Customers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experts_AppUserId",
                table: "Experts",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertService_ServicesId",
                table: "ExpertService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ExpertId",
                table: "Orders",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SubCategoryId",
                table: "Services",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ExpertId",
                table: "Suggestions",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_OrderId",
                table: "Suggestions",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ExpertService");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
