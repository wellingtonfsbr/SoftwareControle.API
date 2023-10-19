using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareControle.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioNome",
                table: "Relatorios",
                newName: "NomeUsuario");

            migrationBuilder.RenameColumn(
                name: "FerramentaNome",
                table: "Relatorios",
                newName: "NomeFerramenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeUsuario",
                table: "Relatorios",
                newName: "UsuarioNome");

            migrationBuilder.RenameColumn(
                name: "NomeFerramenta",
                table: "Relatorios",
                newName: "FerramentaNome");
        }
    }
}
