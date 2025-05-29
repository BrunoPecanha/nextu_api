using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Remove_tabela_Queue_customer_nao_ha_necessidade_da_para_Deixar_tudo_em_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "queue_customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "queue_customers",
                columns: table => new
                {
                    queue_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    last_update = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    registering_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_queue_customers", x => new { x.queue_id, x.customer_id });
                    table.ForeignKey(
                        name: "fk_queue_customers_customers",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_queue_customers_queues",
                        column: x => x.queue_id,
                        principalTable: "queues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_queue_customers_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_queue_customers_customer_id",
                table: "queue_customers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_queue_customers_user_id",
                table: "queue_customers",
                column: "user_id");
        }
    }
}
