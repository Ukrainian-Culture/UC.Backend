using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addEndTimeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SubscriptionEndDate" },
                values: new object[] { "a39e7ab6-0c2b-4f29-a10f-504f1d4cc4e4", "AQAAAAEAACcQAAAAEGRmvvRdVL1wStHB41P/aIcahM2Cp4i6aRGjNKf6T7+7JhyCDUPLIUAaThizg17Ufg==", "5be49770-600e-444d-aa00-0f37a7417508", new DateTime(2023, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "SubscriptionEndDate" },
                values: new object[] { "3445dbe1-ba63-4777-94e8-c82b281846aa", "AQAAAAEAACcQAAAAEDnvOohFLogPdnGw5sqt5F0OV0iZK7hIBgmBKlULOcjd9FB1Tk5CACd1cXB+taPMSg==", "50962a28-979a-4d95-b9dd-8b980bd4244f", new DateTime(2023, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionEndDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39bc3e40-88e8-46c1-9833-ee04e38d844d", "AQAAAAEAACcQAAAAENvgupRgqiG+VpGbQs30P39RWdxVF3iz+/cWEQsz3lWCy5qVFsy+MYEzmNuGC7Fj3w==", "98fc4985-688f-4750-9bd7-f8a0376cdd0b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5ceec0d-30bc-4585-81cd-35d3d7c38711", "AQAAAAEAACcQAAAAEHusFr0HNS4VUfGGUsdJnKF2xWAi7Ysx3vngb1WYFj7hHClAJ0HJPZq0i97QnBitPw==", "1c1764fd-e02c-4aa1-ba6f-2ecf35f36385" });
        }
    }
}
