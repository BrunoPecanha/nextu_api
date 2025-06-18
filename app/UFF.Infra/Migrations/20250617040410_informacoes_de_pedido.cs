using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class informacoes_de_pedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "priority",
                table: "customers",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "processed_at",
                table: "customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "processed_by_id",
                table: "customers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rejection_reason",
                table: "customers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_processed_by_id",
                table: "customers",
                column: "processed_by_id");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_users_processed_by_id",
                table: "customers",
                column: "processed_by_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_users_processed_by_id",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_processed_by_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "processed_at",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "processed_by_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "rejection_reason",
                table: "customers");
        }
    }
}
