using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EticaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class m0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_MyFiles_ProductImagesId",
                table: "ProductProductImageFile");

            migrationBuilder.RenameColumn(
                name: "ProductImagesId",
                table: "ProductProductImageFile",
                newName: "ProductImageFileId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductImageFile_ProductImagesId",
                table: "ProductProductImageFile",
                newName: "IX_ProductProductImageFile_ProductImageFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_MyFiles_ProductImageFileId",
                table: "ProductProductImageFile",
                column: "ProductImageFileId",
                principalTable: "MyFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_MyFiles_ProductImageFileId",
                table: "ProductProductImageFile");

            migrationBuilder.RenameColumn(
                name: "ProductImageFileId",
                table: "ProductProductImageFile",
                newName: "ProductImagesId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductImageFile_ProductImageFileId",
                table: "ProductProductImageFile",
                newName: "IX_ProductProductImageFile_ProductImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_MyFiles_ProductImagesId",
                table: "ProductProductImageFile",
                column: "ProductImagesId",
                principalTable: "MyFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
