using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonHealthMed.Agendamentos.Migrations
{
    /// <inheritdoc />
    public partial class TrocadepropriedadeMedicoIdporMedicoCrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Agendamentos");

            migrationBuilder.AddColumn<string>(
                name: "MedicoCrm",
                table: "Agendamentos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicoCrm",
                table: "Agendamentos");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicoId",
                table: "Agendamentos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
