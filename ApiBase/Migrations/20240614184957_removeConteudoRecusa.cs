using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBase.Migrations
{
    /// <inheritdoc />
    public partial class removeConteudoRecusa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConteudoRecusa",
                table: "Solicitacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConteudoRecusa",
                table: "Solicitacao",
                type: "nvarchar(650)",
                maxLength: 650,
                nullable: false,
                defaultValue: "");
        }
    }
}
