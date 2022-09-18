using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeData.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Created", "IsDeleted", "Modified", "Name" },
                values: new object[] { 1, new DateTime(2022, 9, 18, 16, 44, 53, 102, DateTimeKind.Local).AddTicks(8140), false, null, "developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
