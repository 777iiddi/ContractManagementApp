using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManager.Infrastructure.Migrations
{
    public partial class AddPaysIdToSocietes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaysId",
                table: "Societes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Societes_PaysId",
                table: "Societes",
                column: "PaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Societes_Pays_PaysId",
                table: "Societes",
                column: "PaysId",
                principalTable: "Pays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Societes_Pays_PaysId",
                table: "Societes");

            migrationBuilder.DropIndex(
                name: "IX_Societes_PaysId",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "PaysId",
                table: "Societes");
        }
    }
}
