using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VentanillaUnica.Migrations
{
    /// <inheritdoc />
    public partial class GestionSolicitud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GestionesSolicitud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolicitudId = table.Column<int>(type: "integer", nullable: false),
                    EstadoAnterior = table.Column<int>(type: "integer", nullable: false),
                    EstadoNuevo = table.Column<int>(type: "integer", nullable: false),
                    FuncionarioAnteriorId = table.Column<int>(type: "integer", nullable: true),
                    FuncionarioNuevoId = table.Column<int>(type: "integer", nullable: true),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    FechaGestion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RealizadoPor = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GestionesSolicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GestionesSolicitud_Funcionario_FuncionarioNuevoId",
                        column: x => x.FuncionarioNuevoId,
                        principalTable: "Funcionario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GestionesSolicitud_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GestionesSolicitud_FuncionarioNuevoId",
                table: "GestionesSolicitud",
                column: "FuncionarioNuevoId");

            migrationBuilder.CreateIndex(
                name: "IX_GestionesSolicitud_SolicitudId",
                table: "GestionesSolicitud",
                column: "SolicitudId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GestionesSolicitud");
        }
    }
}
