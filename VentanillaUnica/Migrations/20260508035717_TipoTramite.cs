using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VentanillaUnica.Migrations
{
    /// <inheritdoc />
    public partial class TipoTramite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Tramites_TipoTramiteId",
                table: "Solicitudes");

            migrationBuilder.RenameColumn(
                name: "TipoTramiteId",
                table: "Solicitudes",
                newName: "TramiteId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitudes_TipoTramiteId",
                table: "Solicitudes",
                newName: "IX_Solicitudes_TramiteId");

            migrationBuilder.AlterColumn<int>(
                name: "DiasEstimados",
                table: "Tramites",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tramites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "Tramites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Tramites",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Tramites",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Tramites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoTramiteId",
                table: "Tramites",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "Solicitudes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Solicitudes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Solicitudes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Solicitudes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoTramites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RequierePlaca = table.Column<bool>(type: "boolean", nullable: true),
                    CreadoPor = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificadoPor = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTramites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tramites_TipoTramiteId",
                table: "Tramites",
                column: "TipoTramiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Tramites_TramiteId",
                table: "Solicitudes",
                column: "TramiteId",
                principalTable: "Tramites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tramites_TipoTramites_TipoTramiteId",
                table: "Tramites",
                column: "TipoTramiteId",
                principalTable: "TipoTramites",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Tramites_TramiteId",
                table: "Solicitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tramites_TipoTramites_TipoTramiteId",
                table: "Tramites");

            migrationBuilder.DropTable(
                name: "TipoTramites");

            migrationBuilder.DropIndex(
                name: "IX_Tramites_TipoTramiteId",
                table: "Tramites");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Tramites");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Tramites");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Tramites");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Tramites");

            migrationBuilder.DropColumn(
                name: "TipoTramiteId",
                table: "Tramites");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Solicitudes");

            migrationBuilder.RenameColumn(
                name: "TramiteId",
                table: "Solicitudes",
                newName: "TipoTramiteId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitudes_TramiteId",
                table: "Solicitudes",
                newName: "IX_Solicitudes_TipoTramiteId");

            migrationBuilder.AlterColumn<int>(
                name: "DiasEstimados",
                table: "Tramites",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tramites",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Tramites_TipoTramiteId",
                table: "Solicitudes",
                column: "TipoTramiteId",
                principalTable: "Tramites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
