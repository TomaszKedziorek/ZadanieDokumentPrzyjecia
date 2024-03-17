using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class fixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDocuments_Suppliers_SupplierId",
                table: "AdmissionDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDocuments_Warehouses_TargetWarehouseId",
                table: "AdmissionDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers");

            migrationBuilder.AlterColumn<int>(
                name: "TargetWarehouseId",
                table: "AdmissionDocuments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "AdmissionDocuments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SupplierId",
                table: "Addresses",
                column: "SupplierId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Suppliers_SupplierId",
                table: "Addresses",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDocuments_Suppliers_SupplierId",
                table: "AdmissionDocuments",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDocuments_Warehouses_TargetWarehouseId",
                table: "AdmissionDocuments",
                column: "TargetWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Suppliers_SupplierId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDocuments_Suppliers_SupplierId",
                table: "AdmissionDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDocuments_Warehouses_TargetWarehouseId",
                table: "AdmissionDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_SupplierId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "TargetWarehouseId",
                table: "AdmissionDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "AdmissionDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDocuments_Suppliers_SupplierId",
                table: "AdmissionDocuments",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDocuments_Warehouses_TargetWarehouseId",
                table: "AdmissionDocuments",
                column: "TargetWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                table: "Suppliers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
