using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class UserHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfWatch = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UsersHistories_UserId",
                table: "UsersHistories",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersHistories");

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
    }
}
