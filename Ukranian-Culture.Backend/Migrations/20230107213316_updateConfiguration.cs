using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "169a9df2-231c-45e8-9a0a-c7333f0dc9f4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a385a9fa-3f6b-4348-ba48-2bfe69bcac5f", "3314c092-778b-42a7-9bea-c8863699d398" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d76511-8b74-4250-aef1-c47b8cb9308f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28bb76b1-4142-47ad-9633-2a60933830d6", "6925a4905d02cc4c26872e1813a0a5f2", "1df0335e-b26a-4c32-959b-9e4c9ba3a84e" });

            migrationBuilder.InsertData(
                table: "Categories",
                column: "Id",
                values: new object[]
                {
                    new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"),
                    new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee")
                });

            migrationBuilder.InsertData(
                table: "Cultures",
                columns: new[] { "Id", "DisplayedName", "Name" },
                values: new object[,]
                {
                    { new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), "Ukrainian", "ua" },
                    { new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), "English", "en" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Date", "Region", "Type" },
                values: new object[,]
                {
                    { new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"), new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"), new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kyiv", "file" },
                    { new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"), new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"), new DateTime(1886, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hmelnytsk", "file" }
                });

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

            migrationBuilder.InsertData(
                table: "CategoryLocales",
                columns: new[] { "CategoryId", "CultureId", "Name" },
                values: new object[,]
                {
                    { new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"), new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), "Їжа" },
                    { new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"), new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), "Food" },
                    { new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"), new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"), "Люди" },
                    { new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"), new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"), "People" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"));

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

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"));

            migrationBuilder.DeleteData(
                table: "CategoryLocales",
                keyColumns: new[] { "CategoryId", "CultureId" },
                keyValues: new object[] { new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"), new Guid("0a315a0f-4860-4302-bb79-dec86e87d378") });

            migrationBuilder.DeleteData(
                table: "CategoryLocales",
                keyColumns: new[] { "CategoryId", "CultureId" },
                keyValues: new object[] { new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"), new Guid("4fd5d8c1-f34b-4824-b252-69910285e681") });

            migrationBuilder.DeleteData(
                table: "CategoryLocales",
                keyColumns: new[] { "CategoryId", "CultureId" },
                keyValues: new object[] { new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"), new Guid("0a315a0f-4860-4302-bb79-dec86e87d378") });

            migrationBuilder.DeleteData(
                table: "CategoryLocales",
                keyColumns: new[] { "CategoryId", "CultureId" },
                keyValues: new object[] { new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"), new Guid("4fd5d8c1-f34b-4824-b252-69910285e681") });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"));

            migrationBuilder.DeleteData(
                table: "Cultures",
                keyColumn: "Id",
                keyValue: new Guid("0a315a0f-4860-4302-bb79-dec86e87d378"));

            migrationBuilder.DeleteData(
                table: "Cultures",
                keyColumn: "Id",
                keyValue: new Guid("4fd5d8c1-f34b-4824-b252-69910285e681"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "169a9df2-231c-45e8-9a0a-c7333f0dc9f4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3642f925-cb9b-4c3e-954c-044281435248", "e047c5dc-7e7e-4fa2-9925-7b064b28b450" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d76511-8b74-4250-aef1-c47b8cb9308f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63715815-d3e6-4ec2-b0b0-a1e82a153e74", "6925a4905d02cc4c26872e1913a0a5f2", "a0ceefcd-1bc4-4abc-a874-a543301a0ce5" });
        }
    }
}
