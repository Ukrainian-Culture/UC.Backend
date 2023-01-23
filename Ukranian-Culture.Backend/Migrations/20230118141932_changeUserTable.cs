using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class changeUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersHistories_UserId",
                table: "UsersHistories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                column: "ConcurrencyStamp",
                value: "fed2607c-e45f-4640-b5da-510404d5b9ff");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                column: "ConcurrencyStamp",
                value: "165cd1ab-886e-4e1f-b5aa-7742192b0229");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHistories_UserId",
                table: "UsersHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersHistories_UserId",
                table: "UsersHistories");

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

            migrationBuilder.CreateIndex(
                name: "IX_UsersHistories_UserId",
                table: "UsersHistories",
                column: "UserId",
                unique: true);
        }
    }
}
