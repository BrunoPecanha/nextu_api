using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adiciona_coluna_para_confguraca_procurar_menor_fila : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "look_for_minor_queue",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "look_for_minor_queue",
                table: "users");
        }
    }
}
