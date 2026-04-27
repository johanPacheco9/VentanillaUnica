using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentanillaUnica.Migrations
{
    /// <inheritdoc />
    public partial class FechaCreacionSolicitud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "Funcionario");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitud",
                table: "Solicitudes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Funcionario",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PrimerApellido",
                table: "Funcionario",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimerNombre",
                table: "Funcionario",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SegundoApellido",
                table: "Funcionario",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegundoNombre",
                table: "Funcionario",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Funcionario",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSolicitud",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "PrimerApellido",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "PrimerNombre",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "SegundoApellido",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "SegundoNombre",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Funcionario");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Funcionario",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "Funcionario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "Funcionario",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
