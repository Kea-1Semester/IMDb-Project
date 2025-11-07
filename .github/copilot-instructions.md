## Quick orientation for AI coding agents

This repository contains a Vite + React TypeScript frontend and a .NET EF Core "SeedData" project that populates multiple databases. Use this doc to get productive quickly — focus on the files and commands referenced below.

1) Big picture
- frontend/: Vite + React (TS/TSX) UI using Chakra UI. Key files: `frontend/package.json`, `frontend/src/` and `frontend/Dockerfile.dev`.
- SeedData/: Console app + EF Core used to scaffold models and run seed handlers. Key files: `SeedData/SeedData.csproj`, `SeedData/Handlers/*` (AddActor.cs, AddRating.cs, etc.), `SeedData/Migrations/` and `SeedData/data/`.
- docker-compose.dev.yml: local dev stack (frontend, mysql, mongodb, neo4j). Environment variables (for frontend) come from compose / `SeedData/SeedData/.Env`.

2) Developer workflows (what to run)
- Frontend dev (local):
  - Open `frontend/`, install deps, then run Vite dev server:
    ```powershell
    cd frontend
    npm install
    npm run dev
    ```
- Full local dev stack (docker): the repo root `package.json` exposes helper scripts that run bash scripts. On Windows prefer Git Bash/WSL or call docker-compose directly:
  - From Git Bash / WSL:
    ```bash
    npm run dev_up
    # or
    bash ./scripts/dev_up.sh
    ```
  - Or use Docker Compose from PowerShell directly:
    ```powershell
    docker compose -f docker-compose.dev.yml up --build
    ```
- EF Core / SeedData:
  - Requirements: .NET SDK and `dotnet-ef` tool. See `SeedData/README.md` for scaffolding/migration commands.
  - Typical pattern when changing models: update `EfCoreModelsLib`/models, then run `dotnet ef migrations add <Name>` in the SeedData project and `dotnet ef database update` to apply.

3) Project-specific conventions
- Root npm scripts call Bash scripts (e.g., `build` and `dev_up`) — do not assume PowerShell-only usage.
- Seed handlers: seeding is structured in `SeedData/Handlers/` (one handler per import type). When adding new data imports, follow the file-per-responsibility pattern (e.g., `AddPersonToDb.cs`, `AddRating.cs`).
- Migrations are checked in under `SeedData/Migrations/` and use timestamped filenames (keep naming consistent).

4) Integration points & environment
- Datastores used in development: MySQL (ports: 3307 -> 3306), MongoDB (27018 -> 27017), Neo4j (7474/7687). See `docker-compose.dev.yml` for images, ports, and environment variables.
- Frontend expects an API base URL via `VITE_API_URL` (set in `docker-compose.dev.yml` when running containers).

5) Where to look when modifying behaviour
- UI: `frontend/src/components/ui/` (color-mode, provider, tooltip, toaster) shows component patterns and theming.
- Backend/seed logic: `SeedData/Handlers/*` and `SeedData/DbConnection` (MySqlSettings.cs / MongoDbSettings.cs).
- SQL/schema artifacts: `imdb-data/` contains SQL and data TSVs; `SeedData/data/` contains seed inputs.

6) Small contract for typical AI edits
- Inputs: file(s) to change, target environment (local frontend / docker compose / seed DB), and migration intention (if changing models).
- Outputs: code edits + test instructions (how to run frontend/dev stack or run `dotnet ef`), and a short checklist of manual steps (e.g., "run migrations", "rebuild Docker image").

7) Quick tips for PRs and automation
- Keep EF migrations small and run `dotnet ef migrations add` locally to generate diffs; include them in PRs.
- When updating the frontend, prefer non-breaking changes to public components; reference `frontend/package.json` scripts for lint/build.

If anything here is unclear or you want a different focus (for example, more detail about the SeedData handlers or the Docker setup), tell me what to expand and I will iterate.
