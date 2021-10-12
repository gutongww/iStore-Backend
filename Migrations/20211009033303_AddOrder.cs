using Microsoft.EntityFrameworkCore.Migrations;

namespace MSA2021.Migrations
{
    public partial class AddOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserName",
                table: "Orders",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserName",
                table: "Orders",
                column: "UserName",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserName",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserName",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
