using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updateproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Products",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Orders",
                newName: "Date");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleId1",
                table: "SalesProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoLocation",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SalesProducts_SaleId1",
                table: "SalesProducts",
                column: "SaleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesProducts_Sales_SaleId1",
                table: "SalesProducts",
                column: "SaleId1",
                principalTable: "Sales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesProducts_Sales_SaleId1",
                table: "SalesProducts");

            migrationBuilder.DropIndex(
                name: "IX_SalesProducts_SaleId1",
                table: "SalesProducts");

            migrationBuilder.DropColumn(
                name: "SaleId1",
                table: "SalesProducts");

            migrationBuilder.DropColumn(
                name: "PhotoLocation",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Products",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Orders",
                newName: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesProducts_Sales_SaleId",
                table: "SalesProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
