# To-do List Api
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-blue?style=flat&logo=linkedin&labelColor=blue")](https://www.linkedin.com/in/andr%C3%A9-terra-2a7728145/)


[![Build Test and Deploy](https://github.com/AndreTerra5348/todo-list-api/actions/workflows/azure-webapps-dotnet-core.yml/badge.svg)](https://github.com/AndreTerra5348/todo-list-api/actions/workflows/azure-webapps-dotnet-core.yml)

## Introduction
This is a simple multi-layer monolithic api made with Dotnet and Azure Services to manage Users and To-do lists.

I made this project to learn more about Dotnet, Asp.Net Core, Entity Framework, Unit of Work pattern, multi layer archtecture, xUnit, Github Actions for build, test and deploy, Azure App Service, SQL Server and Managed Identities

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
- [x] Add unit tests
- [x] Change Database to Azure SQL Server
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
- Azure Services (App Service, SQL Server, Managed Identities)

## Getting Started

### Prerequisites

- .Net Core 5.0
- Azure Account

### Running the Application

1. Clone this repository
```bash 
git clone https://github.com/AndreTerra5348/todo-list-api
```

2. Configure Azure App Service to connect to the SQL Server [Following this steps](https://docs.microsoft.com/en-us/azure/app-service/tutorial-connect-msi-sql-database?tabs=vscode%2Cefcore%2Cdotnetcore)

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
- [Azure App Service and SQL Database](https://docs.microsoft.com/en-us/azure/app-service/tutorial-dotnetcore-sqldb-app?tabs=azure-portal%2Cvisual-studio-code-deploy%2Cdeploy-instructions-azure-portal%2Cvisual-studio-code-logs%2Cazure-portal-resources)
- [Azure Managed Identity](https://docs.microsoft.com/en-us/azure/app-service/tutorial-connect-msi-sql-database?tabs=vscode%2Cefcore%2Cdotnetcore)

## License
Distributed under the MIT License. See LICENSE.txt for more information.

## Author
[André Terra](https://www.linkedin.com/in/andr%C3%A9-terra-2a7728145/)
