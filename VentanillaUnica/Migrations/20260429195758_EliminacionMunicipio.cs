using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VentanillaUnica.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionMunicipio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ciudadanos_Municipios_MunicipioId",
                table: "Ciudadanos");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropIndex(
                name: "IX_Ciudadanos_MunicipioId",
                table: "Ciudadanos");

            migrationBuilder.DropColumn(
                name: "MunicipioId",
                table: "Ciudadanos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MunicipioId",
                table: "Ciudadanos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Departmento = table.Column<string>(type: "text", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    IdNumber = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    MunicipioId = table.Column<int>(type: "integer", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudadanos_MunicipioId",
                table: "Ciudadanos",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MunicipioId",
                table: "User",
                column: "MunicipioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudadanos_Municipios_MunicipioId",
                table: "Ciudadanos",
                column: "MunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
