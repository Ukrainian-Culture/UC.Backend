using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticlesLocales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CultureId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    InfoId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Articles_Info_InfoId",
                        column: x => x.InfoId,
                        principalTable: "Info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                column: "Id",
                values: new object[]
                {
                    1,
                    2
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
                table: "Info",
                columns: new[] { "Id", "Date", "ShortDesc", "SubText" },
                values: new object[,]
                {
                    { 1, new DateTime(1667, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "was a Ruthenian military commander and Hetman of the Zaporozhian Host", "About Bohdan Khmelnytsky" },
                    { 2, new DateTime(1687, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a was a Ukrainian military, political, and civic leader who served as the Hetman of Zaporizhian Host in 1687–1708", "About Ivan Mazepa" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "pflfoof", "Vadym", "380785774545" },
                    { 2, "maltokent", "Bohdan", "2122921001" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "InfoId", "Region", "Type" },
                values: new object[,]
                {
                    { 1, 1, 1, "hmelnytsk", "file" },
                    { 2, 1, 2, "Kyiv", "file" }
                });

            migrationBuilder.InsertData(
                table: "ArticlesLocales",
                columns: new[] { "CultureId", "Id", "Content", "Title" },
                values: new object[,]
                {
                    { 1, 1, "About Bohdan Khmelnytsky .... ", "About Bohdan Khmelnytsky" },
                    { 2, 1, "Про Богдана Хмельницького .... ", "Про Богдана Хмельницького" },
                    { 1, 2, "About Ivan Mazepa .... ", "About Ivan Mazepa" },
                    { 2, 2, "Про Івана Мазепу .... ", "Про Івана Мазепу" }
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
                name: "IX_Articles_InfoId",
                table: "Articles",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesLocales_CultureId",
                table: "ArticlesLocales",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLocales_CultureId",
                table: "CategoryLocales",
                column: "CultureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ArticlesLocales");

            migrationBuilder.DropTable(
                name: "CategoryLocales");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Info");

            migrationBuilder.DropTable(
                name: "Cultures");
        }
    }
}
