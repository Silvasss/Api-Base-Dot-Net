-- DROP DATABASE DotNetDatabase

CREATE DATABASE DotNetDatabase
GO

USE DotNetDatabase
GO

INSERT INTO TipoContas
    (Nome, CreatedAt)
VALUES
    ('admin', GETDATE()),
    ('usuario', GETDATE()),
    ('instituicao', GETDATE())
GO

UPDATE Usuarios SET Tipo_Conta_Id = 1 WHERE Usuario_Id = 2
GO