using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentitySonProje.Migrations
{
    public partial class decial_to_double : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "OrderProducts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Extras",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "120682e8-4f56-416c-bc87-c5f5d28cc659");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b03aa1bd-aeee-449e-82e4-d1a17372c54b");

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 1,
                column: "Price",
                value: 10.0);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 2,
                column: "Price",
                value: 10.0);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 3,
                column: "Price",
                value: 10.0);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 4,
                column: "Price",
                value: 10.0);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 5,
                column: "Price",
                value: 10.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "Price",
                value: 100.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "Price",
                value: 120.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "Price",
                value: 140.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "Price",
                value: 160.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "Price",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "Price",
                value: 70.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderProducts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Extras",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "32f136c4-ca53-45d1-bf03-c9858da5e0d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6e88ceb4-773e-4d02-a2b3-6d786d2bc7ca");

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 1,
                column: "Price",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 2,
                column: "Price",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 3,
                column: "Price",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 4,
                column: "Price",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 5,
                column: "Price",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "Price",
                value: 100m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "Price",
                value: 120m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "Price",
                value: 140m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "Price",
                value: 160m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "Price",
                value: 20m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "Price",
                value: 70m);
        }
    }
}
