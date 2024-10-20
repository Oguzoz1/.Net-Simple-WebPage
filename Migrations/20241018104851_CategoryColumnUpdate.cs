using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApp.Migrations
{
    /// <inheritdoc />
    public partial class CategoryColumnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ürünler_CategoryId",
                table: "Ürünler",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ürünler_Kategoriler_CategoryId",
                table: "Ürünler",
                column: "CategoryId",
                principalTable: "Kategoriler",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ürünler_Kategoriler_CategoryId",
                table: "Ürünler");

            migrationBuilder.DropIndex(
                name: "IX_Ürünler_CategoryId",
                table: "Ürünler");
        }
    }
}
