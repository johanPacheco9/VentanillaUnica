using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentanillaUnica.Migrations
{
    /// <inheritdoc />
    public partial class TraceabilityEverything : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "GestionesSolicitud",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "GestionesSolicitud",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "GestionesSolicitud",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "GestionesSolicitud",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "Funcionario",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Funcionario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Funcionario",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Funcionario",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "Ciudadanos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Ciudadanos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Ciudadanos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Ciudadanos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "GestionesSolicitud");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "GestionesSolicitud");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "GestionesSolicitud");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "GestionesSolicitud");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Ciudadanos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Ciudadanos");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Ciudadanos");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Ciudadanos");
        }
    }
}
