using Microsoft.EntityFrameworkCore.Migrations;

namespace bankaccount.Migrations
{
    public partial class SecondMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_myuserId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_myuserId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "myuserId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Id",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_Id",
                table: "Transaction",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_Id",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "myuserId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_myuserId",
                table: "Transaction",
                column: "myuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_myuserId",
                table: "Transaction",
                column: "myuserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
