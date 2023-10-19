using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareControle.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    FerramentaNome = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UsuarioNome = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ferramentas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferramentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ferramentas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    NivelUrgencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Situacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeFerramenta = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescricaoResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataPrazoMaximo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    DataIniciado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFinalizado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataAtualizacao1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HorasTrabalhadas = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    FerramentaId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordens_Ferramentas_FerramentaId",
                        column: x => x.FerramentaId,
                        principalTable: "Ferramentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ferramentas_UsuarioId",
                table: "Ferramentas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordens_FerramentaId",
                table: "Ordens",
                column: "FerramentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordens_UsuarioId",
                table: "Ordens",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordens");

            migrationBuilder.DropTable(
                name: "Relatorios");

            migrationBuilder.DropTable(
                name: "Ferramentas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
