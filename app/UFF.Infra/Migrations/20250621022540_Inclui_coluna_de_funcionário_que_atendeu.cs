using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inclui_coluna_de_funcionário_que_atendeu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "employee_attendant_id",
                table: "customers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_employee_attendant_id",
                table: "customers",
                column: "employee_attendant_id");

            migrationBuilder.AddForeignKey(
                name: "fk_customers_attendat",
                table: "customers",
                column: "employee_attendant_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customers_attendat",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_employee_attendant_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "employee_attendant_id",
                table: "customers");
        }
    }
}
