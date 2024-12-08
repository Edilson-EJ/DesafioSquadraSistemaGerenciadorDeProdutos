# Sistema Gerenciador de Produtos

## Descrição do Projeto

Este projeto é uma aplicação web desenvolvida com .NET 8 no backend e Angular 17 no frontend. O sistema implementa um CRUD (Create, Read, Update, Delete) com autenticação JWT, gerenciamento de permissões e integração com banco de dados SQL Server.

O objetivo é fornecer uma interface para gerenciar produtos, usuários e permissões de acesso, diferenciando as ações disponíveis para gerentes e funcionários.

### Funcionalidades

#### Produtos:

- Qualquer usuário pode consultar todos os produtos cadastrados.
- Qualquer usuário pode consultar os produtos em estoque.
- Gerentes podem excluir produtos.
- Gerentes e funcionários podem atualizar o estoque de um produto.

#### Usuários:

- Gerentes podem adicionar e excluir usuários.
- Autenticação JWT para acesso às funcionalidades do sistema.

### Gerenciamento de Permissões:

- Gerente: Permissões completas para gerenciar usuários e produtos.
- Funcionário: Permissão para atualizar o estoque de produtos.
- Usuários não autenticados: Apenas consulta de produtos.

### Tecnologias Utilizadas

### Backend (.NET 8)

#### Bibliotecas Usadas:

- BCrypt.Net-Next (v4.0.3): Para hashing de senhas.
- Microsoft.AspNetCore.Authentication.JwtBearer (v8.0.11): Para autenticação JWT.
- Microsoft.EntityFrameworkCore (v9.0.0): Para ORM e manipulação do banco de dados.
- Microsoft.EntityFrameworkCore.Design (v9.0.0): Ferramentas de design do EF Core.
- Microsoft.EntityFrameworkCore.SqlServer (v9.0.0): Provedor do SQL Server para EF Core.
- Microsoft.EntityFrameworkCore.Tools (v9.0.0): Ferramentas CLI para EF Core.
- Microsoft.IdentityModel.Tokens (v8.2.1): Para validação e criação de tokens JWT.
- Swashbuckle.AspNetCore (v7.1.0): Para geração de documentação Swagger.
- System.IdentityModel.Tokens.Jwt (v8.2.1): Para manipulação de tokens JWT.

#### Como Instalar as Dependências:

#### Execute os seguintes comandos no terminal para adicionar as bibliotecas ao projeto:

- dotnet add package BCrypt.Net-Next --version 4.0.3
- dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.11
- dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
- dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
- dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
- dotnet add package Microsoft.IdentityModel.Tokens --version 8.2.1
- dotnet add package Swashbuckle.AspNetCore --version 7.1.0
- dotnet add package System.IdentityModel.Tokens.Jwt --version 8.2.1

### Frontend (Angular 17)

- Componentização para organização do código.
- Integração com a API via HttpClient.
- Autenticação com tokens JWT.
- SCSS para estilização.

### Banco de Dados

- SQL Server local.

- Duas tabelas principais:
    - Produto:
        - Nome
        - Descricao
        - Status(Em estoque/Fora de Estoque)
        - Preco
        - Quantidade em estoque

    - Usuário:
        - Nome
        - Email
        - Senha (armazenada com hash)
        - Funcao (Gerente / Funcionário)

### Estrutura do Projeto

#### Backend

- Controllers: Lógica de controle para rotas de produto e usuário.
- Services: Lógica de negócio e manipulação de dados.
- DTOs (Data Transfer Objects): Para entrada e saída de dados.
- Autenticação: Middleware para proteger rotas e verificar permissões.
- Swagger: Documentação acessível em /swagger.

### Frontend

- Componentes Principais:
    - Cadastro de Produto: Permite adicionar novos produtos.
    - Monitoramento Geral: Exibe produtos e usuários (somente para gerentes).

### Rotas:

 - /login: Tela de autenticação.
 - /cadastro-de-produto: Tela para cadastro de produtos.
 - /monitoramento-geral: Tela principal com funcionalidades de gerenciamento.

## Requisitos para Rodar o Projeto

### Backend

#### Pré-requisitos:

- .NET SDK 8.0 ou superior instalado.
- SQL Server configurado localmente.

#### Configuração:

-   Adicione a string de conexão ao banco de dados no appsettings.json.

        {
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Database=SistemaGerenciador;User Id=seu_usuario;Password=sua_senha;"
        }
        }

### Comandos:

- Atualize o banco de dados:

        dotnet ef database update

- Rode o servidor:
        dotnet run

### Frontend

### Pré-requisitos:
- Node.js e npm instalados.
- Angular CLI instalada globalmente.

#### Configuração:

- Atualize o arquivo environment.ts com a URL do backend.

#### Comandos:

- Instale as dependências:

        npm install

- Rode o servidor de desenvolvimento:

        ng serve

Observações

- Testes Unitários: Não foram implementados para este projeto.
- Autenticação: O projeto utiliza tokens JWT para autenticação e autorização.


## Autor

Edilson, com foco em .NET, Angular, e desenvolvimento de aplicações web. Estuda desenvolvimento desde 2022, com participação em projetos como AccessIF e CashPrev no programa IART. Este projeto foi desenvolvido como parte de um desafio de projeto da Squadra Digital.
