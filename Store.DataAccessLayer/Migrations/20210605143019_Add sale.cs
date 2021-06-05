using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccessLayer.Migrations
{
    public partial class Addsale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInBooks_Authors_AuthorId",
                table: "AuthorInBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInBooks_PrintingEditions_PrintingEditionId",
                table: "AuthorInBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks");

            migrationBuilder.RenameTable(
                name: "AuthorInBooks",
                newName: "AuthorInPrintingEditions");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInBooks_PrintingEditionId",
                table: "AuthorInPrintingEditions",
                newName: "IX_AuthorInPrintingEditions_PrintingEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInBooks_AuthorId",
                table: "AuthorInPrintingEditions",
                newName: "IX_AuthorInPrintingEditions_AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                table: "PrintingEditions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorInPrintingEditions",
                table: "AuthorInPrintingEditions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInPrintingEditions_Authors_AuthorId",
                table: "AuthorInPrintingEditions",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInPrintingEditions_PrintingEditions_PrintingEditionId",
                table: "AuthorInPrintingEditions",
                column: "PrintingEditionId",
                principalTable: "PrintingEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInPrintingEditions_Authors_AuthorId",
                table: "AuthorInPrintingEditions");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorInPrintingEditions_PrintingEditions_PrintingEditionId",
                table: "AuthorInPrintingEditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorInPrintingEditions",
                table: "AuthorInPrintingEditions");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "PrintingEditions");

            migrationBuilder.RenameTable(
                name: "AuthorInPrintingEditions",
                newName: "AuthorInBooks");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInPrintingEditions_PrintingEditionId",
                table: "AuthorInBooks",
                newName: "IX_AuthorInBooks_PrintingEditionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorInPrintingEditions_AuthorId",
                table: "AuthorInBooks",
                newName: "IX_AuthorInBooks_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorInBooks",
                table: "AuthorInBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInBooks_Authors_AuthorId",
                table: "AuthorInBooks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorInBooks_PrintingEditions_PrintingEditionId",
                table: "AuthorInBooks",
                column: "PrintingEditionId",
                principalTable: "PrintingEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
