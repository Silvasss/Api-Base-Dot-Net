<h1 align="center">Backend ASP.NET Core</h1>

<h3 align="center">Descrição</h3>

Api de exemplo disponibilizando a criação de contas de diferentes tipos, incluindo 'usuário', 'instituição' e 'administrador'. 

A conta do tipo 'usuário' permite que o usuário crie seu perfil, salve suas informações de experiências profissionais e acadêmicas, e tenha acesso às funcionalidades do sistema e para a interação com outras contas. 

A conta do tipo 'instituição' é destinada às instituições de ensino superior que atuam no Brasil. Essa conta permite que as instituições criem seus perfis, incluindo cursos disponibilizados e validem as solicitações de graduações cadastradas por contas do tipo 'usuário'. Isso permite que as instituições gerenciem suas informações e tenham controle sobre as solicitações de graduações feitas por seus alunos.

A conta do tipo 'administrador' é a mais alta autoridade no sistema. Ela tem acesso a todas as funcionalidades de gerenciamento de contas, permitindo que os administradores gerenciem as contas de todos os tipos, incluindo a criação, edição e remoção de contas.


<h3 align="center">Ferramentas</h3>

### Docker 
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=pks291#%!" -p 1433:1433 -d --name sqlserver mcr.microsoft.com/mssql/server:2022-latest
```

### Azure Data Studio
Necessário instalar `SQL Server Dacpac`, para fazer a importação do backup do banco. Nome do usuário `sa` senha `pks291#%!`