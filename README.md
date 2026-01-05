Public Library Management System

A complete **ASP.NET Core-based system** for managing a public library, built with modern backend and frontend technologies.

The solution demonstrates:
- Clean API design
- Authentication & Authorization
- Real-time communication using SignalR
- Separation of concerns across multiple projects

---

 Solution Structure
PublicLibrary
│
├── PublicLibrary.Api
│ ├── Controllers
│ ├── Authentication (JWT + Refresh Token)
│ ├── Role-Based Authorization
│ ├── SignalR Hubs
│ ├── Exception Handling
│ └── API Versioning
│
├── PublicLibrary.Client (Blazor)
│ ├── SignalR Integration
│ ├── Real-time Notifications
│ └── WebSocket Communication
│
└── PublicLibrary.Testing (Class Library)
├── API Consumption
└── Integration & Utility Services


---

## 🚀 Technologies Used

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT Authentication + Refresh Tokens
- Role-Based Authorization
- SignalR (WebSocket)
- API Versioning
- Serilog (Logging)
- SQL Server

### Frontend
- Blazor WebAssembly
- SignalR Client
- WebSocket Communication

### Other
- Swagger / OpenAPI
- Dependency Injection
- Clean Code Principles

---

## 🔐 Authentication & Authorization

- **JWT-based Authentication**
- **Refresh Token Rotation**
- **ASP.NET Core Identity**
- **Role-Based Authorization**

Supported roles:
- `Admin`
- `User`
- `Publisher`
- `Author`
******************************************
Real-Time Features (SignalR)

Real-time notifications

WebSocket-based communication

SignalR Hub integration between API and Blazor app

***********************************************************

API Versioning

The API supports multiple versioning strategies:

URL segment (/api/v1)

Header (x-api-version)

Query string (?api-version=1.0)
*****************************************************************
Testing & Class Library

Dedicated Class Library to test and consume API endpoints

Reusable services for integration testing

Separation of API logic and client consumption
