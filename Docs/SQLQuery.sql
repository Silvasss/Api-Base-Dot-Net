-- DROP DATABASE DotNetDatabase

CREATE DATABASE DotNetDatabase
GO

USE DotNetDatabase
GO

CREATE SCHEMA DotNetDatabase
GO


CREATE TABLE Auth
(
    AuthId INT IDENTITY PRIMARY KEY
    ,
    Usuario NVARCHAR(32) UNIQUE NOT NULL
    ,
    PasswordHash VARBINARY(MAX) NOT NULL
    ,
    PasswordSalt VARBINARY(MAX) NOT NULL
    ,
    AuthCreated DATETIME DEFAULT GETDATE()
    ,
    AuthUpdated DATETIME NOT NULL
)
GO

CREATE TABLE Instituicao(
    InstituicaoId INT IDENTITY PRIMARY KEY
    ,
    Nome NVARCHAR(50) NOT NULL UNIQUE
    ,
    PlusCode NVARCHAR(50) NOT NULL
    , 
    InstituicaoCreated DATETIME DEFAULT GETDATE()
    ,
    InstituicaoUpdated DATETIME NOT NULL
    ,
    IsActive BIT DEFAULT 1
    ,
    Auth_Usuario NVARCHAR(32) FOREIGN KEY REFERENCES Auth(Usuario) ON DELETE CASCADE
)
GO

-- Criação da tabela de usuários com detalhes individuais
CREATE TABLE Users
(
    UserId INT PRIMARY KEY IDENTITY
    ,
    Nome NVARCHAR(50) NOT NULL
    ,
    Pais NVARCHAR(50) NOT NULL DEFAULT 'Brasil'
    ,
    PlusCode NVARCHAR(150) DEFAULT 'RM88+4G Plano Diretor Sul, Palmas - State of Tocantins' -- Plus Codes technology is open source and free to create and use.
    ,
    IsActive BIT DEFAULT 1
    ,
    UserCreated DATETIME DEFAULT GETDATE()
    ,
    UserUpdated DATETIME NOT NULL
    ,
    Auth_Usuario NVARCHAR(32) FOREIGN KEY REFERENCES Auth(Usuario) ON DELETE CASCADE
)
GO

-- Criação da tabela de funções que categoriza os usuários, concedendo níveis de acesso
CREATE TABLE Roles
(
    RoleId INT PRIMARY KEY IDENTITY(1,1)
    ,
    Nome NVARCHAR(50) NOT NULL
    ,
    Description NVARCHAR(255) NOT NULL
)

-- Criação da tabela de associação entre usuários e suas respectivas funções
CREATE TABLE UserRoles
(
    UserRoleId INT PRIMARY KEY IDENTITY(1,1)
    ,
    User_Id INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE
    ,
    Instituicao_Id INT 
    ,
    Role_Id INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleId)
)

-- Criação da tabela de logs de auditoria que monitora e captura atividades dos usuários
CREATE TABLE AuditLogs
(
    LogId INT PRIMARY KEY IDENTITY(1,1)
    ,
    User_Id INT NOT NULL
    ,
    Instituicao_Id INT NOT NULL
    ,
    ActivityType NVARCHAR(10) NOT NULL
    ,
    Timestamp DATETIME DEFAULT GETDATE()
    ,
    Description NVARCHAR(MAX) NOT NULL
)
GO

-- Criação das três funções (admin, usuario e instituicao) para as tabelas 'Roles' e 'UserRoles'. 
CREATE OR ALTER PROCEDURE spCreateRoles
AS
BEGIN
    INSERT INTO Roles
        (Nome, Description)
    VALUES
        ('admin', 'Full access to the system'),
        ('usuario', 'Can edit and create content'),
        ('instituicao', 'Limited access to the system')
END
GO

-- Executar todas as vezes que banco for criado
EXEC spCreateRoles
GO

-- Recupera o hash de senha e o salt de um usuário
CREATE OR ALTER PROCEDURE spLoginConfirmation_Get
    @Usuario NVARCHAR(32)
AS
BEGIN
    SELECT PasswordHash,
        PasswordSalt 
    FROM Auth 
        WHERE Usuario = @Usuario
END
GO

-- Para inserir um novo registro de usuário nas tabelas "Auth", "Usuarios" e "UserRoles" se um usuário com o 
-- mesmo nome de usuário ainda não existir. Caso contrário, ela retorna false.
CREATE OR ALTER PROCEDURE spRegistration_insert
    @Usuario NVARCHAR(32),
    @PasswordHash VARBINARY(MAX),
    @PasswordSalt VARBINARY(MAX)
AS 
BEGIN
    IF NOT EXISTS (SELECT * FROM Auth WHERE Usuario = @Usuario)
        BEGIN
            INSERT INTO Auth(
                [Usuario],
                [PasswordHash],
                [PasswordSalt],
                [AuthUpdated]
            ) VALUES (
                @Usuario,
                @PasswordHash,
                @PasswordSalt,
                GETDATE() 
            )

            DECLARE @OutputUserId INT

            INSERT INTO Users
                (
                    [Nome],
                    [UserUpdated],
                    [Auth_Usuario]
                )
            VALUES
                (
                    @Usuario,
                    GETDATE(),
                    @Usuario            
                )

            SET @OutputUserId = @@IDENTITY

            INSERT INTO UserRoles
                (
                    [User_Id],
                    [Role_Id]
                )
            VALUES
                (
                    @OutputUserId,
                    2 -- usuario    
                )
            
            SELECT @OutputUserId
        END   
END
GO

-- Retorna o ID do usuário e a role
CREATE OR ALTER PROCEDURE spUser_GetLogin
    @Usuario NVARCHAR(32)
AS
BEGIN
    IF EXISTS (SELECT * FROM Users WHERE Auth_Usuario = @Usuario)
        BEGIN
            SELECT 
                u.UserId AS 'Id',
                r.Nome AS 'Role'
            FROM 
                Users u
            INNER JOIN 
                UserRoles ur ON ur.User_Id = u.UserId
            INNER JOIN 
                Roles r ON r.RoleId = ur.Role_Id 
            WHERE 
                Auth_Usuario = @Usuario 
        END
    ELSE 
        BEGIN            
            SELECT 
                i.InstituicaoId AS 'Id',
                r.Nome AS 'Role'
            FROM 
                Instituicao i
            INNER JOIN 
                UserRoles ur ON ur.Instituicao_Id = i.InstituicaoId
            INNER JOIN 
                Roles r ON ur.Role_Id = r.RoleId
            WHERE 
                Auth_Usuario = @Usuario  
        END
END
GO

-- Para atualizar as informações de um usuário existente na tabela "Usuarios"
CREATE OR ALTER PROCEDURE spUser_PerfilUpdate
    @UserId INT,
    @Nome NVARCHAR(50),
	@Pais NVARCHAR(150) = NULL,
    @PlusCode NVARCHAR(50) = NULL
AS
BEGIN
    IF EXISTS (SELECT * FROM Users WHERE UserId = @UserId)
        BEGIN
            UPDATE Users 
                SET Nome = @Nome,
                    Pais = @Pais,
                    PlusCode = @PlusCode,
                    UserUpdated = GETDATE()
                WHERE UserId = @UserId
        END           
END
GO

-- Atualizar o hash de senha, salt de senha e a data de atualização na tabela "Auth" 
-- para um usuário específico com base no ID do usuário fornecido
CREATE OR ALTER PROCEDURE spUser_SenhaUpdate
    @Usuario NVARCHAR(32),
    @PasswordHash VARBINARY(MAX),
    @PasswordSalt VARBINARY(MAX)
AS 
BEGIN
    UPDATE Auth 
        SET PasswordHash = @PasswordHash,
            PasswordSalt = @PasswordSalt,
            AuthUpdated = GETDATE()
        WHERE Usuario = @Usuario         
END
GO

-- Excluir permanentemente seus dados. 
-- Isso pode ser útil em cenários onde é necessário manter um registro do usuário, 
-- mas ele não deve ter acesso ativo ao sistema.
CREATE OR ALTER PROCEDURE spUser_Delete
    @UserId INT
AS 
BEGIN
    DECLARE @jsonProcedure NVARCHAR(MAX)

    SET
        @jsonProcedure =
    (
        SELECT 
            u.*,
            (
                SELECT 
                    e.*
                FROM 
                    Experiencia e
                WHERE 
                    e.UserId = u.UserId 
                FOR JSON AUTO
            ) AS 'Experiencia',
            (
                SELECT 
                    g.*
                FROM 
                    Graduacao g
                WHERE 
                    u.UserId = g.UserId
                FOR JSON AUTO
            ) AS 'Graduacao'
        FROM 
            Users u
        WHERE
            u.UserId = @UserId  
        FOR JSON PATH, ROOT('Dados')
    )

    -- Criação do log
    INSERT INTO AuditLogs(
        [ActivityType],
        [Description],
        [User_Id]
    ) VALUES (
        'Delete',
        @jsonProcedure,
        @UserId
    )

    DELETE ath
    FROM Auth AS ath
    INNER JOIN Users AS u
        ON u.Auth_Usuario = ath.Usuario
    WHERE UserId = @UserId
END
GO

----------------------------------------------------------------------------------------
------------------------------ Outras tabelas do usuário -------------------------------
----------------------------------------------------------------------------------------
CREATE TABLE Experiencia(
    ExperienciaId INT IDENTITY PRIMARY KEY
    ,
    Setor NVARCHAR(50) NOT NULL
    , 
    Empresa NVARCHAR(50) NOT NULL
    ,
    Situacao NVARCHAR(50) NOT NULL
    ,
    Tipo NVARCHAR(50) NOT NULL
    ,
    PlusCode NVARCHAR(50) NOT NULL
    ,
    ExperienciaCreated DATETIME DEFAULT GETDATE()
    ,
    ExperienciaUpdated DATETIME NOT NULL
    ,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE
)
GO

-- Retorna as experiências de usuário
CREATE OR ALTER PROCEDURE spExperiencia_Get
    @UserId INT
AS
BEGIN    
    SELECT 
        ExperienciaId AS "Id",
        Setor AS "Setor",
        Empresa AS "Empresa",
        Situacao AS "Situacao",
        Tipo AS "Tipo",
        PlusCode AS "PlusCode"
    FROM 
        Experiencia 
    WHERE 
        UserId = @UserId        
END
GO

-- Para inserir ou atualizar informações de experiência de um usuário na tabela "Experiencia".
CREATE OR ALTER PROCEDURE spExperiencia_Upsert
    @UserId INT,
    @ExperienciaId INT = NULL,
    @Setor NVARCHAR(50),
    @Empresa NVARCHAR(50),
    @Situacao NVARCHAR(50),
    @Tipo NVARCHAR(50),
    @PlusCode NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Experiencia WHERE ExperienciaId = @ExperienciaId)
        BEGIN
            INSERT INTO Experiencia(
                [UserId],
                [Setor],
                [Empresa],
                [Situacao],
                [Tipo],
                [PlusCode],
                [ExperienciaUpdated]
            ) VALUES (
                @UserId,
                @Setor,
                @Empresa,
                @Situacao,
                @Tipo,
                @PlusCode,
                GETDATE()
            )
        END
    ELSE
        BEGIN
            UPDATE Experiencia
                SET Setor = @Setor,
                    Empresa = @Empresa,
                    Situacao = @Situacao,
                    Tipo = @Tipo,
                    PlusCode = @PlusCode,
                    ExperienciaUpdated = GETDATE()
                WHERE ExperienciaId = @ExperienciaId AND UserId = @UserId
        END
END
GO

-- Excluir permanentemente uma experiencia
CREATE OR ALTER PROCEDURE spExperiencia_Delete
    @UserId INT,
    @ExperienciaId INT
AS 
BEGIN
    DELETE FROM Experiencia WHERE ExperienciaId = @ExperienciaId AND UserId = @UserId      
END
GO

--  
CREATE TABLE Graduacao(
    Id INT IDENTITY PRIMARY KEY
    ,
    Situacao NVARCHAR(50) NOT NULL
    , 
    GraduacaoCreated DATETIME DEFAULT GETDATE()
    ,
    GraduacaoUpdated DATETIME NOT NULL
    ,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE
    ,
    Instituicao_Id INT FOREIGN KEY REFERENCES Instituicao(InstituicaoId)
    ,
    Curso_Id INT FOREIGN KEY REFERENCES Curso(CursoId)
)
GO

-- Retorna as graduações de usuário
CREATE OR ALTER PROCEDURE spGraduacao_Get
    @UserId INT
AS
BEGIN    
    SELECT
        g.Id AS "Id",
        g.Instituicao_Id AS "InstituicaoId",
        g.Curso_Id AS "CursoId",
        g.Situacao AS "Situacao",          

        c.Nome AS 'CursoNome',                    
        it.Nome AS 'InstituicaoNome'                            
    FROM 
        Graduacao AS g
    FULL JOIN Curso AS c
        ON c.CursoId = g.Curso_Id  
    FULL JOIN Instituicao AS it 
        ON InstituicaoId = g.Instituicao_Id  
    WHERE 
        g.UserId = @UserId        
END
GO

-- Para inserir ou atualizar informações de graduacao de um usuário na tabela "Graduacao".
CREATE OR ALTER PROCEDURE spGraduacao_Upsert
    @UserId INT,
    @GraduacaoId INT = NULL, -- Para atualização
    @InstituicaoId NVARCHAR(50),
    @CursoId INT,
    @Situacao NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Graduacao WHERE Id = @GraduacaoId)
        BEGIN
            DECLARE @OutputGraduacaoId INT

            INSERT INTO Graduacao(
                [UserId],
                [Instituicao_Id],
                [Curso_Id],
                [Situacao],
                [GraduacaoUpdated]
            ) VALUES (
                @UserId,
                @InstituicaoId,
                @CursoId,
                @Situacao,
                GETDATE()
            )

            SET @OutputGraduacaoId = @@IDENTITY

            INSERT INTO SolicitacaoCurso(
                [Instituicao_Id],
                [User_Id],
                [Curso_Id],
                [SituacaoUpdated]
            ) VALUES (
                @InstituicaoId,
                @UserId,
                @CursoId,
                GETDATE()
            )
        END
    ELSE
        BEGIN
            UPDATE Graduacao
                SET Situacao = @Situacao,
                    GraduacaoUpdated = GETDATE()
                WHERE Id = @GraduacaoId AND UserId = @UserId
        END
END
GO

-- Excluir permanentemente uma graduacao
CREATE OR ALTER PROCEDURE spGraduacao_Delete
    @UserId INT,
    @GraduacaoId INT
AS 
BEGIN
    DELETE FROM Graduacao WHERE Id = @GraduacaoId AND UserId = @UserId      
END
GO
                  
-- Retorna os dados de um usuário das tabelas Users, Experiencia e Graduacao
CREATE OR ALTER PROCEDURE spUser_Get
    @UserId INT = NULL
AS
BEGIN   
    IF (@UserId IS NOT NULL)    
        BEGIN
            SELECT 
                Nome,
                Pais,
                PlusCode               
            FROM 
                Users
            WHERE
                UserId = @UserId
        END  
    ELSE
        BEGIN
            SELECT [us].UserId AS "Id",
                [us].Nome,
                [us].Pais,
                [us].PlusCode
            FROM 
                Users AS us           
            INNER JOIN UserRoles AS ur
                ON ur.User_Id = us.UserId
            INNER JOIN Roles AS r
                ON r.RoleId = ur.Role_Id
            WHERE r.Nome = 'usuario'
        END
END
GO

----------------------------------------------------------------------------------------
------------------------------ Instituição ---------------------------------------------
----------------------------------------------------------------------------------------

-- Retorna os dados de uma instituição
-- CREATE OR ALTER PROCEDURE spInstituicao_Get
--     @UserId INT = NULL
-- AS
-- BEGIN          
--     SELECT 
--         Nome,
--         Pais,
--         PlusCode
--     FROM 
--         Users 
--     WHERE
--         UserId = @UserId         
-- END
-- GO

-- Para atualizar as informações de um usuário existente na tabela "Instituicao"
CREATE OR ALTER PROCEDURE spInstituicao_PerfilUpdate
    @Id INT,
    @Nome NVARCHAR(50),
	@PlusCode NVARCHAR(50)
AS
BEGIN
    BEGIN
        UPDATE Instituicao 
            SET Nome = @Nome,
                PlusCode = @PlusCode,
                InstituicaoUpdated = GETDATE()
            WHERE InstituicaoId = @Id
    END           
END
GO

-- Para marcar um instituicao como inativo, em vez de excluir permanentemente seus dados. 
-- Isso pode ser útil em cenários onde é necessário manter um registro da instituicao, 
-- mas ele não deve ter acesso ativo ao sistema.
CREATE OR ALTER PROCEDURE spInstituicao_RequestDelete
    @Id INT
AS 
BEGIN
    UPDATE Instituicao 
        SET IsActive = 0,
            InstituicaoUpdated = GETDATE()
        WHERE InstituicaoId = @Id         
END
GO

CREATE TABLE Curso(
    CursoId INT IDENTITY PRIMARY KEY
    , 
    Nome NVARCHAR(50) NOT NULL 
    ,
    CursoCreated DATETIME DEFAULT GETDATE()
    ,
    CursoUpdated DATETIME NOT NULL
    ,
    IsActive BIT DEFAULT 1
    ,
    Instituicao_Id INT FOREIGN KEY REFERENCES Instituicao(InstituicaoId) ON DELETE CASCADE
)
GO

-- Retorna os cursos de uma Instituição 
CREATE OR ALTER PROCEDURE spInstituicao_CursosGet
    @InstituicaoId INT
AS
BEGIN    
    SELECT 
        CursoId AS 'Id',
        Nome,
        IsActive
    FROM 
        Curso 
    WHERE
        Instituicao_Id = @InstituicaoId          
END
GO

-- Para inserir ou atualizar informações de curso de uma instituicao na tabela "Curso".
CREATE OR ALTER PROCEDURE spInstituicao_Curso_Upsert
    @InstituicaoId INT,
    @CursoId INT = NULL,
    @Nome NVARCHAR(50),
    @IsActive BIT = NULL
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Curso WHERE CursoId = @CursoId)
        BEGIN
            INSERT INTO Curso(
                [Instituicao_Id],
                [Nome],
                [CursoUpdated]
            ) VALUES (
                @InstituicaoId,
                @Nome,
                GETDATE()
            )
        END
    ELSE
        BEGIN
            UPDATE Curso
                SET Nome = @Nome,
                    IsActive = @IsActive,
                    CursoUpdated = GETDATE()
                WHERE CursoId = @CursoId AND Instituicao_Id = @InstituicaoId 
        END
END
GO

-- Excluir permanentemente um curso
CREATE OR ALTER PROCEDURE spInstituicao_Curso_Delete
    @InstituicaoId INT,
    @CursoId INT
AS 
BEGIN
    DELETE FROM Curso WHERE CursoId = @CursoId AND Instituicao_Id = @InstituicaoId      
END
GO

CREATE TABLE SolicitacaoCurso(
    SolicitacaoId INT IDENTITY PRIMARY KEY
    , 
    Situacao BIT DEFAULT 0 NOT NULL
    ,
    Explicacao NVARCHAR(50)
    ,
    SituacaoCreated DATETIME DEFAULT GETDATE()
    ,
    SituacaoUpdated DATETIME NOT NULL
    ,
    IsActive BIT DEFAULT 1
    ,
    Instituicao_Id INT FOREIGN KEY REFERENCES Instituicao(InstituicaoId)
    ,
    User_Id INT FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE
    ,
    Curso_Id INT FOREIGN KEY REFERENCES Curso(CursoId)
)
GO

-- Retorna as solicitações
CREATE OR ALTER PROCEDURE spInstituicao_SolicitacaoGet
    @SolicitacaoId INT = NULL,
    @InstituicaoId INT
AS
BEGIN    
    IF (@SolicitacaoId IS NOT NULL)    
        BEGIN
           SELECT 
                Situacao,
                Explicacao,
                IsActive,
                Curso_Id,
                User_Id
            FROM 
                SolicitacaoCurso 
            WHERE
                SolicitacaoId = @SolicitacaoId AND Instituicao_Id = @InstituicaoId
        END  
    ELSE
        BEGIN
            SELECT 
                Situacao,
                Explicacao,
                IsActive,
                Curso_Id,
                User_Id
            FROM 
                SolicitacaoCurso
            WHERE 
                Instituicao_Id = @InstituicaoId
        END        
END
GO

-- Para atualizar informações de uma solicitação de um usuario na tabela "SolicitacaoCurso".
CREATE OR ALTER PROCEDURE spInstituicao_Solicitacao_Update
    @InstituicaoId INT,
    @SolicitacaoId INT,
    @Situacao BIT = NULL,
    @Explicacao NVARCHAR(50),
    @IsActive BIT = NULL
AS
BEGIN        
    UPDATE SolicitacaoCurso
        SET Situacao = @Situacao,
            Explicacao = @Explicacao,
            IsActive = @IsActive,
            SituacaoUpdated = GETDATE()
        WHERE SolicitacaoId = @SolicitacaoId AND Instituicao_Id = @InstituicaoId
END
GO

----------------------------------------------------------------------------------------
------------------------------ ADMIN  --------------------------------------------------
----------------------------------------------------------------------------------------

-- Editar os dados de qualquer usuário, por meio das procedures usuário e instituição.

-- Retorna os usuários que solicitaram a exclusão da conta
CREATE OR ALTER PROCEDURE spAdmin_UserGetRequestDelete
AS
BEGIN       
    BEGIN
        SELECT UserId AS 'Id', Nome AS 'Nome', Pais AS 'Pais', PlusCode AS 'PlusCode' FROM Users WHERE IsActive = 0
    END
END
GO

-- Alterar o nivel de um usuário.
CREATE OR ALTER PROCEDURE spAdmin_UserUpdateRole
    @UserId INT,
    @Role INT
AS
BEGIN       
    BEGIN
        UPDATE UserRoles SET Role_Id = @Role WHERE User_Id = @UserId

        -- Criação do log
        INSERT INTO AuditLogs(
            [ActivityType],
            [Description],
            [User_Id],
            [Instituicao_Id]
        ) VALUES (
            'Update',
            'Alteração do tipo da conta',
            @UserId,
            0
        ) 
    END
END
GO

-- Para inserir uma conta do tipo 'instituição' na tabela "Instituicao".
CREATE OR ALTER PROCEDURE spAdmin_InstituicaoInsert
    @Usuario NVARCHAR(32),
    @PasswordHash VARBINARY(MAX),
    @PasswordSalt VARBINARY(MAX),
    @Nome NVARCHAR(50),
    @PlusCode NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Instituicao WHERE Auth_Usuario = @Usuario) AND NOT EXISTS (SELECT * FROM Auth WHERE Usuario = @Usuario)
        BEGIN                          
            INSERT INTO Auth(
                [Usuario],
                [PasswordHash],
                [PasswordSalt],
                [AuthUpdated]
            ) VALUES (
                @Usuario,
                @PasswordHash,
                @PasswordSalt,
                GETDATE() 
            )

            DECLARE @OutputInstituicaoId INT

            INSERT INTO Instituicao(
                [Nome],
                [PlusCode],
                [Auth_Usuario],
                [InstituicaoUpdated]
            ) VALUES (
                @Nome,
                @PlusCode,
                @Usuario,
                GETDATE()
            )     

            SET @OutputInstituicaoId = @@IDENTITY

            INSERT INTO UserRoles
                (
                    [Instituicao_Id],
                    [Role_Id]
                )
            VALUES
                (
                    @OutputInstituicaoId,
                    3 -- instituicao    
                )         

            -- Criação do log
            INSERT INTO AuditLogs(
                [ActivityType],
                [Description],
                [Instituicao_Id],
                [User_Id]
            ) VALUES (
                'Create',
                'Instituição',
                @OutputInstituicaoId,
                0
            )                    
        END    
END
GO

-- Logs do sistema. Retorna os logs de alterações de contas da tabelas 'AuditLogs'
CREATE OR ALTER PROCEDURE spAdmin_LogGet
AS
BEGIN       
    BEGIN
        SELECT 
            LogId AS 'Id',
            User_Id AS 'UserId',
            Instituicao_Id AS 'InstituicaoId',
            ActivityType AS 'ActivityType',
            Timestamp AS 'CreatedDate',
            Description AS 'Description'
        FROM AuditLogs
    END
END
GO