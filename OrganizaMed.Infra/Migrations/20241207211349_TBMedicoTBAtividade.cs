using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizaMed.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TBMedicoTBAtividade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBAtividade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    HorarioInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorarioTermino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Crm = table.Column<string>(type: "char(8)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBAtividade_TBMedico",
                columns: table => new
                {
                    AtividadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividade_TBMedico", x => new { x.AtividadeId, x.MedicoId });
                    table.ForeignKey(
                        name: "FK_Atividade_Medico",
                        column: x => x.MedicoId,
                        principalTable: "TBMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medico_Atividade",
                        column: x => x.AtividadeId,
                        principalTable: "TBAtividade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAtividade_TBMedico_MedicoId",
                table: "TBAtividade_TBMedico",
                column: "MedicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAtividade_TBMedico");

            migrationBuilder.DropTable(
                name: "TBMedico");

            migrationBuilder.DropTable(
                name: "TBAtividade");
        }
    }
}
