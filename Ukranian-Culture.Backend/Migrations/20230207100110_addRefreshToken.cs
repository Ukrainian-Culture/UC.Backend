using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "4a5aa023-c764-4f9f-8997-f6d010fa4fb9", "AQAAAAEAACcQAAAAEPEW6C/uDuC9QCStAzbptfQZr3mtN/uh+iSAghdgYFzF0VEaW76VHAuRITqzkBJKTw==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2b731045-de82-4828-b183-6fec80870851" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "723a2ec5-f029-4395-8869-6419512690b8", "AQAAAAEAACcQAAAAEEmRFqpT5cIvi79VjeuE0X1JlnJ4F2ACDO/296Wk0LpyZTyp5QkF3gl21mobTr+hVw==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e94292ae-f295-4d57-8139-099fcd83c447" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
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
    }
}
