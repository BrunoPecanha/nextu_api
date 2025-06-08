using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adiciona_tabela_de_avaliação_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_store_rating__user_user_id",
                table: "store_rating");

            migrationBuilder.DropForeignKey(
                name: "f_k_store_rating_store_store_id",
                table: "store_rating");

            migrationBuilder.RenameIndex(
                name: "IX_store_rating_user_id",
                table: "store_rating",
                newName: "idx_store_rating_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_store_rating_store_id",
                table: "store_rating",
                newName: "idx_store_rating_store_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "score",
                table: "store_rating",
                type: "numeric(3,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "comment",
                table: "store_rating",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_store_rating_professional_id",
                table: "store_rating",
                column: "professional_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_store_rating__user_professional_id",
                table: "store_rating",
                column: "professional_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_store_rating__user_user_id",
                table: "store_rating",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_store_rating_store_store_id",
                table: "store_rating",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_store_rating__user_professional_id",
                table: "store_rating");

            migrationBuilder.DropForeignKey(
                name: "f_k_store_rating__user_user_id",
                table: "store_rating");

            migrationBuilder.DropForeignKey(
                name: "f_k_store_rating_store_store_id",
                table: "store_rating");

            migrationBuilder.DropIndex(
                name: "IX_store_rating_professional_id",
                table: "store_rating");

            migrationBuilder.RenameIndex(
                name: "idx_store_rating_user_id",
                table: "store_rating",
                newName: "IX_store_rating_user_id");

            migrationBuilder.RenameIndex(
                name: "idx_store_rating_store_id",
                table: "store_rating",
                newName: "IX_store_rating_store_id");

            migrationBuilder.AlterColumn<int>(
                name: "score",
                table: "store_rating",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)");

            migrationBuilder.AlterColumn<string>(
                name: "comment",
                table: "store_rating",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "f_k_store_rating__user_user_id",
                table: "store_rating",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_store_rating_store_store_id",
                table: "store_rating",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
