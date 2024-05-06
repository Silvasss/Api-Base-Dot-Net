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

-- População da tabela 'Auth'
Declare @jsonVariable varchar(max)

SET @jsonVariable = N'[
        {
            "Usuario": "Ht8Ks2Xq",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ej7Ld3Mz",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Qn9Ib5Wv",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ry2Gf6Uc",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Jw4Tz1Xm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Kd5Sv9Nh",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Bm6Aq3Ej",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ow7Iy2Zp",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Xf8Gu4Lm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Nh9Vc5Ej",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Tz2Aw7Xm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Qs3Ib6Uc",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ry4Gf9Zp",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Jw5Tz2Lm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Kd6Sv3Nh",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Bm7Aq4Ej",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ow8Iy5Zp",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Xf9Gu6Lm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Nh3Vc7Ej",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Tz4Aw8Xm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Qs5Ib9Uc",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ry6Gf3Zp",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Jw7Tz4Lm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Kd8Sv5Nh",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Bm9Aq6Ej",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ow3Iy7Zp",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Xf4Gu8Lm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Nh5Vc9Ej",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Tz6Aw3Xm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Qs7Ib4Uc",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ry8Gf5Zp",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Jw9Tz6Lm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Kd3Sv7Nh",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Bm4Aq8Ej",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ow5Iy9Zp",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Xf6Gu3Lm",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ht8Ks2Xq11",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ej7Ld3Mz22",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Qn9Ib5Wv33",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        },
        {
            "Usuario": "Ry2Gf6Uc44",
            "PasswordHash": "0x059723ABE080290B089151465E0527A49D52E723324A3877C71BB96D68DF491C",
            "PasswordSalt": "0x0CF973149F8E070B19942BBECF68B43E"
        }
    ]'

INSERT INTO Auth
    (Usuario, PasswordHash, PasswordSalt)
SELECT 
    Usuario, PasswordHash, PasswordSalt
FROM OPENJSON(@jsonVariable, N'$')
    WITH (
        Usuario NVARCHAR(32) N'$.Usuario',
        PasswordHash NVARCHAR(MAX) N'$.PasswordHash',
        PasswordSalt NVARCHAR(MAX) N'$.PasswordSalt'
    )

-- População da tabela 'Usuarios'
Declare @jsonVariableUsuario varchar(max)

SET @jsonVariableUsuario = N'[
        {"Nome": "Alice", "PlusCode": "X234+56", "Usuario_Id": 1},
        {"Nome": "Bob", "PlusCode": "Y789+12", "Usuario_Id": 2},
        {"Nome": "Charlie", "PlusCode": "Z456+78", "Usuario_Id": 3},
        {"Nome": "David", "PlusCode": "A123+45", "Usuario_Id": 4},
        {"Nome": "Eve", "PlusCode": "B678+90", "Usuario_Id": 5},
        {"Nome": "Frank", "PlusCode": "C345+67", "Usuario_Id": 6},
        {"Nome": "Grace", "PlusCode": "D901+23", "Usuario_Id": 7},
        {"Nome": "Hannah", "PlusCode": "E456+78", "Usuario_Id": 8},
        {"Nome": "Ian", "PlusCode": "F012+34", "Usuario_Id": 9},
        {"Nome": "Julia", "PlusCode": "G567+89", "Usuario_Id": 10},
        {"Nome": "Kevin", "PlusCode": "H234+56", "Usuario_Id": 11},
        {"Nome": "Lily", "PlusCode": "I789+12", "Usuario_Id": 12},
        {"Nome": "Mike", "PlusCode": "J456+78", "Usuario_Id": 13},
        {"Nome": "Nancy", "PlusCode": "K123+45", "Usuario_Id": 14},
        {"Nome": "Oliver", "PlusCode": "L678+90", "Usuario_Id": 15},
        {"Nome": "Pam", "PlusCode": "M345+67", "Usuario_Id": 16},
        {"Nome": "Quinn", "PlusCode": "N901+23", "Usuario_Id": 17},
        {"Nome": "Rachel", "PlusCode": "O456+78", "Usuario_Id": 18},
        {"Nome": "Sam", "PlusCode": "P012+34", "Usuario_Id": 19},
        {"Nome": "Tina", "PlusCode": "Q567+89", "Usuario_Id": 20},
        {"Nome": "Ursula", "PlusCode": "R234+56", "Usuario_Id": 21},
        {"Nome": "Victor", "PlusCode": "S789+12", "Usuario_Id": 22},
        {"Nome": "Wendy", "PlusCode": "T456+78", "Usuario_Id": 23},
        {"Nome": "Xavier", "PlusCode": "U123+45", "Usuario_Id": 24},
        {"Nome": "Yara", "PlusCode": "V678+90", "Usuario_Id": 25},
        {"Nome": "Zack", "PlusCode": "W345+67", "Usuario_Id": 26},
        {"Nome": "Amy", "PlusCode": "X901+23", "Usuario_Id": 27},
        {"Nome": "Ben", "PlusCode": "Y456+78", "Usuario_Id": 28},
        {"Nome": "Cathy", "PlusCode": "Z012+34", "Usuario_Id": 29},
        {"Nome": "Derek", "PlusCode": "A567+89", "Usuario_Id": 30},
        {"Nome": "Emily", "PlusCode": "B234+56", "Usuario_Id": 31},
        {"Nome": "Finn", "PlusCode": "C789+12", "Usuario_Id": 32},
        {"Nome": "Gina", "PlusCode": "D456+78", "Usuario_Id": 33},
        {"Nome": "Henry", "PlusCode": "E123+45", "Usuario_Id": 34},
        {"Nome": "Ivy", "PlusCode": "F678+90", "Usuario_Id": 35},
        {"Nome": "Jack", "PlusCode": "G345+67", "Usuario_Id": 36},
        {"Nome": "Kara", "PlusCode": "H901+23", "Usuario_Id": 37},
        {"Nome": "Leo", "PlusCode": "I456+78", "Usuario_Id": 38},
        {"Nome": "Mia", "PlusCode": "J012+34", "Usuario_Id": 39},
        {"Nome": "Noah", "PlusCode": "K567+89", "Usuario_Id": 40}
    ],'

INSERT INTO Usuarios
    (Nome, PlusCode, Auth_Id)
SELECT 
    Nome, PlusCode, Usuario_Id
FROM OPENJSON(@jsonVariableUsuario, N'$')
    WITH (
        Nome nvarchar(50) N'$.Nome',
        PlusCode nvarchar(150) N'$.PlusCode',
        Usuario_Id INT N'$.Usuario_Id'
    )

-- População da tabela 'Experiências'
Declare @jsonVariableExperiencias varchar(max)

SET @jsonVariableExperiencias = N'[
        {
            "Setor": "Comércio",
            "Empresa": "Lojas Americanas",
            "PlusCode": "W6R4C9G4+R4",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 1,
            "Inicio": "2022-01-01T00:00:00.0000000",
            "Fim": "2022-12-31T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Natura",
            "PlusCode": "W6R4C9G5+R5",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 2,
            "Inicio": "2021-06-01T00:00:00.0000000",
            "Fim": "2022-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Vale",
            "PlusCode": "W6R4C9G6+R6",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 3,
            "Inicio": "2020-03-01T00:00:00.0000000",
            "Fim": "2021-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Gol",
            "PlusCode": "W6R4C9G7+R7",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 4,
            "Inicio": "2019-09-01T00:00:00.0000000",
            "Fim": "2020-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Casas Bahia",
            "PlusCode": "W6R4C9G8+R8",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 5,
            "Inicio": "2018-06-01T00:00:00.0000000",
            "Fim": "2019-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Itaú",
            "PlusCode": "W6R4C9G9+R9",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 6,
            "Inicio": "2017-03-01T00:00:00.0000000",
            "Fim": "2018-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Oi",
            "PlusCode": "W6R4C9G10+R10",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 7,
            "Inicio": "2016-09-01T00:00:00.0000000",
            "Fim": "2017-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Extra",
            "PlusCode": "W6R4C9G11+R11",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 8,
            "Inicio": "2015-06-01T00:00:00.0000000",
            "Fim": "2016-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Braskem",
            "PlusCode": "W6R4C9G12+R12",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 9,
            "Inicio": "2014-03-01T00:00:00.0000000",
            "Fim": "2015-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Telefónica",
            "PlusCode": "W6R4C9G13+R13",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 10,
            "Inicio": "2013-09-01T00:00:00.0000000",
            "Fim": "2014-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Pão de Açúcar",
            "PlusCode": "W6R4C9G14+R14",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 11,
            "Inicio": "2012-06-01T00:00:00.0000000",
            "Fim": "2013-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Vale",
            "PlusCode": "W6R4C9G15+R15",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 12,
            "Inicio": "2011-03-01T00:00:00.0000000",
            "Fim": "2012-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Gol",
            "PlusCode": "W6R4C9G16+R16",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 13,
            "Inicio": "2010-09-01T00:00:00.0000000",
            "Fim": "2011-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Casas Bahia",
            "PlusCode": "W6R4C9G17+R17",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 14,
            "Inicio": "2009-06-01T00:00:00.0000000",
            "Fim": "2010-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Itaú",
            "PlusCode": "W6R4C9G18+R18",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 15,
            "Inicio": "2008-03-01T00:00:00.0000000",
            "Fim": "2009-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Oi",
            "PlusCode": "W6R4C9G19+R19",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 16,
            "Inicio": "2007-09-01T00:00:00.0000000",
            "Fim": "2008-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Extra",
            "PlusCode": "W6R4C9G20+R20",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 17,
            "Inicio": "2006-06-01T00:00:00.0000000",
            "Fim": "2007-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Braskem",
            "PlusCode": "W6R4C9G21+R21",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 18,
            "Inicio": "2005-03-01T00:00:00.0000000",
            "Fim": "2006-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Telefónica",
            "PlusCode": "W6R4C9G22+R22",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 19,
            "Inicio": "2004-09-01T00:00:00.0000000",
            "Fim": "2005-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Pão de Açúcar",
            "PlusCode": "W6R4C9G23+R23",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 20,
            "Inicio": "2003-06-01T00:00:00.0000000",
            "Fim": "2004-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Vale",
            "PlusCode": "W6R4C9G24+R24",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 21,
            "Inicio": "2002-03-01T00:00:00.0000000",
            "Fim": "2003-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Gol",
            "PlusCode": "W6R4C9G25+R25",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 22,
            "Inicio": "2001-09-01T00:00:00.0000000",
            "Fim": "2002-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Casas Bahia",
            "PlusCode": "W6R4C9G26+R26",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 23,
            "Inicio": "2000-06-01T00:00:00.0000000",
            "Fim": "2001-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Itaú",
            "PlusCode": "W6R4C9G27+R27",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 24,
            "Inicio": "1999-03-01T00:00:00.0000000",
            "Fim": "2000-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Oi",
            "PlusCode": "W6R4C9G28+R28",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 25,
            "Inicio": "1998-09-01T00:00:00.0000000",
            "Fim": "1999-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Extra",
            "PlusCode": "W6R4C9G29+R29",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 26,
            "Inicio": "1997-06-01T00:00:00.0000000",
            "Fim": "1998-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Braskem",
            "PlusCode": "W6R4C9G30+R30",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 27,
            "Inicio": "1996-03-01T00:00:00.0000000",
            "Fim": "1997-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Telefónica",
            "PlusCode": "W6R4C9G31+R31",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 28,
            "Inicio": "1995-09-01T00:00:00.0000000",
            "Fim": "1996-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Pão de Açúcar",
            "PlusCode": "W6R4C9G32+R32",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 29,
            "Inicio": "1994-06-01T00:00:00.0000000",
            "Fim": "1995-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Vale",
            "PlusCode": "W6R4C9G33+R33",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 30,
            "Inicio": "1993-03-01T00:00:00.0000000",
            "Fim": "1994-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Gol",
            "PlusCode": "W6R4C9G34+R34",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 31,
            "Inicio": "1992-09-01T00:00:00.0000000",
            "Fim": "1993-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Casas Bahia",
            "PlusCode": "W6R4C9G35+R35",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 32,
            "Inicio": "1991-06-01T00:00:00.0000000",
            "Fim": "1992-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Itaú",
            "PlusCode": "W6R4C9G36+R36",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 33,
            "Inicio": "1990-03-01T00:00:00.0000000",
            "Fim": "1991-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Oi",
            "PlusCode": "W6R4C9G37+R37",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 34,
            "Inicio": "1989-09-01T00:00:00.0000000",
            "Fim": "1990-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Extra",
            "PlusCode": "W6R4C9G38+R38",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 35,
            "Inicio": "1988-06-01T00:00:00.0000000",
            "Fim": "1989-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Vale",
            "PlusCode": "W6R4C9G40+R40",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 36,
            "Inicio": "1987-03-01T00:00:00.0000000",
            "Fim": "1988-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Gol",
            "PlusCode": "W6R4C9G41+R41",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 37,
            "Inicio": "1986-09-01T00:00:00.0000000",
            "Fim": "1987-08-31T23:59:59.9999999"
        },
        {
            "Setor": "Comércio",
            "Empresa": "Casas Bahia",
            "PlusCode": "W6R4C9G42+R42",
            "Vinculo": "Colaborador",
            "Ativo": false,
            "Usuario_Id": 38,
            "Inicio": "1985-06-01T00:00:00.0000000",
            "Fim": "1986-05-31T23:59:59.9999999"
        },
        {
            "Setor": "Indústria",
            "Empresa": "Itaú",
            "PlusCode": "W6R4C9G43+R43",
            "Vinculo": "Empregado",
            "Ativo": false,
            "Usuario_Id": 39,
            "Inicio": "1984-03-01T00:00:00.0000000",
            "Fim": "1985-02-28T23:59:59.9999999"
        },
        {
            "Setor": "Serviços",
            "Empresa": "Oi",
            "PlusCode": "W6R4C9G44+R44",
            "Vinculo": "Funcionário",
            "Ativo": false,
            "Usuario_Id": 40,
            "Inicio": "1983-09-01T00:00:00.0000000",
            "Fim": "1984-08-31T23:59:59.9999999"
        }
    ]'

INSERT INTO Experiencia
    (Setor, Empresa, PlusCode, Vinculo, Ativo, Inicio, Fim, Usuario_Id)
SELECT
    Setor, Empresa, PlusCode, Vinculo, Ativo, Inicio, Fim, Usuario_Id
FROM OPENJSON(@jsonVariableExperiencias, N'$')
    WITH (
        Setor nvarchar(50) N'$.Setor',
        Empresa nvarchar(50) N'$.Empresa',
        PlusCode nvarchar(150) N'$.PlusCode',
        Vinculo nvarchar(50) N'$.Vinculo',
        Ativo BIT N'$.Ativo',
        Inicio DATETIME2(7) N'$.Inicio',
        Fim DATETIME2(7) N'$.Fim',
        Usuario_Id INT N'$.Usuario_Id'
    )

-- População da tabela 'Auth' para a tabela 'Instituicao'
INSERT Auth
    (Usuario, PasswordHash, PasswordSalt)
VALUES
    ('luteranoPalmas', 0x300078003000350039003700320033004100420045003000380030003200390030004200300038003900310035003100340036003500450030003500320037004100340039004400350032004500370032003300330032003400410033003800370037004300370031004200420039003600440036003800440046003400390031004300,
        0x3000780030004300460039003700330031003400390046003800450030003700300042003100390039003400320042004200450043004600360038004200340033004500),
    ('catolicaTocantins', 0x300078003000350039003700320033004100420045003000380030003200390030004200300038003900310035003100340036003500450030003500320037004100340039004400350032004500370032003300330032003400410033003800370037004300370031004200420039003600440036003800440046003400390031004300,
        0x3000780030004300460039003700330031003400390046003800450030003700300042003100390039003400320042004200450043004600360038004200340033004500),
    ('fapalPalmas', 0x300078003000350039003700320033004100420045003000380030003200390030004200300038003900310035003100340036003500450030003500320037004100340039004400350032004500370032003300330032003400410033003800370037004300370031004200420039003600440036003800440046003400390031004300,
        0x3000780030004300460039003700330031003400390046003800450030003700300042003100390039003400320042004200450043004600360038004200340033004500),
    ('uftPalmas', 0x300078003000350039003700320033004100420045003000380030003200390030004200300038003900310035003100340036003500450030003500320037004100340039004400350032004500370032003300330032003400410033003800370037004300370031004200420039003600440036003800440046003400390031004300,
        0x3000780030004300460039003700330031003400390046003800450030003700300042003100390039003400320042004200450043004600360038004200340033004500)

-- População da tabela 'Instituicao'
INSERT Instituicao
    (Nome, PlusCode, Auth_Id, Tipo_Conta_Id)
VALUES
    ('Centro Universitário Luterano de Palmas', 'PMC8+G4 Plano Diretor Expansão Sul, Palmas - State of Tocantins', 41, 3),
    ('Centro Universitário Católica do Tocantins', 'PMH9+MC Palmas, State of Tocantins', 42, 3),
    ('FAPAL - Faculdade de Palmas', 'QMR9+G5 Plano Diretor Sul, Palmas - State of Tocantins', 43, 3),
    ('Instituto Federal do Tocantins', 'RJCR+73 Plano Diretor Norte, Palmas - State of Tocantins', 44, 3)

-- População da tabela 'Curso'
INSERT Curso
    (Nome, Instituicao_Id)
VALUES
    ('Agronomia', 1),
    ('Biomedicina', 1),
    ('Ciência da Computação', 1),
    ('Direito', 1),
    ('Engenharia Mecânica Automotiva', 1),
    ('Administração', 2),
    ('Ciências Contábeis', 2),
    ('Enfermagem', 2),
    ('Medicina Veterinária', 2),
    ('Pedagogia', 2),
    ('Arquitetura e Urbanismo', 3),
    ('Biomedicina', 3),
    ('Ciência da Computação', 3),
    ('Direito', 3),
    ('Educação Física (Bacharelado)', 3),
    ('Administração', 4),
    ('Engenharia Agronômica', 4),
    ('Ciências Biológicas', 4),
    ('Física', 4),
    ('Matemática', 4)

-- População da tabela 'Graduacao'
Declare @jsonVariableGraduacao varchar(max)

SET @jsonVariableGraduacao = N'[
        {
            "Situacao": "Ativa",
            "Curso_Id": 1,
            "InstituicaoId": 1,
            "Usuario_Id": 1,
            "Inicio": "2016-09-01 08:00:00.0000000",
            "Fim": "2020-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 2,
            "InstituicaoId": 1,
            "Usuario_Id": 2,
            "Inicio": "2018-02-15 10:15:00.0000000",
            "Fim": "2022-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 3,
            "InstituicaoId": 1,
            "Usuario_Id": 3,
            "Inicio": "2019-05-01 09:30:00.0000000",
            "Fim": "2023-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 4,
            "InstituicaoId": 1,
            "Usuario_Id": 4,
            "Inicio": "2020-01-01 08:00:00.0000000",
            "Fim": "2024-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 5,
            "InstituicaoId": 1,
            "Usuario_Id": 5,
            "Inicio": "2021-03-15 10:15:00.0000000",
            "Fim": "2025-11-30 14:45:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 1,
            "InstituicaoId": 1,
            "Usuario_Id": 6,
            "Inicio": "2017-09-01 08:00:00.0000000",
            "Fim": "2021-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 2,
            "InstituicaoId": 1,
            "Usuario_Id": 7,
            "Inicio": "2019-02-15 10:15:00.0000000",
            "Fim": "2023-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 3,
            "InstituicaoId": 1,
            "Usuario_Id": 8,
            "Inicio": "2020-05-01 09:30:00.0000000",
            "Fim": "2024-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 4,
            "InstituicaoId": 1,
            "Usuario_Id": 9,
            "Inicio": "2021-01-01 08:00:00.0000000",
            "Fim": "2025-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 5,
            "InstituicaoId": 1,
            "Usuario_Id": 10,
            "Inicio": "2022-03-15 10:15:00.0000000",
            "Fim": "2026-11-30 14:45:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 1,
            "InstituicaoId": 2,
            "Usuario_Id": 11,
            "Inicio": "2018-09-01 08:00:00.0000000",
            "Fim": "2022-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 2,
            "InstituicaoId": 2,
            "Usuario_Id": 12,
            "Inicio": "2020-02-15 10:15:00.0000000",
            "Fim": "2024-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 3,
            "InstituicaoId": 2,
            "Usuario_Id": 13,
            "Inicio": "2021-05-01 09:30:00.0000000",
            "Fim": "2025-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 4,
            "InstituicaoId": 2,
            "Usuario_Id": 14,
            "Inicio": "2022-01-01 08:00:00.0000000",
            "Fim": "2026-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 5,
            "InstituicaoId": 2,
            "Usuario_Id": 15,
            "Inicio": "2023-03-15 10:15:00.0000000",
            "Fim": "2027-11-30 14:45:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 1,
            "InstituicaoId": 2,
            "Usuario_Id": 16,
            "Inicio": "2019-09-01 08:00:00.0000000",
            "Fim": "2023-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 2,
            "InstituicaoId": 2,
            "Usuario_Id": 17,
            "Inicio": "2021-02-15 10:15:00.0000000",
            "Fim": "2025-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 3,
            "InstituicaoId": 2,
            "Usuario_Id": 18,
            "Inicio": "2022-05-01 09:30:00.0000000",
            "Fim": "2026-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 4,
            "InstituicaoId": 2,
            "Usuario_Id": 19,
            "Inicio": "2023-01-01 08:00:00.0000000",
            "Fim": "2027-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 5,
            "InstituicaoId": 2,
            "Usuario_Id": 20,
            "Inicio": "2024-03-15 10:15:00.0000000",
            "Fim": "2028-11-30 14:45:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 1,
            "InstituicaoId": 3,
            "Usuario_Id": 21,
            "Inicio": "2020-09-01 08:00:00.0000000",
            "Fim": "2024-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 2,
            "InstituicaoId": 3,
            "Usuario_Id": 22,
            "Inicio": "2022-02-15 10:15:00.0000000",
            "Fim": "2026-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 3,
            "InstituicaoId": 3,
            "Usuario_Id": 23,
            "Inicio": "2023-05-01 09:30:00.0000000",
            "Fim": "2027-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 4,
            "InstituicaoId": 3,
            "Usuario_Id": 24,
            "Inicio": "2024-01-01 08:00:00.0000000",
            "Fim": "2028-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 5,
            "InstituicaoId": 3,
            "Usuario_Id": 25,
            "Inicio": "2025-03-15 10:15:00.0000000",
            "Fim": "2029-11-30 14:45:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 1,
            "InstituicaoId": 3,
            "Usuario_Id": 26,
            "Inicio": "2021-09-01 08:00:00.0000000",
            "Fim": "2025-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 2,
            "InstituicaoId": 3,
            "Usuario_Id": 27,
            "Inicio": "2023-02-15 10:15:00.0000000",
            "Fim": "2027-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 3,
            "InstituicaoId": 3,
            "Usuario_Id": 28,
            "Inicio": "2024-05-01 09:30:00.0000000",
            "Fim": "2028-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 4,
            "InstituicaoId": 3,
            "Usuario_Id": 29,
            "Inicio": "2025-01-01 08:00:00.0000000",
            "Fim": "2029-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 5,
            "InstituicaoId": 3,
            "Usuario_Id": 30,
            "Inicio": "2026-03-15 10:15:00.0000000",
            "Fim": "2030-11-30 14:45:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 1,
            "InstituicaoId": 4,
            "Usuario_Id": 31,
            "Inicio": "2022-09-01 08:00:00.0000000",
            "Fim": "2026-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 2,
            "InstituicaoId": 4,
            "Usuario_Id": 32,
            "Inicio": "2024-02-15 10:15:00.0000000",
            "Fim": "2028-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 3,
            "InstituicaoId": 4,
            "Usuario_Id": 33,
            "Inicio": "2025-05-01 09:30:00.0000000",
            "Fim": "2029-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 4,
            "InstituicaoId": 4,
            "Usuario_Id": 34,
            "Inicio": "2026-01-01 08:00:00.0000000",
            "Fim": "2030-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 5,
            "InstituicaoId": 4,
            "Usuario_Id": 35,
            "Inicio": "2027-03-15 10:15:00.0000000",
            "Fim": "2031-11-30 14:45:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 1,
            "InstituicaoId": 4,
            "Usuario_Id": 36,
            "Inicio": "2023-09-01 08:00:00.0000000",
            "Fim": "2027-06-30 17:30:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 2,
            "InstituicaoId": 4,
            "Usuario_Id": 37,
            "Inicio": "2025-02-15 10:15:00.0000000",
            "Fim": "2029-10-31 14:45:00.0000000"
        },
        {
            "Situacao": "Trancada",
            "Curso_Id": 3,
            "InstituicaoId": 4,
            "Usuario_Id": 38,
            "Inicio": "2026-05-01 09:30:00.0000000",
            "Fim": "2030-04-30 16:00:00.0000000"
        },
        {
            "Situacao": "Concluída",
            "Curso_Id": 4,
            "InstituicaoId": 4,
            "Usuario_Id": 39,
            "Inicio": "2027-01-01 08:00:00.0000000",
            "Fim": "2031-12-31 17:30:00.0000000"
        },
        {
            "Situacao": "Ativa",
            "Curso_Id": 5,
            "InstituicaoId": 4,
            "Usuario_Id": 40,
            "Inicio": "2028-03-15 10:15:00.0000000",
            "Fim": "2032-11-01 11:15:00.0000000"
        }
    ]'

INSERT INTO Graduacaos
    (Situacao, Curso_Id, Inicio, Fim, Usuario_Id, InstituicaoId)
SELECT
    Situacao, Curso_Id, Inicio, Fim, Usuario_Id, InstituicaoId
FROM OPENJSON(@jsonVariableGraduacao, N'$')
    WITH (
        Situacao nvarchar(50) N'$.Situacao',
        Curso_Id INT N'$.Curso_Id',
        Inicio DATETIME2(7) N'$.Inicio',
        Fim DATETIME2(7) N'$.Fim',
        Usuario_Id INT N'$.Usuario_Id',
        InstituicaoId INT N'$.InstituicaoId'
    )

-- População da tabela 'Situação'
Declare @jsonVariableSituacao varchar(max)

SET @jsonVariableSituacao = N'[
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 1
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 2
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 3
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 4
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 5
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 6
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 7
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 8
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 9
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 1,
            "Ativo": false,
            "Graduacao_Id": 10
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 11
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 12
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 13
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 14
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 15
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 16
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 17
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 18
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 19
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 2,
            "Ativo": false,
            "Graduacao_Id": 20
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 21
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 22
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 23
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 24
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 25
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 26
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 27
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 28
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 29
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 3,
            "Ativo": false,
            "Graduacao_Id": 30
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 31
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 32
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 33
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 34
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 35
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 36
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 37
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 38
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 39
        },
        {
            "Descricao": "Aprovado",
            "InstituicaoId": 4,
            "Ativo": false,
            "Graduacao_Id": 40
        }
    ]'

INSERT INTO Solicitacao
    (Descricao, Ativo, Instituicao_Id, Graduacao_Id)
SELECT
    Descricao, Ativo, Instituicao_Id, Graduacao_Id
FROM OPENJSON(@jsonVariableSituacao, N'$')
    WITH (
        Descricao nvarchar(50) N'$.Descricao',
        Instituicao_Id INT N'$.InstituicaoId',
        Ativo BIT N'$.Ativo',
        Graduacao_Id INT N'$.Graduacao_Id'
    )