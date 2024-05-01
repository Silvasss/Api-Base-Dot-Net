using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBase.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLog_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Auth_Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLog_Id);
                });

            migrationBuilder.CreateTable(
                name: "Auth",
                columns: table => new
                {
                    Auth_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth", x => x.Auth_id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContas",
                columns: table => new
                {
                    Tipo_Conta_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContas", x => x.Tipo_Conta_Id);
                });

            migrationBuilder.CreateTable(
                name: "Instituicao",
                columns: table => new
                {
                    Instituicao_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlusCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()"),
                    Auth_Id = table.Column<int>(type: "int", nullable: false),
                    Tipo_Conta_Id = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicao", x => x.Instituicao_Id);
                    table.ForeignKey(
                        name: "FK_Instituicao_Auth_Auth_Id",
                        column: x => x.Auth_Id,
                        principalTable: "Auth",
                        principalColumn: "Auth_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "'Brasil'"),
                    PlusCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, defaultValueSql: "'RM88+4G Plano Diretor Sul, Palmas - State of Tocantins'"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()"),
                    Auth_Id = table.Column<int>(type: "int", nullable: false),
                    Tipo_Conta_Id = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usuario_Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Auth_Auth_Id",
                        column: x => x.Auth_Id,
                        principalTable: "Auth",
                        principalColumn: "Auth_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Curso_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()"),
                    Instituicao_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Curso_Id);
                    table.ForeignKey(
                        name: "FK_Curso_Instituicao_Instituicao_Id",
                        column: x => x.Instituicao_Id,
                        principalTable: "Instituicao",
                        principalColumn: "Instituicao_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiencia",
                columns: table => new
                {
                    Experiencia_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Setor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Empresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlusCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Vinculo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()"),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencia", x => x.Experiencia_Id);
                    table.ForeignKey(
                        name: "FK_Experiencia_Usuarios_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Graduacaos",
                columns: table => new
                {
                    Graduacao_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Situacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Curso_Id = table.Column<int>(type: "int", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()"),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false),
                    InstituicaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graduacaos", x => x.Graduacao_Id);
                    table.ForeignKey(
                        name: "FK_Graduacaos_Usuarios_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Solicitacao_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Instituicao_Id = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, computedColumnSql: "getdate()"),
                    Graduacao_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Solicitacao_Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Graduacaos_Graduacao_Id",
                        column: x => x.Graduacao_Id,
                        principalTable: "Graduacaos",
                        principalColumn: "Graduacao_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Usuario",
                table: "Auth",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Instituicao_Id",
                table: "Curso",
                column: "Instituicao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_Nome",
                table: "Curso",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiencia_Usuario_Id",
                table: "Experiencia",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Graduacaos_Usuario_Id",
                table: "Graduacaos",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_Auth_Id",
                table: "Instituicao",
                column: "Auth_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_Nome",
                table: "Instituicao",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_Graduacao_Id",
                table: "Solicitacao",
                column: "Graduacao_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoContas_Nome",
                table: "TipoContas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Auth_Id",
                table: "Usuarios",
                column: "Auth_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Experiencia");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "TipoContas");

            migrationBuilder.DropTable(
                name: "Instituicao");

            migrationBuilder.DropTable(
                name: "Graduacaos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Auth");
        }
    }
}
