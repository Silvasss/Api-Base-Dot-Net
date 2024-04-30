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