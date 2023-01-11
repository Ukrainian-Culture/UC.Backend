using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class changeArticleLocalesIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("0a2e4bf1-ce88-4008-8e7b-ad6855572a6d") });

            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("0a2e4bf1-ce88-4008-8e7b-ad6855572a6d") });

            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("e847e218-1be2-40c2-9d44-d4c93bbf493b") });

            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("e847e218-1be2-40c2-9d44-d4c93bbf493b") });

            migrationBuilder.InsertData(
                table: "ArticlesLocales",
                columns: new[] { "CultureId", "Id", "Content", "ShortDescription", "SubText", "Title" },
                values: new object[,]
                {
                    { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"), "Про Івана Мазепу .... ", "Про Івана Мазепу", "Про Івана Мазепу", "Про Івана Мазепу" },
                    { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"), "About Ivan Mazepa .... ", "About Ivan Mazepa", "About Ivan Mazepa", "About Ivan Mazepa" },
                    { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"), "Про Богдана Хмельницького .... ", "Про Богдана Хмельницького", "Про Богдана Хмельницького", "Про Богдана Хмельницького" },
                    { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"), "About Bohdan Khmelnytsky .... ", "About Bohdan Khmelnytsky", "About Bohdan Khmelnytsky", "About Bohdan Khmelnytsky" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                column: "ConcurrencyStamp",
                value: "bb591619-3a25-44ad-add3-d1c0389313a6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                column: "ConcurrencyStamp",
                value: "2a2f6e3b-f47b-4b07-828f-c5b5068cafab");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53") });

            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53") });

            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203") });

            migrationBuilder.DeleteData(
                table: "ArticlesLocales",
                keyColumns: new[] { "CultureId", "Id" },
                keyValues: new object[] { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203") });

            migrationBuilder.InsertData(
                table: "ArticlesLocales",
                columns: new[] { "CultureId", "Id", "Content", "ShortDescription", "SubText", "Title" },
                values: new object[,]
                {
                    { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("0a2e4bf1-ce88-4008-8e7b-ad6855572a6d"), "Про Івана Мазепу .... ", "Про Івана Мазепу", "Про Івана Мазепу", "Про Івана Мазепу" },
                    { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("0a2e4bf1-ce88-4008-8e7b-ad6855572a6d"), "About Ivan Mazepa .... ", "About Ivan Mazepa", "About Ivan Mazepa", "About Ivan Mazepa" },
                    { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), new Guid("e847e218-1be2-40c2-9d44-d4c93bbf493b"), "Про Богдана Хмельницького .... ", "Про Богдана Хмельницького", "Про Богдана Хмельницького", "Про Богдана Хмельницького" },
                    { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), new Guid("e847e218-1be2-40c2-9d44-d4c93bbf493b"), "About Bohdan Khmelnytsky .... ", "About Bohdan Khmelnytsky", "About Bohdan Khmelnytsky", "About Bohdan Khmelnytsky" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                column: "ConcurrencyStamp",
                value: "5ad7a89d-356a-465f-9da1-0c657d48f80a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                column: "ConcurrencyStamp",
                value: "503a516f-c5e5-4e85-9a48-5856f1f3f1da");
        }
    }
}
