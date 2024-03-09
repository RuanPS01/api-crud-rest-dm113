using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_crud_rest_dm113.Migrations
{
    /// <inheritdoc />
    public partial class TypeOfTextAttAndNameRenamedToTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Messages",
                newName: "title");

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Messages",
                newName: "name");

            migrationBuilder.AlterColumn<int>(
                name: "text",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
