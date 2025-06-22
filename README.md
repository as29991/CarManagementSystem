# Car Management System

A comprehensive Car Management System built with ASP.NET Core Web API, featuring vehicle management, transaction processing, and secure user authentication with JWT tokens.

## 🚀 Live Demo

- **API Base URL**: [https://carmanagement-ademmevaip.azurewebsites.net](https://carmanagement-ademmevaip.azurewebsites.net)
- **API Documentation (Swagger)**: [https://carmanagement-ademmevaip.azurewebsites.net/swagger](https://carmanagement-ademmevaip.azurewebsites.net/swagger)

## 👥 Team Members

- **Adem Shaini**
- **Mevaip Zeqiri**

## 📋 Project Overview

This Car Management System implements a service-oriented architecture (SOA) approach to provide efficient vehicle fleet management, streamlined transaction processing, and secure user access control. The system is designed for vehicle rental companies, car dealerships, fleet management departments, and transportation service providers.

## 🛠️ Technologies Used

- **Backend Framework**: ASP.NET Core 8.0 Web API
- **Database**: PostgreSQL (Azure Database for PostgreSQL)
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity with JWT Tokens
- **Object Mapping**: AutoMapper
- **Testing**: xUnit, Moq
- **Cloud Platform**: Microsoft Azure
- **Version Control**: Git & GitHub
- **API Documentation**: Swagger/OpenAPI

## 🏗️ Project Structure

```
CarManagementSystem/
├── CarManagementSystem/                 # Main API Project
│   ├── Controllers/                     # API Controllers
│   │   ├── AuthController.cs           # Authentication endpoints
│   │   ├── VehicleController.cs        # Vehicle CRUD operations
│   │   └── TransactionController.cs    # Transaction management
│   ├── Models/                         # Data Models
│   │   ├── DTOs/                       # Data Transfer Objects
│   │   ├── Domain/                     # Domain entities
│   │   └── Auth/                       # Authentication models
│   ├── Services/                       # Business Logic Layer
│   │   ├── Interfaces/                 # Service contracts
│   │   └── Implementations/            # Service implementations
│   ├── Repositories/                   # Data Access Layer
│   │   ├── Interfaces/                 # Repository contracts
│   │   └── Implementations/            # Repository implementations
│   ├── Data/                          # Database Context
│   ├── Migrations/                    # EF Core Migrations
│   └── Program.cs                     # Application entry point
├── CarManagementSystemTests/          # Unit Tests
│   ├── Controllers/                   # Controller tests
│   ├── Services/                      # Service tests
│   └── Repositories/                  # Repository tests
├── azure-pipelines.yml               # Azure DevOps CI/CD Pipeline
└── README.md                         # Project documentation
```

## ⚙️ Setup Instructions

### Prerequisites

- .NET 8.0 SDK
- PostgreSQL (local development)
- Visual Studio 2022 or VS Code
- Git

### Local Development Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/as29991/CarManagementSystem.git
   cd CarManagementSystem
   ```

2. **Configure Database Connection**
   
   Update `appsettings.json` with your local PostgreSQL connection:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=CarManagementDB;Username=your_username;Password=your_password"
     }
   }
   ```

3. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Run Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run --project CarManagementSystem
   ```

6. **Access the API**
   - API: `https://localhost:7000` or `http://localhost:5000`
   - Swagger UI: `https://localhost:7000/swagger`

## 🧪 Running Tests

Execute all unit tests:
```bash
dotnet test
```

Run tests with coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 🔑 Authentication

The system uses JWT-based authentication with role-based authorization.

### Available Roles
- **Admin**: Full system access
- **Manager**: Vehicle and transaction management
- **User**: Basic vehicle viewing and transaction creation

### Authentication Endpoints
- `POST /api/Auth/register` - User registration
- `POST /api/Auth/login` - User login
- `POST /api/Auth/role` - Create roles (Admin only)
- `POST /api/Auth/assign` - Assign roles (Admin only)

## 📊 API Endpoints

### Vehicle Management
- `GET /api/Vehicle` - Get all vehicles
- `GET /api/Vehicle/{id}` - Get vehicle by ID
- `POST /api/Vehicle` - Create new vehicle
- `PUT /api/Vehicle/{id}` - Update vehicle
- `DELETE /api/Vehicle/{id}` - Delete vehicle

### Transaction Management
- `GET /api/Transaction` - Get all transactions
- `GET /api/Transaction/{id}` - Get transaction by ID
- `POST /api/Transaction` - Create new transaction
- `PUT /api/Transaction/{id}` - Update transaction
- `DELETE /api/Transaction/{id}` - Delete transaction

## 🏗️ Architecture Features

### Repository Pattern
- Clean separation of data access logic
- Interface-based design for testability
- Dependency injection implementation

### Service Layer
- Complex business logic implementation
- Transaction management
- Data validation and processing

### Authentication & Authorization
- JWT token-based security
- Role-based access control
- Secure endpoint protection

### Unit Testing
- Comprehensive test coverage
- Mock-based testing with Moq
- Controller, Service, and Repository tests

## 🌐 Azure Deployment

The application is deployed on Microsoft Azure with the following services:

- **Azure App Service**: Hosting the Web API
- **Azure Database for PostgreSQL**: Cloud database
- **Azure DevOps**: CI/CD pipeline automation

### Deployment Features
- Automated deployment pipeline
- Environment-specific configurations
- Health monitoring and logging

## 🚀 Getting Started with the Live API

1. **Register a new user**:
   ```bash
   POST https://carmanagement-ademmevaip.azurewebsites.net/api/Auth/register
   ```

2. **Login to get JWT token**:
   ```bash
   POST https://carmanagement-ademmevaip.azurewebsites.net/api/Auth/login
   ```

3. **Use the token in Authorization header**:
   ```
   Authorization: Bearer <your-jwt-token>
   ```

4. **Access protected endpoints**:
   ```bash
   GET https://carmanagement-ademmevaip.azurewebsites.net/api/Vehicle
   ```

## 📈 Project Accomplishments

✅ **Complete .NET Core Web API**
- ✅ CRUD operations implementation
- ✅ Complex business logic
- ✅ Repository pattern with dependency injection
- ✅ Comprehensive unit testing

✅ **Authentication & Authorization**
- ✅ User login functionality
- ✅ Role-based authorization
- ✅ JWT-secured endpoints

✅ **Azure Deployment**
- ✅ Successful cloud deployment
- ✅ CI/CD pipeline implementation

✅ **GitHub Usage**
- ✅ Consistent commits with clear messages
- ✅ Comprehensive README documentation

## 🎯 Target Audience

- Vehicle rental companies
- Car dealerships
- Fleet management departments
- Transportation service providers
- Automotive businesses requiring vehicle inventory management
---
