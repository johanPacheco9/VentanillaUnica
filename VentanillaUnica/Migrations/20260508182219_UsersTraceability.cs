using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentanillaUnica.Migrations
{
    /// <inheritdoc />
    public partial class UsersTraceability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Users");
        }
    }
}
