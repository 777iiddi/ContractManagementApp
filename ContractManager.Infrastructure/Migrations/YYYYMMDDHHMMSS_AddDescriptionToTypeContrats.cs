using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManager.Infrastructure.Migrations
{
    public partial class AddDescriptionToTypeContrats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ajouter la colonne Description à la table TypeContrats
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TypeContrats",
                type: "longtext",
                nullable: false,
                defaultValue: "");

            // Ajouter les autres colonnes manquantes si elles n'existent pas
            migrationBuilder.AddColumn<int>(
                name: "DureeDefautMois",
                table: "TypeContrats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodeEssaiDefautJours",
                table: "TypeContrats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreavisDefautJours",
                table: "TypeContrats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstActif",
                table: "TypeContrats",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeleDocumentId",
                table: "TypeContrats",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TypeContrats");

            migrationBuilder.DropColumn(
                name: "DureeDefautMois",
                table: "TypeContrats");

            migrationBuilder.DropColumn(
                name: "PeriodeEssaiDefautJours",
                table: "TypeContrats");

            migrationBuilder.DropColumn(
                name: "PreavisDefautJours",
                table: "TypeContrats");

            migrationBuilder.DropColumn(
                name: "EstActif",
                table: "TypeContrats");

            migrationBuilder.DropColumn(
                name: "ModeleDocumentId",
                table: "TypeContrats");
        }
    }
}
