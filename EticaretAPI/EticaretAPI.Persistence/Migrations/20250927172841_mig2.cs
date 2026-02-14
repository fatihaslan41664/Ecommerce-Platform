using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EticaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
