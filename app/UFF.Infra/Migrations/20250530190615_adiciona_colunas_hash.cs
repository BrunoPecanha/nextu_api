using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adiciona_colunas_hash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "logo_hash",
                table: "stores",
                type: "varchar(44)",
                maxLength: 44,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wallpaper_hash",
                table: "stores",
                type: "varchar(44)",
                maxLength: 44,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logo_hash",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "wallpaper_hash",
                table: "stores");
        }
    }
}
