# Ztracená Tlapka - Backend

REST API for a lost animals tracking system built with ASP.NET Core (.NET 10) and PostgreSQL.

## Requirements

- .NET 10 SDK
- Docker

## Setup

```bash
cp .env.example .env
# fill in your values in .env
```

## Running

**Start the database:**
```bash
docker compose up -d
```

**Apply migrations:**
```bash
dotnet ef database update
```

**Start the API:**
```bash
dotnet watch run --launch-profile http
```

API is available at `http://localhost:5180`.  
OpenAPI schema at `http://localhost:5180/openapi/v1.json`.

## Migrations

```bash
# Create a new migration after changing entities
dotnet ef migrations add <MigrationName>

# Apply pending migrations
dotnet ef database update

# Undo the last migration
dotnet ef migrations remove
```
