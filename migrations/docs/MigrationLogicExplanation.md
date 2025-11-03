# EF Core Migration Logic (Database-First Workflow)

This document explains the complete workflow of how Entity Framework Core (EF Core) is used in this project when working **with an existing database**.
It describes how to scaffold models, create migrations, and update the database schema using EF Core commands.

---

##  Overview of the Diagram

The following steps correspond to the diagram `MigrationLogicDiagram.png` and show the logical flow from an **existing database** to an **updated schema** managed by EF Core migrations.

---

### 1️ Existing Database

The process starts with a database that already exists (for example, a MySQL or SQL Server database).
This database contains tables, relationships, and constraints that we want EF Core to represent as C# classes.

---

### 2️ Command: `dotnet ef dbcontext scaffold`

This command **reverse-engineers** the existing database into EF Core models and a DbContext class.
It automatically generates:

* Entity classes that represent each table.
* A `DbContext` class that contains `DbSet<TEntity>` properties and connection configuration.

**Example:**

```bash
dotnet ef dbcontext scaffold "Your_Connection_String" Pomelo.EntityFrameworkCore.MySql -o Models
```

**Result:**
The project now contains the generated C# files (models and context).

---

### 3️ Generated Models + DbContext

After scaffolding, EF Core creates:

* C# entity classes (representing tables).
* A `DbContext` file that acts as a bridge between your application and the database.

At this stage, your project is fully connected to the database schema, but **no migrations exist yet**.

---

### 4️ Command: `dotnet ef migrations add InitialCreate`

This command creates the **first migration** that captures the current state of your models.
It tells EF Core:

> “Generate migration files that describe how to build the database schema based on these models.”

Even if the database already exists, this step helps EF Core start tracking schema changes for future updates.

---

### 5️ Migration Files (`.cs`)

EF Core generates one or more C# files under a `Migrations/` folder.
Each migration file includes two important methods:

* `Up()` → Defines what changes to apply (e.g., create tables, add columns).
* `Down()` → Defines how to revert those changes.

This allows versioning and rollback of database schema changes.

---

### 6️ Command: `dotnet ef database update`

This command applies all **pending migrations** to the database.
EF Core executes the SQL commands described in the migration files.

**Result:**
The database schema is synchronized with your C# models.

---

### 7️ Database Schema Updated 

At this final stage:

* The database structure is fully aligned with the EF Core model.
* Any future schema changes can be handled by adding new migrations (`dotnet ef migrations add <Name>`) and updating again.

---


###  Notes for Contributors

* Always ensure your connection string is correct before scaffolding.
* Never edit migration files manually unless absolutely necessary.
* When changing models, create a **new migration** instead of editing the old ones.
* Test migrations on a development database before applying to production.

---

*Author: [mohamedReda404](https://github.com/mohamedReda404)*
*Project: [IMDb-Project](https://github.com/Kea-1Semester/IMDb-Project)*
