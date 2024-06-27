using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBase.Migrations
{
    /// <inheritdoc />
    public partial class removeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Curso_Nome",
                table: "Curso");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Nome",
                table: "Curso",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Curso_Nome",
                table: "Curso");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Nome",
                table: "Curso",
                column: "Nome",
                unique: true);
        }
    }
}
