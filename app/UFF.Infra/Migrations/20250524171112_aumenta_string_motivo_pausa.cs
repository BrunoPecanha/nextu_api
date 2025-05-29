using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class aumenta_string_motivo_pausa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "pause_reason",
                table: "queues",
                type: "varchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "pause_reason",
                table: "queues",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldNullable: true);
        }
    }
}
