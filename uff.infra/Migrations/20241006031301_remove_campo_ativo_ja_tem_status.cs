using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uff.infra.Migrations
{
    /// <inheritdoc />
    public partial class remove_campo_ativo_ja_tem_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
