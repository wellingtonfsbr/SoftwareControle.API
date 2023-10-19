using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareControle.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class HorasTrabalhadasInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HorasTrabalhadas",
                table: "Ordens",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "HorasTrabalhadas",
                table: "Ordens",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
