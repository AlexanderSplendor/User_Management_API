# User Management API

A simple user management API built with ASP.NET Core Web API, Entity Framework Core, and SQLite. The API supports user creation, authentication (login), profile management, and CRUD operations on user data.

## How to Run the API Locally

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/User_Management_API.git
   cd User_Management_API

Open the solution in Visual Studio or your preferred IDE.

Restore the NuGet packages:
dotnet restore

Run the API:
dotnet run
The API should be running at http://localhost:5000 (or another URL if configured differently{https://localhost:7237/swagger/index.html} ).

Use Swagger to test the API or Postman to interact with the endpoints.

**Key Design Decisions**
Authentication: A dummy token is used for simplicity, but JWT could be implemented for production-level security.
Database: SQLite was used for simplicity, but in a production environment, SQL Server or PostgreSQL would be recommended for better scalability.
Model Validation: Basic validation is applied for user creation, including password strength and unique username/email checks.

**Assumptions Made**
The API is a prototype and does not include full production-level security or user authentication.
The database is kept in-memory during development, but SQLite is used for persistence.
The password strength is checked only by basic length and special character rules.

**Sample API Requests and Responses
POST /api/users (Create User)**
_Request:_
{
  "username": "JohnDoe",
  "email": "john@example.com",
  "password": "StrongP@ss1"
}
_Response:_
{
  "id": "12345",
  "username": "JohnDoe",
  "email": "john@example.com",
  "createdAt": "2025-04-16T12:00:00Z"
}

**POST /api/auth/login (Login)**
_Request:_
{
  "username": "Admin",
  "password": "AdminP@ss1"
}
_Response:_
{
  "id": "257678F7-DA0D-4C50-8956-E8BB80E3B067",
  "username": "admin",
  "email": "admin@gmail.com",
  "token": "abc123"
}

**GET /api/users/{id} (Get User Profile)
Request: GET http://localhost:5000/api/users/12345**

_Response:_
{
  "id": "257678F7-DA0D-4C50-8956-E8BB80E3B067",
  "username": "admin",
  "email": "admin@gmail.com",
  "isAdmin": true,
  "createdAt": "2025-04-16T12:00:00Z"
}

**Update User Info – PUT /api/users/{id}**
Example URL:
PUT /api/users/be43e5cf-1e41-47aa-b460-63cf68d8727a
Request:
{
  "username": "JohnDoeUpdated",
  "email": "john.updated@example.com"
}
Response (200 OK):
{
  "id": "be43e5cf-1e41-47aa-b460-63cf68d8727a",
  "username": "JohnDoeUpdated",
  "email": "john.updated@example.com",
  "isAdmin": false,
  "createdAt": "2025-04-17T12:00:00Z"
}

**Delete User – DELETE /api/users/{id}**
Example URL:
DELETE /api/users/be43e5cf-1e41-47aa-b460-63cf68d8727a
Response (204 No Content):

(no response body)
