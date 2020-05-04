using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccessLayer.Migrations
{
    public partial class AvatarTypeIsaString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);
        }
    }
}
