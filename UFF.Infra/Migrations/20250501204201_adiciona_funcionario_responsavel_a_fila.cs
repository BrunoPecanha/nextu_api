using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adiciona_funcionario_responsavel_a_fila : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "employee_id",
                table: "queues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_queues_employee_id",
                table: "queues",
                column: "employee_id");

            migrationBuilder.AddForeignKey(
                name: "fk_queues_users",
                table: "queues",
                column: "employee_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_queues_users",
                table: "queues");

            migrationBuilder.DropIndex(
                name: "IX_queues_employee_id",
                table: "queues");

            migrationBuilder.DropColumn(
                name: "employee_id",
                table: "queues");
        }
    }
}
