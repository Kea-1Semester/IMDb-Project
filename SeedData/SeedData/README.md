# EF Core Quick Start (Database-First with Migrations)

This project uses **Entity Framework Core** to scaffold models from an existing MySQL     database and manage schema changes via migrations.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) OR using Visual Studio 2022+
- MySQL or compatible database
- EF Core CLI tools installed:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

## Setup Steps

1. Create a Console App

    ``` bash
    dotnet new console -n SeedData
    cd SeedData
    ```

2. Add EF Core Packages

    ``` bash
    dotnet add package Microsoft.EntityFrameworkCore.MySQL
    dotnet add package Microsoft.EntityFrameworkCore.Design
    ```

3. Scaffold Models from Existing Database

    ```bash
    dotnet ef dbcontext scaffold "ConnectionString" Pomelo.EntityFrameworkCore.MySql -o Models
    ```

    - Replace `ConnectionString` with your actual database connection string.
    - This command generates entity classes and a DbContext based on your existing database schema.
    - -o Models specifies the output directory for the generated classes.
    - you can specify additional options like `--schema` to limit the scaffolding to specific schemas. Vist [EF Core Docs](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli) for more details.
    -

4. Enable Migrations

    ```bash
    dotnet ef migrations add InitialCreate
    ```

    - This command creates a new migration named `InitialCreate` that captures the current state of the database schema.

5. Apply Future Changes (optional)

    - After making changes to your models, create a new migration:

    ```bash
    dotnet ef migrations add YourMigrationName
    ```

    - Apply the migration to update the database schema:

    ```bash
    dotnet ef database update
    ```

Using this step-by-sep guide, you can effectively manage your database schema using Entity Framework Core in a .NET application. For more advanced scenarios and options, refer to the [EF Core documentation](https://learn.microsoft.com/en-us/ef/core/).