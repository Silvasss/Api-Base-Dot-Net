﻿// <auto-generated />
using System;
using ApiBase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiBase.Migrations
{
    [DbContext(typeof(DataContextEF))]
    [Migration("20240607133152_serilogRequire")]
    partial class serilogRequire
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiBase.Models.AuditLogs", b =>
                {
                    b.Property<int>("AuditLog_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuditLog_Id"));

                    b.Property<string>("Auth_Usuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("AuditLog_Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("ApiBase.Models.Auth", b =>
                {
                    b.Property<int>("Auth_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Auth_id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Auth_id");

                    b.HasIndex("Usuario")
                        .IsUnique();

                    b.ToTable("Auth");
                });

            modelBuilder.Entity("ApiBase.Models.Curso", b =>
                {
                    b.Property<int>("Curso_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Curso_Id"));

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Instituicao_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.HasKey("Curso_Id");

                    b.HasIndex("Instituicao_Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("ApiBase.Models.Experiencia", b =>
                {
                    b.Property<int>("Experiencia_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Experiencia_Id"));

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Empresa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Fim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Inicio")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("PlusCode")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Setor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.Property<int>("Usuario_Id")
                        .HasColumnType("int");

                    b.Property<string>("Vinculo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Experiencia_Id");

                    b.HasIndex("Usuario_Id");

                    b.ToTable("Experiencia");
                });

            modelBuilder.Entity("ApiBase.Models.Graduacao", b =>
                {
                    b.Property<int>("Graduacao_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Graduacao_Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Curso_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Fim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Inicio")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("InstituicaoId")
                        .HasColumnType("int");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.Property<int>("Usuario_Id")
                        .HasColumnType("int");

                    b.HasKey("Graduacao_Id");

                    b.HasIndex("Usuario_Id");

                    b.ToTable("Graduacaos");
                });

            modelBuilder.Entity("ApiBase.Models.InstituicaoEF", b =>
                {
                    b.Property<int>("Instituicao_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Instituicao_Id"));

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("Auth_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PlusCode")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Tipo_Conta_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.HasKey("Instituicao_Id");

                    b.HasIndex("Auth_Id")
                        .IsUnique();

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Instituicao");
                });

            modelBuilder.Entity("ApiBase.Models.SerilogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("LogEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageTemplate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("ApiBase.Models.Solicitacao", b =>
                {
                    b.Property<int>("Solicitacao_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Solicitacao_Id"));

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Graduacao_Id")
                        .HasColumnType("int");

                    b.Property<int>("Instituicao_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.HasKey("Solicitacao_Id");

                    b.HasIndex("Graduacao_Id")
                        .IsUnique();

                    b.ToTable("Solicitacao");
                });

            modelBuilder.Entity("ApiBase.Models.TipoConta", b =>
                {
                    b.Property<int>("Tipo_Conta_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Tipo_Conta_Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.HasKey("Tipo_Conta_Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("TipoContas");
                });

            modelBuilder.Entity("ApiBase.Models.Usuario", b =>
                {
                    b.Property<int>("Usuario_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Usuario_Id"));

                    b.Property<int>("Auth_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Brasil");

                    b.Property<string>("PlusCode")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasDefaultValue("RM88+4G Plano Diretor Sul, Palmas - State of Tocantins");

                    b.Property<int>("Tipo_Conta_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("getdate()");

                    b.HasKey("Usuario_Id");

                    b.HasIndex("Auth_Id")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ApiBase.Models.Curso", b =>
                {
                    b.HasOne("ApiBase.Models.InstituicaoEF", "Instituicao")
                        .WithMany("Cursos")
                        .HasForeignKey("Instituicao_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instituicao");
                });

            modelBuilder.Entity("ApiBase.Models.Experiencia", b =>
                {
                    b.HasOne("ApiBase.Models.Usuario", "Usuario")
                        .WithMany("Experiencias")
                        .HasForeignKey("Usuario_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiBase.Models.Graduacao", b =>
                {
                    b.HasOne("ApiBase.Models.Usuario", "Usuario")
                        .WithMany("graduacoes")
                        .HasForeignKey("Usuario_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ApiBase.Models.InstituicaoEF", b =>
                {
                    b.HasOne("ApiBase.Models.Auth", "Auth")
                        .WithOne("Instituicao")
                        .HasForeignKey("ApiBase.Models.InstituicaoEF", "Auth_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");
                });

            modelBuilder.Entity("ApiBase.Models.Solicitacao", b =>
                {
                    b.HasOne("ApiBase.Models.Graduacao", "Graduacao")
                        .WithOne("Solicitacao")
                        .HasForeignKey("ApiBase.Models.Solicitacao", "Graduacao_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Graduacao");
                });

            modelBuilder.Entity("ApiBase.Models.Usuario", b =>
                {
                    b.HasOne("ApiBase.Models.Auth", "Auth")
                        .WithOne("UsuarioPerfil")
                        .HasForeignKey("ApiBase.Models.Usuario", "Auth_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");
                });

            modelBuilder.Entity("ApiBase.Models.Auth", b =>
                {
                    b.Navigation("Instituicao");

                    b.Navigation("UsuarioPerfil");
                });

            modelBuilder.Entity("ApiBase.Models.Graduacao", b =>
                {
                    b.Navigation("Solicitacao");
                });

            modelBuilder.Entity("ApiBase.Models.InstituicaoEF", b =>
                {
                    b.Navigation("Cursos");
                });

            modelBuilder.Entity("ApiBase.Models.Usuario", b =>
                {
                    b.Navigation("Experiencias");

                    b.Navigation("graduacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
