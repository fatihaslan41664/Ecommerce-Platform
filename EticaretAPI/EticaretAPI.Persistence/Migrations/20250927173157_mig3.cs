using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EticaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyFiles_Products_ProductId",
                table: "MyFiles");

            migrationBuilder.DropIndex(
                name: "IX_MyFiles_ProductId",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "FileProp",
                table: "MyFiles");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "MyFiles");

            migrationBuilder.CreateTable(
                name: "ProductProductImageFile",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductImagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductImageFile", x => new { x.ProductId, x.ProductImagesId });
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_MyFiles_ProductImagesId",
                        column: x => x.ProductImagesId,
                        principalTable: "MyFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductImageFile_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductImageFile_ProductImagesId",
                table: "ProductProductImageFile",
                column: "ProductImagesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductImageFile");

            migrationBuilder.AddColumn<string>(
                name: "FileProp",
                table: "MyFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "MyFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyFiles_ProductId",
                table: "MyFiles",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyFiles_Products_ProductId",
                table: "MyFiles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
