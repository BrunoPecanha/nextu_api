using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Adiciona_telefone_e_redes_sociais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "facebook",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "site",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "youtube",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "activated",
                table: "services",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "facebook",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "instagram",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "site",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "youtube",
                table: "stores");

            migrationBuilder.AlterColumn<bool>(
                name: "activated",
                table: "services",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);
        }
    }
}
