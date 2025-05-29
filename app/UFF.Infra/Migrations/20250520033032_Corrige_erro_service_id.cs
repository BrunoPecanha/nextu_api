using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Corrige_erro_service_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "final_price",
                table: "customer_services",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "customer_services",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id",
                table: "customer_services");

            migrationBuilder.AlterColumn<decimal>(
                name: "final_price",
                table: "customer_services",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");
        }
    }
}
