<h1 align="center">Backend ASP.NET Core</h1>

<h3 align="center">Descri��o</h3>

Api de exemplo disponibilizando a cria��o de contas de diferentes tipos, incluindo 'usu�rio', 'institui��o' e 'administrador'. 

A conta do tipo 'usu�rio' permite que o usu�rio crie seu perfil, salve suas informa��es de experi�ncias profissionais e acad�micas, e tenha acesso �s funcionalidades do sistema e para a intera��o com outras contas. 

A conta do tipo 'institui��o' � destinada �s institui��es de ensino superior que atuam no Brasil. Essa conta permite que as institui��es criem seus perfis, incluindo cursos disponibilizados e validem as solicita��es de gradua��es cadastradas por contas do tipo 'usu�rio'. Isso permite que as institui��es gerenciem suas informa��es e tenham controle sobre as solicita��es de gradua��es feitas por seus alunos.

A conta do tipo 'administrador' � a mais alta autoridade no sistema. Ela tem acesso a todas as funcionalidades de gerenciamento de contas, permitindo que os administradores gerenciem as contas de todos os tipos, incluindo a cria��o, edi��o e remo��o de contas.


<h3 align="center">Ferramentas</h3>

### Docker 
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=pks291#%!" -p 1433:1433 -d --name sqlserver mcr.microsoft.com/mssql/server:2022-latest
```

### Azure Data Studio
Necess�rio instalar `SQL Server Dacpac`, para fazer a importa��o do backup do banco. Nome do usu�rio `sa` senha `pks291#%!`