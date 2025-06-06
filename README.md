# 👤 Client Manager

**Client Manager** is a full-stack web application for managing client records, custom fields, and generating PDF reports. It includes a RESTful API and a lightweight static frontend.

---

## 🔧 Technologies

- ASP.NET Core 9 (Web API)
- C# 12
- Entity Framework Core with SQLite
- AutoMapper
- Docker & Docker Compose
- Swagger / OpenAPI
- xUnit + Moq (unit testing)
- HTML / CSS / JavaScript (static frontend)

---

## 🧠 Architecture

The solution is organized into multiple layers, following Clean Architecture principles:

| Project                    | Responsibility                                                  |
|---------------------------|------------------------------------------------------------------|
| `ClientManager.API`       | Entry point – Web API, configuration, middleware, Swagger        |
| `ClientManager.Core`      | Business logic – DTOs, domain models, interfaces                 |
| `ClientManager.Infrastructure` | EF Core implementation, services, repositories, database setup |
| `ClientManager.Client`    | Static frontend with JS & HTML                                   |
| `ClientManager.Tests`     | Unit tests using xUnit and Moq                                   |

### Patterns & Principles:

- **Repository Pattern**
- **Dependency Injection** (built-in)
- **Single Responsibility Principle**
- **Separation of Concerns**
- **DTO Mapping with AutoMapper**

---

## 📍 API Endpoints

| Method | Endpoint                    | Description               |
|--------|-----------------------------|---------------------------|
| GET    | `/api/Clients`              | Retrieve all clients      |
| GET    | `/api/Clients/{id}`         | Get client by ID          |
| POST   | `/api/Clients`              | Create a new client       |
| PUT    | `/api/Clients/{id}`         | Update client             |
| DELETE | `/api/Clients/{id}`         | Delete client             |
| GET    | `/api/Report/download`      | Generate and download PDF |

---

## 🖥️ Running Locally

> .NET 9 SDK is required

1. **Restore NuGet packages**  
   ```bash
   dotnet restore
   ```

2. **Generate migrations and update database**  
   ```bash
   dotnet ef migrations add InitialCreate --project ClientManager.Infrastructure --startup-project ClientManager.API
   dotnet ef database update --project ClientManager.Infrastructure --startup-project ClientManager.API
   ```

3. **Run the API and frontend**  
   - `ClientManager.API` runs at: http://localhost:5001  
   - `ClientManager.Client` serves static files at: http://localhost:5003  

---

## 🐳 Running with Docker

1. **Generate migrations and update database**  
   ```bash
   dotnet ef migrations add InitialCreate --project ClientManager.Infrastructure --startup-project ClientManager.API
   dotnet ef database update --project ClientManager.Infrastructure --startup-project ClientManager.API
   ```
2. **Build and run the containers**  
   ```bash
   docker-compose up --build
   ```

3. **Available at:**
   - Frontend → http://localhost:5003  
   - API & Swagger → http://localhost:5001/swagger  

> Uses `appsettings.Docker.json` with mapped SQLite volume

---

## 🧪 Unit Tests

Unit tests are located in the `ClientManager.Tests` project.

| Tested Component    | Frameworks Used     |
|---------------------|---------------------|
| `ClientServiceTests`| xUnit, Moq          |

To run tests:
```bash
dotnet test
```

---

## 📦 NuGet Packages

- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.EntityFrameworkCore.Tools`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `Swashbuckle.AspNetCore`
- `Moq`
- `xunit`
- `Microsoft.NET.Test.Sdk`

---
```

---

