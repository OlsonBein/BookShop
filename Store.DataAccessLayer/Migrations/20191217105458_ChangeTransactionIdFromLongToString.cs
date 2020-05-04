using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccessLayer.Migrations
{
    public partial class ChangeTransactionIdFromLongToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TransactionId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
