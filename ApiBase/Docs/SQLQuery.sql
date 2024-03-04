CREATE DATABASE DotNetDatabase
GO

USE DotNetDatabase
GO

CREATE SCHEMA DotNetDatabase
GO

CREATE TABLE DotNetDatabase.Auth(
    AuthId INT IDENTITY,
    Email NVARCHAR(50) PRIMARY KEY,
    PasswordHash VARBINARY(MAX),
	PasswordSalt VARBINARY(MAX),
    AuthCreated DATETIME,
    AuthUpdated DATETIME
)
GO

CREATE TABLE DotNetDatabase.Usuarios(
    UserId INT IDENTITY PRIMARY KEY,
    Email NVARCHAR(50) NOT NULL,
    Nome NVARCHAR(50),
    Pais NVARCHAR(50),
    Estado NVARCHAR(50),
    Cidade NVARCHAR(50),
    UserCreated DATETIME,
    UserUpdated DATETIME
)
GO

CREATE TABLE DotNetDatabase.Experiencias(
    ExperienciaId INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    Setor NVARCHAR(50) NOT NULL, 
    NomeEmpresa NVARCHAR(50) NOT NULL,
    Situacao NVARCHAR(50) NOT NULL,
    TipoEmprego NVARCHAR(50) NOT NULL,
    Pais NVARCHAR(50) NOT NULL,
    Estado NVARCHAR(50),
    Cidade NVARCHAR(50),
    ExperienciaCreated DATETIME,
    ExperienciaUpdated DATETIME
)
GO

CREATE TABLE DotNetDatabase.Graduacao(
    GraduacaoId INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    Instituicao NVARCHAR(50) NOT NULL,
    Curso NVARCHAR(50) NOT NULL,
    Situacao NVARCHAR(50) NOT NULL, 
    GraduacaoCreated DATETIME,
    GraduacaoUpdated DATETIME
)
GO


-- Para autenticação local
CREATE OR ALTER PROCEDURE DotNetDatabase.spCheckEmail_Get
    @Email NVARCHAR(50)
AS
BEGIN
    SELECT [Auth].[Email]
    FROM DotNetDatabase.Auth AS Auth 
        WHERE Auth.Email = @Email
END
GO

-- Registro de uma nova conta
CREATE OR ALTER PROCEDURE DotNetDatabase.spRegistration_Upsert
    @Email NVARCHAR(50),
    @PasswordHash VARBINARY(MAX),
    @PasswordSalt VARBINARY(MAX)
AS 
BEGIN
    IF NOT EXISTS (SELECT * FROM DotNetDatabase.Auth WHERE Email = @Email)
        BEGIN
            INSERT INTO DotNetDatabase.Auth(
                [Email],
                [PasswordHash],
                [PasswordSalt],
                [AuthCreated],
                [AuthUpdated]
            ) VALUES (
                @Email,
                @PasswordHash,
                @PasswordSalt,
                GETDATE(),
                GETDATE() 
            )
        END
    ELSE
        BEGIN
            UPDATE DotNetDatabase.Auth 
                SET PasswordHash = @PasswordHash,
                    PasswordSalt = @PasswordSalt
                WHERE Email = @Email
        END
END
GO

-- Para autenticação local
CREATE OR ALTER PROCEDURE DotNetDatabase.spLoginConfirmation_Get
    @Email NVARCHAR(50)
AS
BEGIN
    SELECT [Auth].[PasswordHash],
        [Auth].[PasswordSalt] 
    FROM DotNetDatabase.Auth AS Auth 
        WHERE Auth.Email = @Email
END
GO

-- criação ou atualização de um usuário
CREATE OR ALTER PROCEDURE DotNetDatabase.spUser_Upsert
    @UserId INT = NULL,
    @Nome NVARCHAR(50),
    @Email NVARCHAR(50),
	@Pais NVARCHAR(150),
    @Estado NVARCHAR(50),
    @Cidade NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM DotNetDatabase.Usuarios WHERE UserId = @UserId)
        BEGIN
        IF NOT EXISTS (SELECT * FROM DotNetDatabase.Usuarios WHERE Email = @Email)
            BEGIN
                INSERT INTO DotNetDatabase.Usuarios
                    (
                        [Email],
                        [Nome],
                        [Pais],
                        [UserCreated],    
                        [UserUpdated] 
                    )
                VALUES
                    (
                        @Email,
                        CRYPT_GEN_RANDOM(15),
                        'Brasil',
                        GETDATE(),
                        GETDATE()                    
                    )
            END 
        END 
    ELSE 
        BEGIN
            UPDATE DotNetDatabase.Usuarios 
                SET Nome = @Nome,
                    Pais = @Pais,
                    Estado = @Estado,
                    Cidade = @Cidade,
                    UserUpdated = GETDATE()
                WHERE UserId = @UserId
        END        
END;
GO

-- Função que retorna um ou todos os usuários
CREATE OR ALTER PROCEDURE DotNetDatabase.spUsuario_Get
    @UserId INT = NULL
AS
BEGIN
    SELECT UserId,
            Email,
            Nome,
            Pais,
            Estado,
            Cidade
    FROM DotNetDatabase.Usuarios AS Usuarios 
        WHERE Usuarios.UserId = ISNULL(@UserId, Usuarios.UserId)
END
GO

-- Função para criar ou atualizar uma experiência 
CREATE OR ALTER PROCEDURE DotNetDatabase.spExperiencia_Upsert
    @UserId INT,
    @ExperienciaId INT = NULL,
    @Setor NVARCHAR(50),
    @NomeEmpresa NVARCHAR(50),
    @Situacao NVARCHAR(50),
    @TipoEmprego NVARCHAR(50),
    @Pais NVARCHAR(50),
    @Estado NVARCHAR(50) = NULL,
    @Cidade NVARCHAR(50) = NULL
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM DotNetDatabase.Experiencias WHERE ExperienciaId = @ExperienciaId)
        BEGIN
            INSERT INTO DotNetDatabase.Experiencias(
                [UserId],
                [Setor],
                [NomeEmpresa],
                [Situacao],
                [TipoEmprego],
                [Pais],
                [Estado],
                [Cidade],
                [ExperienciaCreated],
                [ExperienciaUpdated]
            ) VALUES (
                @UserId,
                @Setor,
                @NomeEmpresa,
                @Situacao,
                @TipoEmprego,
                @Pais,
                @Estado,
                @Cidade,
                GETDATE(),
                GETDATE()
            )
        END
    ELSE
        BEGIN
            UPDATE DotNetDatabase.Experiencias 
                SET Setor = @Setor,
                    NomeEmpresa = @NomeEmpresa,
                    Situacao = @Situacao,
                    TipoEmprego = @TipoEmprego,
                    Pais = @Pais,
                    Estado = @Estado,
                    Cidade = @Cidade,
                    ExperienciaUpdated = GETDATE()
                WHERE ExperienciaId = @ExperienciaId AND UserId = @UserId
        END
END
GO

-- Retorna as experiências cadastradas de um usuário 
CREATE OR ALTER PROCEDURE DotNetDatabase.spExperiencia_Get
    @UserId INT
AS
BEGIN
    SELECT [Exp].[ExperienciaId],
        [Exp].[Setor],
        [Exp].[NomeEmpresa],
        [Exp].[Situacao],
        [Exp].[TipoEmprego],
        [Exp].[Pais],
        [Exp].[Estado],
        [Exp].[Cidade]
    FROM DotNetDatabase.Experiencias AS Exp
        WHERE Exp.UserId = @UserId
END
GO

-- Função para criar ou atualizar uma Graduação 
CREATE OR ALTER PROCEDURE DotNetDatabase.spGraduacao_Upsert
    @UserId INT,
    @GraduacaoId NVARCHAR(50),
    @Instituicao NVARCHAR(50),
    @Curso NVARCHAR(50),
    @Situacao NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM DotNetDatabase.Graduacao WHERE GraduacaoId = @GraduacaoId)
        BEGIN
            INSERT INTO DotNetDatabase.Graduacao(
                [UserId],
                [Instituicao],
                [Curso],
                [Situacao],
                [GraduacaoCreated],
                [GraduacaoUpdated]
            ) VALUES (
                @UserId,
                @Instituicao,
                @Curso,
                @Situacao,
                GETDATE(),
                GETDATE()
            )
        END
    ELSE
        BEGIN
            UPDATE DotNetDatabase.Graduacao 
                SET Instituicao = @Instituicao,
                    Curso = @Curso,
                    Situacao = @Situacao,
                    GraduacaoUpdated = GETDATE()
                WHERE GraduacaoId = @GraduacaoId AND UserId = @UserId
        END
END
GO

-- Retorna as graduações cadastradas de um usuário 
CREATE OR ALTER PROCEDURE DotNetDatabase.spGraduacao_Get
    @UserId INT
AS
BEGIN
    SELECT [Grad].[GraduacaoId],
        [Grad].[Instituicao],
        [Grad].[Curso],
        [Grad].[Situacao]
    FROM DotNetDatabase.Graduacao AS Grad
        WHERE Grad.UserId = @UserId
END
GO