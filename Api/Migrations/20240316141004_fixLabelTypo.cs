using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class fixLabelTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDocumentLabel_Labels_LablesId",
                table: "AdmissionDocumentLabel");

            migrationBuilder.RenameColumn(
                name: "LablesId",
                table: "AdmissionDocumentLabel",
                newName: "LabelsId");

            migrationBuilder.RenameIndex(
                name: "IX_AdmissionDocumentLabel_LablesId",
                table: "AdmissionDocumentLabel",
                newName: "IX_AdmissionDocumentLabel_LabelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDocumentLabel_Labels_LabelsId",
                table: "AdmissionDocumentLabel",
                column: "LabelsId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDocumentLabel_Labels_LabelsId",
                table: "AdmissionDocumentLabel");

            migrationBuilder.RenameColumn(
                name: "LabelsId",
                table: "AdmissionDocumentLabel",
                newName: "LablesId");

            migrationBuilder.RenameIndex(
                name: "IX_AdmissionDocumentLabel_LabelsId",
                table: "AdmissionDocumentLabel",
                newName: "IX_AdmissionDocumentLabel_LablesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDocumentLabel_Labels_LablesId",
                table: "AdmissionDocumentLabel",
                column: "LablesId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
