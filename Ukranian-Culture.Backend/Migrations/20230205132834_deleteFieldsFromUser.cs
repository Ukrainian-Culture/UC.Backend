using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class deleteFieldsFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d5aa0ba-e298-47c6-82e6-8579d8eef825", "AQAAAAEAACcQAAAAEI45gWwwEJ4s6ogTf1c/m3Ky42oEuJeVvUB+Yp3hJuK74ASOmew9d3qBq4qA53P5UA==", "76004976-4518-4c9f-b6db-3537f05ceb7a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aacf9056-4351-46ef-bf55-1cc4595af262", "AQAAAAEAACcQAAAAEIFpwcL9s/pQ3v8Jkag0C536aqgEhtJ92oYb4869lKb581QvZFrJVbjKJhLTR4nUrA==", "54512474-5c70-449b-8e3c-eaeb6b40cfde" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4390213f-6f7f-46db-92e9-0e9dff8caa0f", "Admin", "Admin", "AQAAAAEAACcQAAAAECgYOplXdzsrXffvXJ1VXhuKtynvuiXWZIO9ToYNmzL4aTeBBo8ug4SHa78sjm6dJA==", "17372ac6-3da3-4513-9930-771703c101c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78b12075-8c61-469d-a294-13f5bd3aa1f0", "Bohdan", "Vivchar", "AQAAAAEAACcQAAAAEJh5Nyr+TLqJDHGwKxiKLsujc+xGqwk6RA5VR7FnHtDa03CgIVykjIAW/18UZl0LrQ==", "c14c6975-f688-47bd-a83b-b19c807da2de" });
        }
    }
}
