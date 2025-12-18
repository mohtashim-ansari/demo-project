using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dsr_web_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatFirstName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirtName",
                table: "UsersInfos",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "UsersInfos",
                newName: "FirtName");
        }
    }
}
