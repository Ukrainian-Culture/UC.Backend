using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("431f29e9-13ff-4f5f-b178-511610d5103f"), new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "d299d51c-0a3c-48ca-a77d-a46b7b4e277c", "Admin@gmail.com", "Admin", "Admin", "ADMIN@GMAIL.COM", "ADMIN", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                column: "ConcurrencyStamp",
                value: "5ab7d164-cfc0-45f5-9259-341d85c632b8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("431f29e9-13ff-4f5f-b178-511610d5103f"), new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "UserName" },
                values: new object[] { "bb591619-3a25-44ad-add3-d1c0389313a6", "Vadym@gmail.com", "Vadym", "Orlov", "VADYM@GMAIL.COM", "VADYM", "Vadym" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                column: "ConcurrencyStamp",
                value: "2a2f6e3b-f47b-4b07-828f-c5b5068cafab");
        }
    }
}
