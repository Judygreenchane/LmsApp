using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityType_ActivityTypeId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityType",
                table: "ActivityType");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fc09f71-2c7e-434f-8eef-01751ede761d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6983cd3e-a7ad-4ceb-8ab1-402173a29f93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84db07a9-3379-4d5a-bee8-d8f14458b5ed");

            migrationBuilder.RenameTable(
                name: "ActivityType",
                newName: "ActivityTypes");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "Modules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Courses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Activities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActivityTypeId",
                table: "ActivityTypes",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTypes",
                table: "ActivityTypes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39f72ca2-db9f-4f6a-875f-3c5794cd802c", null, "Student", "STUDENT" },
                    { "70925482-58b7-4fe0-980d-ef45d51ee4a1", null, "Admin", "ADMIN" },
                    { "f4e963c1-d0a3-4040-acce-1ef19efb01cd", null, "Teacher", "TEACHER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityTypes_ActivityTypeId",
                table: "Activities",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityTypes_ActivityTypeId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTypes",
                table: "ActivityTypes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39f72ca2-db9f-4f6a-875f-3c5794cd802c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70925482-58b7-4fe0-980d-ef45d51ee4a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4e963c1-d0a3-4040-acce-1ef19efb01cd");

            migrationBuilder.RenameTable(
                name: "ActivityTypes",
                newName: "ActivityType");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Modules",
                newName: "ModuleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Courses",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Activities",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ActivityType",
                newName: "ActivityTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityType",
                table: "ActivityType",
                column: "ActivityTypeId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fc09f71-2c7e-434f-8eef-01751ede761d", null, "Teacher", "TEACHER" },
                    { "6983cd3e-a7ad-4ceb-8ab1-402173a29f93", null, "Student", "STUDENT" },
                    { "84db07a9-3379-4d5a-bee8-d8f14458b5ed", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityType_ActivityTypeId",
                table: "Activities",
                column: "ActivityTypeId",
                principalTable: "ActivityType",
                principalColumn: "ActivityTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
