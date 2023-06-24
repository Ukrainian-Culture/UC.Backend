using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addNameTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "169a9df2-231c-45e8-9a0a-c7333f0dc9f4",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f2dfa596-0b21-4613-bfae-8583ad63da8a", "Vadym@gmail.com", "VADYM@GMAIL.COM", "VADYM", "6925a4905d02cc4c26872e1713a0a5f2", "dfb7e0b1-c388-4665-a7c8-da878c236441", "Vadym" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d76511-8b74-4250-aef1-c47b8cb9308f",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "5ce183de-6dc3-447c-a2d5-c3d5221bdc0c", "Bohdan@gmail.com", "BOHDAN@GMAIL.COM", "BOHDAN", "800b682e617c823e5d2458115bd291c4", "9d444150-5a60-4039-85b1-2e752c6f0764", "Bohdan" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Name");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "169a9df2-231c-45e8-9a0a-c7333f0dc9f4",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e3af51c8-5134-4640-b777-c767938add25", null, null, null, null, "c02d0817-bd3c-405f-8fd5-25042059894c", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87d76511-8b74-4250-aef1-c47b8cb9308f",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "0d8e6e17-c8f8-4687-ab75-4bbad13b82e3", null, null, null, null, "ca39f882-c929-4804-bed0-eef0bc50c5b7", null });
        }
    }
}
