# ExoApi - Gerenciamento de Projetos

Este repositório foi desenvolvido por mim como aluno do SENAI, fazendo parte das atividades práticas da matéria de Desenvolvimento de APIs para o projeto da empresa ExoApi.

## 🚀 Tecnologias Utilizadas
* C# / .NET Core
* Entity Framework Core (Mapeamento do banco)
* SQL Server (Persistência de dados)
* Padrão de Arquitetura MVC (Model-View-Controller)
* **Swagger UI** (Documentação interativa da API)

## 🛠️ Recursos Implementados (CRUD)
A API disponibiliza os seguintes endpoints sob o protocolo HTTP para o gerenciamento completo dos projetos:
* **GET /api/projetos** - Lista todos os projetos salvos no banco.
* **GET /api/projetos/{id}** - Busca um projeto específico pelo seu ID de registro.
* **POST /api/projetos** - Cadastra um novo projeto (retorna status HTTP 201 Created).
* **PUT /api/projetos/{id}** - Atualiza as informações de um projeto existente.
* **DELETE /api/projetos/{id}** - Remove um projeto de forma permanente do sistema.

## 📄 Documentação da API
A API conta com documentação automatizada via **Swagger**. Para visualizar os endpoints de forma interativa e realizar testes de requisições, basta executar a aplicação. A interface gráfica estará disponível na rota raiz do projeto (`/`).
