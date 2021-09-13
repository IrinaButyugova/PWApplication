using Microsoft.EntityFrameworkCore.Migrations;

namespace PWApplication.DAL.Data.Migrations
{
    public partial class AddCorrespondent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrespondentId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CorrespondentId",
                table: "Transactions",
                column: "CorrespondentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_CorrespondentId",
                table: "Transactions",
                column: "CorrespondentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_CorrespondentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CorrespondentId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CorrespondentId",
                table: "Transactions");
        }
    }
}
