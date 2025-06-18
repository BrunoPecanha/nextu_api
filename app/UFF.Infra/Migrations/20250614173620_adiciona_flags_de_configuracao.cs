using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adiciona_flags_de_configuracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "end_service_with_q_r_code",
                table: "stores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "release_order_before_gets_queued",
                table: "stores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "share_queue",
                table: "stores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "start_service_with_q_r_code",
                table: "stores",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_service_with_q_r_code",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "release_order_before_gets_queued",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "share_queue",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "start_service_with_q_r_code",
                table: "stores");
        }
    }
}
