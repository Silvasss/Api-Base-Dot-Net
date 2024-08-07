﻿using ApiBase.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBase.Data
{
    public class DataContextEF(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Auth> Auth { get; set; }
        public DbSet<SerilogEntry> Logs { get; set; }
        public DbSet<AuditLogs> AuditLogs { get; set; }
        public DbSet<TipoConta> TipoContas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Experiencia> Experiencia { get; set; }
        public DbSet<Graduacao> Graduacaos { get; set; }
        public DbSet<Solicitacao> Solicitacao { get; set; }
        public DbSet<InstituicaoEF> Instituicao { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<RespostaSolicitacao> RespostasSolicitacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Log gerados pelo Serilog
            modelBuilder.Entity<SerilogEntry>().HasKey(s => s.Id);
            modelBuilder.Entity<SerilogEntry>().Property(s => s.Exception).IsRequired(false);
            modelBuilder.Entity<SerilogEntry>().Property(s => s.Properties).IsRequired(false);
            modelBuilder.Entity<SerilogEntry>().Property(s => s.LogEvent).IsRequired(false);
            modelBuilder.Entity<SerilogEntry>().Property(s => s.TimeStamp).HasColumnType("datetime");
            modelBuilder.Entity<SerilogEntry>().Property(s => s.Level).HasMaxLength(16);

            // Log de alteração
            modelBuilder.Entity<AuditLogs>().HasKey(a => a.AuditLog_Id);
            modelBuilder.Entity<AuditLogs>().Property(a => a.Tipo).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<AuditLogs>().Property(a => a.Descricao).IsRequired();
            modelBuilder.Entity<AuditLogs>().Property(a => a.CreatedDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<AuditLogs>().Property(a => a.Auth_Usuario).HasMaxLength(50).IsRequired();

            // Tipo da conta
            modelBuilder.Entity<TipoConta>().HasKey(t => t.Tipo_Conta_Id);
            modelBuilder.Entity<TipoConta>().Property(t => t.Nome).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<TipoConta>().HasIndex(t => t.Nome).IsUnique(true);
            modelBuilder.Entity<TipoConta>().Property(t => t.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TipoConta>().Property(t => t.UpdatedAt).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();

            // Autenticação
            modelBuilder.Entity<Auth>().HasKey(a => a.Auth_id);
            modelBuilder.Entity<Auth>().Property(a => a.Usuario).HasMaxLength(32).IsRequired();
            modelBuilder.Entity<Auth>().HasIndex(a => a.Usuario).IsUnique(true);
            modelBuilder.Entity<Auth>().Property(a => a.PasswordHash).IsRequired();
            modelBuilder.Entity<Auth>().Property(a => a.PasswordSalt).IsRequired();
            modelBuilder.Entity<Auth>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Auth>().Property(a => a.UpdatedAt).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();

            // Instituição
            modelBuilder.Entity<InstituicaoEF>().HasKey(b => b.Instituicao_Id);
            modelBuilder.Entity<InstituicaoEF>().Property(a => a.Nome).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<InstituicaoEF>().HasIndex(a => a.Nome).IsUnique(true);
            modelBuilder.Entity<InstituicaoEF>().Property(a => a.PlusCode).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<InstituicaoEF>().Property(a => a.Ativo).HasDefaultValue(true);
            modelBuilder.Entity<InstituicaoEF>().Property(a => a.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<InstituicaoEF>().Property(a => a.UpdatedAt).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<InstituicaoEF>().Property(a => a.Tipo_Conta_Id).HasDefaultValue(2);
            modelBuilder.Entity<InstituicaoEF>().HasOne<Auth>(a => a.Auth).WithOne(e => e.Instituicao).HasForeignKey<InstituicaoEF>(i => i.Auth_Id).IsRequired();

            // Curso da instituição
            modelBuilder.Entity<Curso>().HasKey(c => c.Curso_Id);
            modelBuilder.Entity<Curso>().Property(c => c.Nome).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Curso>().HasIndex(c => c.Nome);
            modelBuilder.Entity<Curso>().Property(c => c.Ativo).HasDefaultValue(true);
            modelBuilder.Entity<Curso>().Property(c => c.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Curso>().Property(c => c.UpdatedAt).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Curso>().HasOne<InstituicaoEF>(i => i.Instituicao).WithMany(c => c.Cursos).HasForeignKey(i => i.Instituicao_Id).IsRequired();

            // Usuario
            modelBuilder.Entity<Usuario>().HasKey(u => u.Usuario_Id);
            modelBuilder.Entity<Usuario>().Property(u => u.Nome).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Pais).HasDefaultValue("Brasil").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.PlusCode).HasDefaultValue("RM88+4G Plano Diretor Sul, Palmas - State of Tocantins").HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.SobreMin).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.CargoPrincipal).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Email).HasMaxLength(150);
            modelBuilder.Entity<Usuario>().Property(u => u.PortfolioURL).HasMaxLength(150);
            modelBuilder.Entity<Usuario>().Property(u => u.Experiencia).HasDefaultValue("Start Up").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Usuario>().Property(u => u.UpdatedAt).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Usuario>().Property(u => u.Tipo_Conta_Id).HasDefaultValue(2);
            modelBuilder.Entity<Usuario>().Property(u => u.ConfiguracoesConta);
            modelBuilder.Entity<Usuario>().HasOne<Auth>(u => u.Auth).WithOne(a => a.UsuarioPerfil).HasForeignKey<Usuario>(u => u.Auth_Id).IsRequired();

            // Experiência
            modelBuilder.Entity<Experiencia>().HasKey(e => e.Experiencia_Id);
            modelBuilder.Entity<Experiencia>().Property(e => e.Setor).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Experiencia>().Property(e => e.Empresa).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Experiencia>().Property(e => e.PlusCode).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Experiencia>().Property(e => e.Vinculo).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Experiencia>().Property(e => e.Funcao).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Experiencia>().Property(e => e.Responsabilidade).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Experiencia>().Property(e => e.Ativo).HasColumnType("BIT").HasDefaultValue(true);
            modelBuilder.Entity<Experiencia>().Property(e => e.Inicio).IsRequired();
            modelBuilder.Entity<Experiencia>().Property(e => e.Fim);
            modelBuilder.Entity<Experiencia>().Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Experiencia>().Property(e => e.UpdatedAt).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Experiencia>().HasOne<Usuario>(e => e.Usuario).WithMany(u => u.Experiencias).HasForeignKey(e => e.Usuario_Id).IsRequired();

            // Graduação
            modelBuilder.Entity<Graduacao>().HasKey(g => g.Graduacao_Id);
            modelBuilder.Entity<Graduacao>().Property(g => g.Situacao);
            modelBuilder.Entity<Graduacao>().Property(g => g.Curso_Id).IsRequired();
            modelBuilder.Entity<Graduacao>().Property(g => g.CursoNome).IsRequired();
            modelBuilder.Entity<Graduacao>().Property(g => g.Tipo).IsRequired();
            modelBuilder.Entity<Graduacao>().Property(g => g.Status);
            modelBuilder.Entity<Graduacao>().Property(g => g.InstituicaoId).IsRequired();
            modelBuilder.Entity<Graduacao>().Property(g => g.InstituicaoNome).IsRequired();
            modelBuilder.Entity<Graduacao>().Property(g => g.Inicio).IsRequired();
            modelBuilder.Entity<Graduacao>().Property(g => g.Fim);
            modelBuilder.Entity<Graduacao>().Property(g => g.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Graduacao>().Property(g => g.UpdatedAt).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Graduacao>().HasOne<Usuario>(g => g.Usuario).WithMany(u => u.graduacoes).HasForeignKey(e => e.Usuario_Id).IsRequired();

            // Solicitação
            modelBuilder.Entity<Solicitacao>().HasKey(s => s.Solicitacao_Id);
            modelBuilder.Entity<Solicitacao>().Property(s => s.Status);
            modelBuilder.Entity<Solicitacao>().Property(s => s.Instituicao_Id).IsRequired();
            modelBuilder.Entity<Solicitacao>().Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Solicitacao>().Property(s => s.UpdatedAt).HasDefaultValueSql("getdate()").IsRequired();
            modelBuilder.Entity<Solicitacao>().HasOne<Graduacao>(s => s.Graduacao).WithOne(g => g.Solicitacao).HasForeignKey<Solicitacao>(s => s.Graduacao_Id).IsRequired();

            // Resposta da solicitação
            modelBuilder.Entity<RespostaSolicitacao>().HasKey(s => s.Resposta_Id);
            modelBuilder.Entity<RespostaSolicitacao>().Property(s => s.ConteudoReposta).HasMaxLength(650);
            modelBuilder.Entity<RespostaSolicitacao>().Property(s => s.Origem);
            modelBuilder.Entity<RespostaSolicitacao>().Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<RespostaSolicitacao>().HasOne<Solicitacao>(s => s.Solicitacao).WithMany(g => g.Respostas).HasForeignKey(s => s.Solicitacao_Id).IsRequired();
        }
    }
}
