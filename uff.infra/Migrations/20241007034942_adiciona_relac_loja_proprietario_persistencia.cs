using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uff.infra.Migrations
{
    /// <inheritdoc />
    public partial class adiciona_relac_loja_proprietario_persistencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_User_UserId",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_UserId",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Store");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Store",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Store_UserId",
                table: "Store",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_User_UserId",
                table: "Store",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
