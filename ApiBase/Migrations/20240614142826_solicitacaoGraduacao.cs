using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBase.Migrations
{
    /// <inheritdoc />
    public partial class solicitacaoGraduacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Solicitacao");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Solicitacao");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Solicitacao",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComputedColumnSql: "getdate()");

            migrationBuilder.AddColumn<string>(
                name: "ConteudoRecusa",
                table: "Solicitacao",
                type: "nvarchar(650)",
                maxLength: 650,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Solicitacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Situacao",
                table: "Graduacaos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Graduacaos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RespostasSolicitacao",
                columns: table => new
                {
                    Resposta_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConteudoReposta = table.Column<string>(type: "nvarchar(650)", maxLength: 650, nullable: false),
                    Origem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Solicitacao_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasSolicitacao", x => x.Resposta_Id);
                    table.ForeignKey(
                        name: "FK_RespostasSolicitacao_Solicitacao_Solicitacao_Id",
                        column: x => x.Solicitacao_Id,
                        principalTable: "Solicitacao",
                        principalColumn: "Solicitacao_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespostasSolicitacao_Solicitacao_Id",
                table: "RespostasSolicitacao",
                column: "Solicitacao_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespostasSolicitacao");

            migrationBuilder.DropColumn(
                name: "ConteudoRecusa",
                table: "Solicitacao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Solicitacao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Graduacaos");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Solicitacao",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Solicitacao",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Situacao",
                table: "Graduacaos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Solicitacao",
                type: "datetime2",
                nullable: true,
                computedColumnSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
