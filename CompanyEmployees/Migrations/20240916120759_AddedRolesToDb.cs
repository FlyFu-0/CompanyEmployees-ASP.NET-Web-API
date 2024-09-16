using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "434f6556-036d-4096-bb69-77a00a22cc5d", "0d3775c9-72c5-445b-afe1-3231ae7606f1", "Administrator", "ADMINISTRATOR" },
                    { "d501ee25-3689-42d3-b19d-78c576e88e50", "42b95525-d798-4e2c-8e95-adae39569cf4", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "434f6556-036d-4096-bb69-77a00a22cc5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d501ee25-3689-42d3-b19d-78c576e88e50");
        }
    }
}
