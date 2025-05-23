# Diseño Arquitectónico de la API

Este proyecto implementa una API RESTful para la gestión de una biblioteca utilizando una arquitectura moderna basada en buenas prácticas de separación de responsabilidades, pruebas, y mantenibilidad.

---

## Patrones de diseño utilizados

### 1. Clean Architecture

**¿Por qué?**

* Separación clara de responsabilidades: controladores, negocio, datos, servicios externos.
* Escenarios como validación, autenticación, pruebas, autorización y consultas complejas requieren capas bien definidas.

**Beneficios:**

* Alta mantenibilidad y testabilidad.
* Independencia de frameworks y bases de datos.
* Cada capa depende de abstracciones, no implementaciones.

**Capas:**

* `Domain`: Entidades y contratos.
* `Application`: Casos de uso, comandos/queries, DTOs, validadores.
* `Infrastructure`: Implementaciones (EF Core, JWT, servicios externos).
* `WebApi`: Controladores, middlewares, configuración.

---

### 2. CQRS (Command Query Responsibility Segregation)

**¿Por qué?**

* Separar comandos (crear, actualizar, eliminar) de queries (lecturas).

**Beneficios:**

* Simplifica la lógica de cada operación.
* Mejora la escalabilidad y el testeo.
* Cada acción tiene su handler (ideal para pruebas).

Uso de **MediatR** para orquestar los handlers.

---

### 3. Repository Pattern + Unit of Work

**¿Por qué?**

* Abstracción de acceso a datos.
* Facilidad para pruebas unitarias (uso de mocks).

**Beneficios:**

* Separa la infraestructura de la lógica de aplicación.
* Promueve el principio de Inversión de Dependencias.
* Reduce código repetido de EF Core en los servicios.

---

## Autenticación y Autorización

* JWT con `Microsoft.AspNetCore.Authentication.JwtBearer`
* Roles: `Admin` y `Usuario`
* Protección de endpoints con `[Authorize(Roles = "...")]`

---

## Testing

* Pruebas unitarias con **xUnit** y **Moq**.
* Testeamos:

  * Handlers de CQRS (`Application`)
  * Validadores de comandos (FluentValidation)
 

---

## Tecnologías y Librerías sugeridas

| Necesidad                 | Tecnología/Librería                                 |
| ------------------------- | --------------------------------------------------- |
| ORM                       | Entity Framework Core                               |
| Validaciones              | FluentValidation                                    |
| Autenticación             | JWT (Microsoft.AspNetCore.Authentication.JwtBearer) |
| Inyección de dependencias | .NET built-in DI                                    |
| CQRS                      | MediatR                                             |
| Mapeo DTOs                | AutoMapper                                          |
| Testing                   | xUnit + Moq                                         |
| Documentación API         | Swagger (Swashbuckle.AspNetCore)                    |

---

## Estructura de carpetas y proyectos

```
/LibraryAPI.sln
│
├── src/
│   ├── LibraryAPI.Api/                --> Proyecto principal Web API (.NET)
│   ├── LibraryAPI.Application/        --> Lógica de negocio (CQRS, DTOs, Handlers)
│   ├── LibraryAPI.Domain/             --> Entidades, interfaces de repositorio
│   ├── LibraryAPI.Infrastructure/     --> EF Core, JWT, implementaciones de repos
│   └── LibraryAPI.Persistence/        --> Configuración de EF, DbContext
│
└── tests/
    └── LibraryAPI.Tests/             --> Pruebas unitarias con xUnit
```

---

Este diseño facilita el crecimiento de la aplicación, la separación de responsabilidades y el mantenimiento a largo plazo.


# Libreria API (.NET 7, JWT, Clean Architecture)

API RESTful para la gestión de libros, autores y préstamos de biblioteca. Implementa:

* ASP.NET Core 7
* Clean Architecture + CQRS + MediatR
* Entity Framework Core 7
* JWT con autorización por roles (Admin, Usuario)
* FluentValidation
* xUnit + Moq para pruebas unitarias

---

## Características principales

* CRUD de libros y autores
* Registro y devolución de préstamos
* Seguridad con JWT (login, registro de usuarios con rol)
* Swagger con autenticación integrada

---

## Requisitos

* .NET 7 SDK
* SQL Server
* Visual Studio 2022 o VS Code

---

## Configuración

### 1. Crear base de datos `LibreriaDb`

Ejecuta este script en SQL Server: `scriptBibliotecaDb` que se encuentra en el proyecto

### 2. Cadena de conexión en `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=LibreriaDb;Trusted_Connection=True;"
},
"JwtSettings": {
  "Key": "clave-super-larga-y-secreta"
}
```

### 4. Restaurar paquetes y ejecutar

```bash
dotnet build
dotnet run --project LibreriaApi
```

Abre Swagger:

```
https://localhost:{puerto}/swagger
```

---

## Seguridad y autenticación

### 1. Login

`POST /api/auth/login`

```json
{
  "username": "admin",
  "password": "admin"
}
```

Copia el token y autoriza en Swagger: `Bearer {token}`

### 2. Registro de usuario

`POST /api/auth/registrar`

```json
{
  "username": "nuevo",
  "password": "clave123",
  "rol": "Usuario"
}
```

### 3. Endpoints protegidos

| Endpoint                 | Rol requerido   |
| ------------------------ | --------------- |
| `POST /libros`           | Admin           |
| `PUT /prestamos/{id}`    | Usuario o Admin |
| `DELETE /prestamos/{id}` | Admin           |

---

## Pruebas

```bash
dotnet test
```

Usa xUnit + Moq + AutoMapper. Pruebas para comandos, validadores y queries.

---






