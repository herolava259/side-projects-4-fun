using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assgiment1011.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeOfStateInSQLTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Annoucements",
                type: "nvarchar(24)",
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Annoucements",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldDefaultValue: "Active");
        }
    }
}
