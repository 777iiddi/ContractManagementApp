using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerToEmploye : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Employes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "AuditLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Employes_ManagerId",
                table: "Employes",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employes_Utilisateurs_ManagerId",
                table: "Employes",
                column: "ManagerId",
                principalTable: "Utilisateurs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employes_Utilisateurs_ManagerId",
                table: "Employes");

            migrationBuilder.DropIndex(
                name: "IX_Employes_ManagerId",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "AuditLogs");
        }
    }
}
