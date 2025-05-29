using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class reposiciona_propriedades_em_customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_queue_customers_position",
                table: "queue_customers");

            migrationBuilder.DropIndex(
                name: "ix_queue_customers_queue_id",
                table: "queue_customers");

            migrationBuilder.DropIndex(
                name: "ix_queue_customers_queue_position",
                table: "queue_customers");

            migrationBuilder.DropIndex(
                name: "ix_queue_customers_status",
                table: "queue_customers");

            migrationBuilder.DropIndex(
                name: "ix_queue_customers_time_entered",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "is_priority",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "position",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "service_end_time",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "service_start_time",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "time_called_in_queue",
                table: "queue_customers");

            migrationBuilder.DropColumn(
                name: "time_entered_queue",
                table: "queue_customers");

            migrationBuilder.RenameIndex(
                name: "ix_queue_customers_customer_id",
                table: "queue_customers",
                newName: "IX_queue_customers_customer_id");

            migrationBuilder.AddColumn<bool>(
                name: "is_priority",
                table: "customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "service_end_time",
                table: "customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "service_start_time",
                table: "customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "customers",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "time_called_in_queue",
                table: "customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "time_entered_queue",
                table: "customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_position",
                table: "customers",
                column: "position");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_queue_position",
                table: "customers",
                columns: new[] { "queue_id", "position" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_status",
                table: "customers",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_time_entered",
                table: "customers",
                column: "time_entered_queue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_queue_customers_position",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_queue_customers_queue_position",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_queue_customers_status",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_queue_customers_time_entered",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "is_priority",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "position",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "service_end_time",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "service_start_time",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "time_called_in_queue",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "time_entered_queue",
                table: "customers");

            migrationBuilder.RenameIndex(
                name: "IX_queue_customers_customer_id",
                table: "queue_customers",
                newName: "ix_queue_customers_customer_id");

            migrationBuilder.AddColumn<bool>(
                name: "is_priority",
                table: "queue_customers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "position",
                table: "queue_customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "service_end_time",
                table: "queue_customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "service_start_time",
                table: "queue_customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "queue_customers",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "time_called_in_queue",
                table: "queue_customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "time_entered_queue",
                table: "queue_customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_position",
                table: "queue_customers",
                column: "position");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_queue_id",
                table: "queue_customers",
                column: "queue_id");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_queue_position",
                table: "queue_customers",
                columns: new[] { "queue_id", "position" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_status",
                table: "queue_customers",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_queue_customers_time_entered",
                table: "queue_customers",
                column: "time_entered_queue");
        }
    }
}
