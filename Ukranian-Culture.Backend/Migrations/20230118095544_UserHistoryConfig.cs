using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UserHistoryConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                column: "ConcurrencyStamp",
                value: "1299be31-1ec4-42ef-988b-261440426393");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                column: "ConcurrencyStamp",
                value: "43011e9e-51a3-4c8d-8163-f5968d48e71d");

            migrationBuilder.InsertData(
                table: "UsersHistories",
                columns: new[] { "Id", "DateOfWatch", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("9d2abe54-d8fb-45eb-94a0-65cefcbfa432"), new DateTime(2023, 1, 18, 1, 3, 1, 0, DateTimeKind.Utc), "About Ivan Mazepa", new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f") },
                    { new Guid("c5a0e131-46a0-4f37-9a9d-6e426cb94f46"), new DateTime(2023, 1, 18, 1, 1, 1, 1, DateTimeKind.Utc), "About Bohdan Khmelnytsky", new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersHistories",
                keyColumn: "Id",
                keyValue: new Guid("9d2abe54-d8fb-45eb-94a0-65cefcbfa432"));

            migrationBuilder.DeleteData(
                table: "UsersHistories",
                keyColumn: "Id",
                keyValue: new Guid("c5a0e131-46a0-4f37-9a9d-6e426cb94f46"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                column: "ConcurrencyStamp",
                value: "b22ade2e-1345-42eb-b436-5654e2b7a23c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                column: "ConcurrencyStamp",
                value: "c6ed41ec-11c3-4280-b600-0aa0231a8740");
        }
    }
}
