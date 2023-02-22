using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UkranianCulture.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("5adbec33-97c5-4041-be6a-e0f3d3ca6f44"), new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cdd05d11-c333-4e3c-b99d-c0e7367ea08e", true, "AQAAAAEAACcQAAAAECMW2GX8+lQzxf9nXf/dXtaqG+r4qI65OOrz6EO2wCIB7VdHUDw4cg2ojAelxTtrxA==", "017886c9-f946-4fa4-b888-a83bdfbf56fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd037157-ae05-4583-9220-a3ec8cf11cdf", true, "AQAAAAEAACcQAAAAEOse8rmrcBBpsyE13SclchiuNV8nja2hUaZE0poo9Mwt3lBWp/z3d1KR4CgsDTex/Q==", "f63ebafe-942a-499f-9c9c-c0bca2d190ba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5adbec33-97c5-4041-be6a-e0f3d3ca6f44"), new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a39e7ab6-0c2b-4f29-a10f-504f1d4cc4e4", false, "AQAAAAEAACcQAAAAEGRmvvRdVL1wStHB41P/aIcahM2Cp4i6aRGjNKf6T7+7JhyCDUPLIUAaThizg17Ufg==", "5be49770-600e-444d-aa00-0f37a7417508" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3445dbe1-ba63-4777-94e8-c82b281846aa", false, "AQAAAAEAACcQAAAAEDnvOohFLogPdnGw5sqt5F0OV0iZK7hIBgmBKlULOcjd9FB1Tk5CACd1cXB+taPMSg==", "50962a28-979a-4d95-b9dd-8b980bd4244f" });
        }
    }
}
