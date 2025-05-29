using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Ajusta_relacao_fila_cliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_queue_customers_users",
                table: "queue_customers");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "queue_customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_queue_customers_user_id",
                table: "queue_customers",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_queue_customers_users",
                table: "queue_customers",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_queue_customers_users",
                table: "queue_customers");

            migrationBuilder.DropIndex(
                name: "IX_queue_customers_user_id",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "id",
                table: "queue_customers");

            migrationBuilder.AddForeignKey(
                name: "fk_queue_customers_users",
                table: "queue_customers",
                column: "customer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
