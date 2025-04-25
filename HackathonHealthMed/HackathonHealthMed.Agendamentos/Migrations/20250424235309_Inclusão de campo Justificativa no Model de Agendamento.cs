using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonHealthMed.Agendamentos.Migrations
{
    /// <inheritdoc />
    public partial class InclusãodecampoJustificativanoModeldeAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Justificativa",
                table: "Agendamentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Justificativa",
                table: "Agendamentos");
        }
    }
}
