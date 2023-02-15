using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class changeUserHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "UsersHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "UsersHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "UsersHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubText",
                table: "UsersHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "UsersHistories",
                keyColumn: "Id",
                keyValue: new Guid("9d2abe54-d8fb-45eb-94a0-65cefcbfa432"),
                columns: new[] { "ArticleId", "Category", "Region", "SubText" },
                values: new object[] { new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"), "People", "Kyiv", "text2" });

            migrationBuilder.UpdateData(
                table: "UsersHistories",
                keyColumn: "Id",
                keyValue: new Guid("c5a0e131-46a0-4f37-9a9d-6e426cb94f46"),
                columns: new[] { "ArticleId", "Category", "Region", "SubText" },
                values: new object[] { new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"), "Music", "Kyiv", "Text" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "UsersHistories");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "UsersHistories");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "UsersHistories");

            migrationBuilder.DropColumn(
                name: "SubText",
                table: "UsersHistories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a5aa023-c764-4f9f-8997-f6d010fa4fb9", "AQAAAAEAACcQAAAAEPEW6C/uDuC9QCStAzbptfQZr3mtN/uh+iSAghdgYFzF0VEaW76VHAuRITqzkBJKTw==", "2b731045-de82-4828-b183-6fec80870851" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "723a2ec5-f029-4395-8869-6419512690b8", "AQAAAAEAACcQAAAAEEmRFqpT5cIvi79VjeuE0X1JlnJ4F2ACDO/296Wk0LpyZTyp5QkF3gl21mobTr+hVw==", "e94292ae-f295-4d57-8139-099fcd83c447" });
        }
    }
}
