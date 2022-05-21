# To-do List Api
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-blue?style=flat&logo=linkedin&labelColor=blue")](https://www.linkedin.com/in/andr%C3%A9-terra-2a7728145/)


![](https://github.com/AndreTerra5348/todo-list-api/actions/workflows/azure-webapps-dotnet-core.yml/badge.svg)

## Introduction
This is a simple multi-layer monolithic api made with Dotnet to manage Users and To-do lists, with test projects

I made this project to learn more about Dotnet, Asp.Net Core, Entity Framework, xUnit for testing, Unit of Work pattern, and multi layer archtecture for apis.

## Description Breakdown:
### Core Layer 
Models and interfaces, base classes used by other layers
- Models for User, Todo
- Repositories Interfaces for generic, User and Todo models
- Services Interfaces for User and Todo models
- Unit of Work Interface for repositories

### Data Layer 
Implementations related with data transactions
- Repositories: generic and model specific implementations
- Unit of Work, single db context for all repositories, and repositories themselves.
- Database Context
- EntityFramework configuration for models relations and properties.
- Migrations

### Services Layer 
Implementations related with business logic
- Services: model specific implementations, uses unit of work to access repositories.

### Api Layer 
Implementations related with api
- Controllers: uses injected services to access business logic.
- Dto for create and read operations
- AutoMapper profile to map Dtos to models and vice versa
- FluentValidation to validate Dtos
- uses MSSQL Server for Development and Production environments

### Api.Test
Unit Test Api projects
- Unit tests for controllers with xUnit, Moq and FluentAssertions.
- Uses a pattern to group Moq setups for a specific service.

## TODO
- [ ] Add unit tests for controllers
    - [x] TodosController
    - [ ] UsersController
- [x] Change Database to Azure SQL
- [x] Add ~~Azure pipelines~~ Github Actions for Build, Test and Deploy (CI/CD)
- [x] Deploy to Azure


## Built with
- .Net
- Multi-layer architecture
- Unit of Work pattern
- Asp.net Core
- Entity Framework
- AutoMapper
- FluentValidation
- MSSQL Server
- xUnit
- Moq

## Getting Started

### Prerequisites

- .Net Core 5.0
- MSSQL Server

### Running the Application

1. Clone this repository
```bash 
git clone https://github.com/AndreTerra5348/todo-list-api
```

2. Set local environment variables containing MSSQL Connection String
```bash
setx SQLCONNSTR_TodoListConnStr "Server=localhost,1433;Initial Catalog=todolistdb;User Id=sa;Password=<password>;"
```

3. Restore packages
```bash
cd todo-list-api
dotnet restore
```

4. Run the application
```bash
cd src\TodoList.Api\
dotnet run
```

## Acknowledgments and Resources
- [Andre Lopes Medium Article](https://medium.com/swlh/building-a-nice-multi-layer-net-core-3-api-c68a9ef16368)
- [Martin Rybak Medium Article](https://medium.com/@martinrybak/a-cleaner-way-to-create-mocks-in-net-6e039c3d1db0)
- [Macoratti Unit Of Work Article](https://www.macoratti.net/16/01/net_uow1.htm)
- [Martin Fowler Unit of Work](https://martinfowler.com/eaaCatalog/unitOfWork.html)
- [Microsoft Docs Unit Test](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0)

## License
Distributed under the MIT License. See LICENSE.txt for more information.

## Author
[André Terra](https://www.linkedin.com/in/andr%C3%A9-terra-2a7728145/)
