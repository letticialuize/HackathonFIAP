using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonHealthMed.GestaoHorarios.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioConsulta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoCrm = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    HorarioInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorarioFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaDisponivel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioConsulta", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioConsulta");
        }
    }
}
