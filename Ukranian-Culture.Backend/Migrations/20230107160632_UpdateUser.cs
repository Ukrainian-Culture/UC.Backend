using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "169a9df2-231c-45e8-9a0a-c7333f0dc9f4", 0, "e3af51c8-5134-4640-b777-c767938add25", null, false, "Vadym", "Orlov", false, null, null, null, null, null, false, "c02d0817-bd3c-405f-8fd5-25042059894c", false, null },
                    { "87d76511-8b74-4250-aef1-c47b8cb9308f", 0, "0d8e6e17-c8f8-4687-ab75-4bbad13b82e3", null, false, "Bohdan", "Vivchar", false, null, null, null, null, null, false, "ca39f882-c929-4804-bed0-eef0bc50c5b7", false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "169a9df2-231c-45e8-9a0a-c7333f0dc9f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d76511-8b74-4250-aef1-c47b8cb9308f");
        }
    }
}
