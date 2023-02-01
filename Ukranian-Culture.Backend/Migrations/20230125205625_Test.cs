using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryLocales");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4390213f-6f7f-46db-92e9-0e9dff8caa0f", "AQAAAAEAACcQAAAAECgYOplXdzsrXffvXJ1VXhuKtynvuiXWZIO9ToYNmzL4aTeBBo8ug4SHa78sjm6dJA==", "17372ac6-3da3-4513-9930-771703c101c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78b12075-8c61-469d-a294-13f5bd3aa1f0", "AQAAAAEAACcQAAAAEJh5Nyr+TLqJDHGwKxiKLsujc+xGqwk6RA5VR7FnHtDa03CgIVykjIAW/18UZl0LrQ==", "c14c6975-f688-47bd-a83b-b19c807da2de" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd"),
                column: "Name",
                value: "People");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee"),
                column: "Name",
                value: "Music");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CategoryLocales",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CultureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLocales", x => new { x.CategoryId, x.CultureId });
                    table.ForeignKey(
                        name: "FK_CategoryLocales_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                column: "Type",
                value: "file");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                column: "Type",
                value: "file");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6109506-85bd-4ca0-866f-a68f8dd210e2", "AQAAAAEAACcQAAAAEAorz7O5EtCaCDaKOJvzxLiU0ruiHmySYe47ZXjbnKNayhe0TKAf3FXg0ikB35caGg==", "c3372fce-ce4d-4093-b7da-3fbf58c74512" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55b33ca8-05e8-404e-a8ad-730c65fa3f5b", "AQAAAAEAACcQAAAAEGqjglrWwK2Z8q/VoAm1q1q1yVfFLbQMjLX0c90BuOnI5+14Edbu00MQ6pP5b2j6hw==", "960afcbc-bed1-4887-886b-b629a4be9816" });

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

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLocales_CultureId",
                table: "CategoryLocales",
                column: "CultureId");
        }
    }
}
