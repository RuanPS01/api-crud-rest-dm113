using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_crud_rest_dm113.Migrations
{
    /// <inheritdoc />
    public partial class RenamingUpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updateAt",
                table: "Messages",
                newName: "updatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Messages",
                newName: "updateAt");
        }
    }
}
