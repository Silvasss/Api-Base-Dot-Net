using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBase.Migrations
{
    /// <inheritdoc />
    public partial class AuditLogsTamanhoCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlusCode",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "RM88+4G Plano Diretor Sul, Palmas - State of Tocantins",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldDefaultValueSql: "'RM88+4G Plano Diretor Sul, Palmas - State of Tocantins'");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Brasil",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "'Brasil'");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlusCode",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValueSql: "'RM88+4G Plano Diretor Sul, Palmas - State of Tocantins'",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldDefaultValue: "RM88+4G Plano Diretor Sul, Palmas - State of Tocantins");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Usuarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "'Brasil'",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Brasil");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "AuditLogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
