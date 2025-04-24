using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackathonHealthMed.GestaoHorarios.Migrations
{
    /// <inheritdoc />
    public partial class InclusãodemodelValordeConsultaporMédico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ValorConsultaMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoCrm = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValorConsultaMedico", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValorConsultaMedico");
        }
    }
}
