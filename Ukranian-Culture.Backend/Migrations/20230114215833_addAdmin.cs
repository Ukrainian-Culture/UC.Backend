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
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "a6109506-85bd-4ca0-866f-a68f8dd210e2", "Admin@gmail.com", "Admin", "Admin", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEAorz7O5EtCaCDaKOJvzxLiU0ruiHmySYe47ZXjbnKNayhe0TKAf3FXg0ikB35caGg==", "c3372fce-ce4d-4093-b7da-3fbf58c74512", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55b33ca8-05e8-404e-a8ad-730c65fa3f5b", "AQAAAAEAACcQAAAAEGqjglrWwK2Z8q/VoAm1q1q1yVfFLbQMjLX0c90BuOnI5+14Edbu00MQ6pP5b2j6hw==", "960afcbc-bed1-4887-886b-b629a4be9816" });
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
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "bb591619-3a25-44ad-add3-d1c0389313a6", "Vadym@gmail.com", "Vadym", "Orlov", "VADYM@GMAIL.COM", "VADYM", "6925a4905d02cc4c26872e1713a0a5f2", null, "Vadym" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a2f6e3b-f47b-4b07-828f-c5b5068cafab", "6925a4905d02cc4c26872e1813a0a5f2", null });
        }
    }
}
