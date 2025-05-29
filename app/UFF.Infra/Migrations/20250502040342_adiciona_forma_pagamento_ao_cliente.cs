using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adiciona_forma_pagamento_ao_cliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_customers_status",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "customers");

            migrationBuilder.AddColumn<int>(
                name: "payment_id",
                table: "customers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    img_path = table.Column<string>(type: "text", nullable: true),
                    icon = table.Column<string>(type: "text", nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customers_payment_id",
                table: "customers",
                column: "payment_id");

            migrationBuilder.AddForeignKey(
                name: "fk_customers_payments",
                table: "customers",
                column: "payment_id",
                principalTable: "payment",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customers_payments",
                table: "customers");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropIndex(
                name: "ix_customers_payment_id",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "payment_id",
                table: "customers");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "customers",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_customers_status",
                table: "customers",
                column: "status");
        }
    }
}
