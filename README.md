# CRUD Operations with Rest Api
Multi Layer Rest Api CRUD Operations with Swagger User Interface,

Asp.net Core Rest Api project that Creates, Updates, Views, Deletes, Searchs and Exports Product objects

## Prerequisites

* Visual Studio 2019

* ASP.NET Core 3.1

* SQL Server(Microsoft Sql Server or MySql Server)

* Postman

## Dependencies and Used Libraries

* ClosedXml (v- 0.95.4)

* FluentValidation (v- 9.3.0)

* Swashbuckle.AspNetCore for UserInterface (5.0.0-rc3 recommended)

* AutoMapper (v 9.0.0)

* MicrosoftEntityFrameworkCore.Design (v- 3.1.10)

* MicrosoftEntityFrameworkCore.SqlServer (v- 3.1.10)

* MicrosoftEntityFrameworkCore.Tools (v- 3.1.10)

## Create Database

Create new database (MsSql server used in this project and Inventory database created)

Add new table as Product ![alt text](https://i.ibb.co/257Bqbq/Product-Table.png)

Set Id as a primary key and auto increment

## How to run the project

* Checkout this project to a location in your disk( Clone via link or download)

* Open the solution file using the Visual Studio 2019.

* Restore the NuGet packages by rebuilding the solution.

* Change server name in appsettings.json file: UGUR-PC to your localDb servername 

![alt text](https://i.ibb.co/TPrQh0h/Connstring.png)


## Run with Swagger UI

Open project folder location and run PowerShell application

command this code : dotnet run -p CRUDOperations/CRUDOperations.Api.csproj

PowerShell screen must be like this: 

![alt text](https://i.ibb.co/PQfMywJ/Power-Shell.png)

Enter to browser : https://localhost:5001/index.html for Swagger UI, you will see all http actions with their description

![alt text](https://i.ibb.co/Jm0QLZ8/Swagger-IO.png)

## Used Design Patterns

* Repository Pattern

  To ensure the reusability of the codes we have created for our CRUD (Create Read Update Delete) operations such as adding data to the database, updating and reading.
* Unit of Work Pattern

  The Unit Of Work design pattern is a design pattern that prevents every action related to the database from being reflected on the database instantaneously in our software application and accordingly allows all actions to be accumulated and performed over a single connection at a time, thus minimizing database costs.
  
## Layers
  1. Api
  2. Data
  3. Core
  4. Service
## Issues
Photo property keeps only string data. Photo property will be change for saving image media.
  
## Will be Added
* Photo property
* Authentication

