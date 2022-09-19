using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MangerId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "BirthDate", "CreatedAt", "Email", "MangerId", "MobileNo", "Name" },
                values: new object[] { 1, "Cairo", new DateTime(1987, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 16, 18, 56, 45, 369, DateTimeKind.Local).AddTicks(6120), "JohnDoeManager@gmail.com", null, "01050460225", "John Doe" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_MangerId",
                table: "Employees",
                column: "MangerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_MangerId",
                table: "Employees",
                column: "MangerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_MangerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_MangerId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MangerId",
                table: "Employees");
        }
    }
}
