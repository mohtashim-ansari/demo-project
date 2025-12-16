using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dsr_web_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoleWisePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PageName", "RoleId" },
                values: new object[,]
                {
                    { 1, "Home", 1 },
                    { 2, "Attendance", 1 },
                    { 3, "Search", 1 },
                    { 4, "DSR", 1 },
                    { 5, "Registration", 1 },
                    { 6, "Home", 2 },
                    { 7, "Attendance", 2 },
                    { 8, "Search", 2 },
                    { 9, "DSR", 2 },
                    { 10, "Home", 2 },
                    { 11, "Attendance", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");
        }
    }
}
