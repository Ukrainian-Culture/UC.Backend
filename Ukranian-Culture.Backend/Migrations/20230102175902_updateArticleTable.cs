using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateArticleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Info_InfoId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "Info");

            migrationBuilder.DropIndex(
                name: "IX_Articles_InfoId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "ArticlesLocales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubText",
                table: "ArticlesLocales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "ShortDescription", "SubText" },
                values: new object[] { "About Bohdan Khmelnytsky", "About Bohdan Khmelnytsky" });

            migrationBuilder.UpdateData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { 2, 1 },
                columns: new[] { "ShortDescription", "SubText" },
                values: new object[] { "Про Богдана Хмельницького", "Про Богдана Хмельницького" });

            migrationBuilder.UpdateData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "ShortDescription", "SubText" },
                values: new object[] { "About Ivan Mazepa", "About Ivan Mazepa" });

            migrationBuilder.UpdateData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "ShortDescription", "SubText" },
                values: new object[] { "Про Івана Мазепу", "Про Івана Мазепу" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "ArticlesLocales");

            migrationBuilder.DropColumn(
                name: "SubText",
                table: "ArticlesLocales");

            migrationBuilder.AddColumn<int>(
                name: "InfoId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "InfoId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "InfoId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Info",
                columns: new[] { "Id", "Date", "ShortDesc", "SubText" },
                values: new object[,]
                {
                    { 1, new DateTime(1667, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "was a Ruthenian military commander and Hetman of the Zaporozhian Host", "About Bohdan Khmelnytsky" },
                    { 2, new DateTime(1687, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a was a Ukrainian military, political, and civic leader who served as the Hetman of Zaporizhian Host in 1687–1708", "About Ivan Mazepa" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_InfoId",
                table: "Articles",
                column: "InfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Info_InfoId",
                table: "Articles",
                column: "InfoId",
                principalTable: "Info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
