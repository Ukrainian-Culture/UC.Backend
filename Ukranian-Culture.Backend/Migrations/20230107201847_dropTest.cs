using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class dropTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ArticlesLocales");

            migrationBuilder.DropTable(
                name: "CategoryLocales");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "169a9df2-231c-45e8-9a0a-c7333f0dc9f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d76511-8b74-4250-aef1-c47b8cb9308f");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticlesLocales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CultureId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesLocales", x => new { x.Id, x.CultureId });
                    table.ForeignKey(
                        name: "FK_ArticlesLocales_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLocales",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CultureId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLocales", x => new { x.CategoryId, x.CultureId });
                    table.ForeignKey(
                        name: "FK_CategoryLocales_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "169a9df2-231c-45e8-9a0a-c7333f0dc9f4", 0, "f2dfa596-0b21-4613-bfae-8583ad63da8a", "Vadym@gmail.com", false, "Vadym", "Orlov", false, null, "VADYM@GMAIL.COM", "VADYM", "6925a4905d02cc4c26872e1713a0a5f2", null, false, "dfb7e0b1-c388-4665-a7c8-da878c236441", false, "Vadym" },
                    { "87d76511-8b74-4250-aef1-c47b8cb9308f", 0, "5ce183de-6dc3-447c-a2d5-c3d5221bdc0c", "Bohdan@gmail.com", false, "Bohdan", "Vivchar", false, null, "BOHDAN@GMAIL.COM", "BOHDAN", "800b682e617c823e5d2458115bd291c4", null, false, "9d444150-5a60-4039-85b1-2e752c6f0764", false, "Bohdan" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Name" },
                    { 2, "Name" }
                });

            migrationBuilder.InsertData(
                table: "Cultures",
                columns: new[] { "Id", "DisplayedName", "Name" },
                values: new object[,]
                {
                    { 1, "English", "en" },
                    { 2, "Ukrainian", "ua" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Date", "Region", "Type" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1886, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hmelnytsk", "file" },
                    { 2, 1, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kyiv", "file" }
                });

            migrationBuilder.InsertData(
                table: "ArticlesLocales",
                columns: new[] { "CultureId", "Id", "Content", "ShortDescription", "SubText", "Title" },
                values: new object[,]
                {
                    { 1, 1, "About Bohdan Khmelnytsky .... ", "About Bohdan Khmelnytsky", "About Bohdan Khmelnytsky", "About Bohdan Khmelnytsky" },
                    { 2, 1, "Про Богдана Хмельницького .... ", "Про Богдана Хмельницького", "Про Богдана Хмельницького", "Про Богдана Хмельницького" },
                    { 1, 2, "About Ivan Mazepa .... ", "About Ivan Mazepa", "About Ivan Mazepa", "About Ivan Mazepa" },
                    { 2, 2, "Про Івана Мазепу .... ", "Про Івана Мазепу", "Про Івана Мазепу", "Про Івана Мазепу" }
                });

            migrationBuilder.InsertData(
                table: "CategoryLocales",
                columns: new[] { "CategoryId", "CultureId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "People" },
                    { 1, 2, "Люди" },
                    { 2, 1, "Food" },
                    { 2, 2, "Їжа" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesLocales_CultureId",
                table: "ArticlesLocales",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLocales_CultureId",
                table: "CategoryLocales",
                column: "CultureId");
        }
    }
}
