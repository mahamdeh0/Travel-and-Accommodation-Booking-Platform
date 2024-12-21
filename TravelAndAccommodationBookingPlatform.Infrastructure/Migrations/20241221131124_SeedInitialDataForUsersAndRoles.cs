using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialDataForUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Roles");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("62d4e5fd-f212-4f17-19c8-08dd21b8d161"), "Guest" },
                    { new Guid("6979da61-a3ba-42de-ab1a-08dd21b746d6"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber" },
                values: new object[] { new Guid("7e754e75-d677-4483-57bd-08dd21b65a13"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@Test.com", "Admin", "Admin", "AEO5MEnY6njK7M2UYW6K49qb+MqiU5uGFirzMZ/8d39QAiqJ9S9jdn/Qbe4mnZP4tg==", "0569345887" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("6979da61-a3ba-42de-ab1a-08dd21b746d6"), new Guid("7e754e75-d677-4483-57bd-08dd21b65a13") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("62d4e5fd-f212-4f17-19c8-08dd21b8d161"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6979da61-a3ba-42de-ab1a-08dd21b746d6"), new Guid("7e754e75-d677-4483-57bd-08dd21b65a13") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6979da61-a3ba-42de-ab1a-08dd21b746d6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e754e75-d677-4483-57bd-08dd21b65a13"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
