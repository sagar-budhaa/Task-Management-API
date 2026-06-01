# Task Management API

A modern, RESTful API for managing projects and tasks with user authentication. Built with ASP.NET Core and Entity Framework, this API provides a clean and intuitive way to organize, track, and manage your projects and their associated tasks.

## 🎯 Features

- **User Authentication**: Secure JWT-based authentication system for user registration and login
- **Project Management**: Create, read, update, and delete projects with detailed tracking
- **Task Management**: Organize tasks within projects, mark them as complete, and update their status
- **Authorization**: Every project and task is tied to the authenticated user—your data stays yours
- **Input Validation**: Comprehensive server-side validation on all request DTOs
- **Consistent Response Format**: All endpoints return a standardized response wrapper with status codes and error messages
- **OpenAPI/Swagger Support**: Built-in API documentation for easy exploration

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core (.NET 10)
- **Database**: MySQL (Entity Framework Core)
- **Authentication**: JWT (JSON Web Tokens)
- **Documentation**: OpenAPI 3.1.1 / Swagger
- **Language**: C#

## 📋 Prerequisites

Before you get started, make sure you have:

- **.NET 10 SDK** or later ([Download here](https://dotnet.microsoft.com/download))
- **MySQL Server** (local or remote)
- **Visual Studio Code**, **Visual Studio**, or **JetBrains Rider** (optional, for development)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/sagar-budhaa/Task-Management-API.git
cd task-management-api
```

### 2. Configure the Database

Update your connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagementDb;User=root;Password=yourpassword"
  }
}
```

> **Tip**: Create an `appsettings.Development.json` for your local environment (it won't be committed to Git).

### 3. Set Up JWT Secrets

Add your JWT configuration to `appsettings.json`:

```json
{
  "AppSettings": {
    "Secret": "your-super-secret-key-min-32-chars-long",
    "Issuer": "TaskManagementAPI",
    "Audience": "TaskManagementAPIUsers"
  }
}
```

### 4. Apply Database Migrations

Run the following command to set up your database schema:

```bash
dotnet ef database update
```

If you want to add a new migration after model changes:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### 5. Build and Run

Build the project:

```bash
dotnet build
```

Run the project:

```bash
dotnet run
```

The API will start at `https://localhost:7072/` by default.

### 6. Access the API Documentation

Once running, open your browser and navigate to:

```
https://localhost:7072/openapi/v1.json
```

Or use the interactive Swagger UI at:

```
https://localhost:7072/scalar/v1
```

## 🔐 Authentication

This API uses **JWT (JSON Web Tokens)** for authentication. Here's how it works:

### Register a New User

**Endpoint**: `POST /api/Auth/register`

Request body:
```json
{
  "username": "john_doe",
  "password": "SecurePassword123"
}
```

Response (201 Created):
```json
{
  "data": {
    "success": true,
    "message": "User registered successfully"
  },
  "success": true,
  "statusCode": 201
}
```

### Login

**Endpoint**: `POST /api/Auth/login`

Request body:
```json
{
  "username": "john_doe",
  "password": "SecurePassword123"
}
```

Response (200 OK):
```json
{
  "data": {
    "success": true,
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "message": "User logged in"
  },
  "success": true,
  "statusCode": 200
}
```

### Get Current User

**Endpoint**: `GET /api/Auth/me`

Requires Authorization header:
```
Authorization: Bearer <your_access_token>
```

Response:
```json
{
  "username": "john_doe"
}
```

## 📚 API Endpoints

All endpoints (except register/login) require the `Authorization: Bearer <token>` header.

### Projects

#### Get All Projects

**Endpoint**: `GET /api/Project`

Returns all projects for the authenticated user.

**Response**:
```json
{
  "data": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "name": "Website Redesign",
      "description": "Complete overhaul of company website",
      "status": "In Progress",
      "isActive": true,
      "createdAt": "2026-06-01T10:30:00Z"
    }
  ],
  "success": true,
  "statusCode": 200
}
```

#### Create a Project

**Endpoint**: `POST /api/Project`

**Request body**:
```json
{
  "name": "Mobile App Development",
  "description": "Build iOS and Android apps",
  "status": "Planning"
}
```

**Response** (201 Created):
```json
{
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "name": "Mobile App Development",
    "description": "Build iOS and Android apps",
    "status": "Planning",
    "isActive": true,
    "createdAt": "2026-06-01T10:35:00Z"
  },
  "success": true,
  "statusCode": 201
}
```

#### Get a Specific Project

**Endpoint**: `GET /api/Project/{id}`

**Response**:
```json
{
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Website Redesign",
    "description": "Complete overhaul of company website",
    "status": "In Progress",
    "isActive": true,
    "createdAt": "2026-06-01T10:30:00Z"
  },
  "success": true,
  "statusCode": 200
}
```

#### Update a Project

**Endpoint**: `PUT /api/Project/{id}`

**Request body**:
```json
{
  "name": "Website Redesign v2",
  "description": "Updated overhaul of company website",
  "status": "In Progress",
  "isActive": true,
  "endDate": "2026-12-31T23:59:59Z"
}
```

**Response**:
```json
{
  "data": {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Website Redesign v2",
    "description": "Updated overhaul of company website",
    "status": "In Progress",
    "isActive": true,
    "createdAt": "2026-06-01T10:30:00Z"
  },
  "success": true,
  "statusCode": 200
}
```

#### Delete a Project

**Endpoint**: `DELETE /api/Project/{id}`

**Response** (200 OK):
```json
{
  "data": true,
  "success": true,
  "statusCode": 200
}
```

### Tasks

#### Get All Tasks for a Project

**Endpoint**: `GET /api/Task/{projectId}`

Returns all tasks in a specific project.

**Response**:
```json
{
  "data": [
    {
      "id": "660e8400-e29b-41d4-a716-446655440000",
      "name": "Design mockups",
      "isCompleted": false,
      "createdAt": "2026-06-01T11:00:00Z",
      "updatedAt": "2026-06-01T11:00:00Z",
      "projectId": "550e8400-e29b-41d4-a716-446655440000"
    }
  ],
  "success": true,
  "statusCode": 200
}
```

#### Create a Task

**Endpoint**: `POST /api/Task/{projectId}`

**Request body**:
```json
{
  "name": "Implement authentication",
  "isActive": true
}
```

**Response** (201 Created):
```json
{
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "name": "Implement authentication",
    "isCompleted": true,
    "createdAt": "2026-06-01T11:05:00Z",
    "updatedAt": "2026-06-01T11:05:00Z",
    "projectId": "550e8400-e29b-41d4-a716-446655440000"
  },
  "success": true,
  "statusCode": 201
}
```

#### Get a Specific Task

**Endpoint**: `GET /api/Task/{projectId}/{id}`

**Response**:
```json
{
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440000",
    "name": "Design mockups",
    "isCompleted": false,
    "createdAt": "2026-06-01T11:00:00Z",
    "updatedAt": "2026-06-01T11:00:00Z",
    "projectId": "550e8400-e29b-41d4-a716-446655440000"
  },
  "success": true,
  "statusCode": 200
}
```

#### Update a Task

**Endpoint**: `PUT /api/Task/{projectId}/{id}`

**Request body**:
```json
{
  "name": "Design mockups - Updated",
  "isActive": true
}
```

**Response**:
```json
{
  "data": {
    "id": "660e8400-e29b-41d4-a716-446655440000",
    "name": "Design mockups - Updated",
    "isCompleted": true,
    "createdAt": "2026-06-01T11:00:00Z",
    "updatedAt": "2026-06-01T11:10:00Z",
    "projectId": "550e8400-e29b-41d4-a716-446655440000"
  },
  "success": true,
  "statusCode": 200
}
```

#### Delete a Task

**Endpoint**: `DELETE /api/Task/{projectId}/{id}`

**Response**:
```json
{
  "data": true,
  "success": true,
  "statusCode": 200
}
```

## 📦 Request/Response Models

### Project Models

#### ProjectCreateDto
```json
{
  "name": "string (required, 3-200 chars)",
  "description": "string (required, max 2000 chars)",
  "status": "string (optional, max 100 chars)"
}
```

#### ProjectUpdateDto
```json
{
  "name": "string (required, 3-200 chars)",
  "description": "string (required, max 2000 chars)",
  "status": "string (optional, max 100 chars)",
  "endDate": "ISO 8601 datetime (optional)",
  "isActive": "boolean"
}
```

#### ProjectResponseDto
```json
{
  "id": "UUID",
  "name": "string",
  "description": "string",
  "status": "string",
  "isActive": "boolean",
  "createdAt": "ISO 8601 datetime"
}
```

### Task Models

#### TaskCreateDto
```json
{
  "name": "string (required, 1-200 chars)",
  "isActive": "boolean"
}
```

#### TaskUpdateDto
```json
{
  "name": "string (required, 1-200 chars)",
  "isActive": "boolean"
}
```

#### TaskResponseDto
```json
{
  "id": "UUID",
  "name": "string",
  "isCompleted": "boolean",
  "createdAt": "ISO 8601 datetime",
  "updatedAt": "ISO 8601 datetime",
  "projectId": "UUID"
}
```

### User Models

#### UserRegisterRequestDto
```json
{
  "username": "string (required, 3-100 chars)",
  "password": "string (required, 6-100 chars)"
}
```

#### UserLoginRequestDto
```json
{
  "username": "string (required, 3-100 chars)",
  "password": "string (required, 6-100 chars)"
}
```

## ⚠️ Error Handling

The API returns standardized error responses. Here are common scenarios:

### Validation Error (400 Bad Request)

```json
{
  "success": false,
  "statusCode": 400,
  "errorMessage": "Username is required. Password must be at least 6 characters.",
  "data": null
}
```

### Resource Not Found (404 Not Found)

```json
{
  "success": false,
  "statusCode": 404,
  "errorMessage": "Project not found",
  "data": null
}
```

### Unauthorized (401 Unauthorized)

Missing or invalid JWT token in the Authorization header.

```json
{
  "success": false,
  "statusCode": 401,
  "errorMessage": "Unauthorized"
}
```

## 💻 Usage Examples

### Using PowerShell

Register a new user:
```powershell
$body = @{
    username = "testuser"
    password = "TestPassword123"
} | ConvertTo-Json

Invoke-WebRequest -Uri "https://localhost:7072/api/Auth/register" `
  -Method POST `
  -ContentType "application/json" `
  -Body $body
```

Login and get token:
```powershell
$loginBody = @{
    username = "testuser"
    password = "TestPassword123"
} | ConvertTo-Json

$response = Invoke-WebRequest -Uri "https://localhost:7072/api/Auth/login" `
  -Method POST `
  -ContentType "application/json" `
  -Body $loginBody

$token = ($response.Content | ConvertFrom-Json).data.accessToken
```

Create a project:
```powershell
$headers = @{
    Authorization = "Bearer $token"
    "Content-Type" = "application/json"
}

$projectBody = @{
    name = "My First Project"
    description = "This is a test project"
    status = "Active"
} | ConvertTo-Json

Invoke-WebRequest -Uri "https://localhost:7072/api/Project" `
  -Method POST `
  -Headers $headers `
  -Body $projectBody
```

### Using cURL

Register:
```bash
curl -X POST https://localhost:7072/api/Auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"testuser","password":"TestPassword123"}'
```

Login:
```bash
curl -X POST https://localhost:7072/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"testuser","password":"TestPassword123"}'
```

Create a project (replace TOKEN with actual token):
```bash
curl -X POST https://localhost:7072/api/Project \
  -H "Authorization: Bearer TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"name":"My Project","description":"Test","status":"Planning"}'
```

## 📁 Project Structure

```
Task Management API/
├── Controllers/           # API endpoint handlers
│   ├── Auth/
│   ├── Project/
│   └── Task/
├── Models/               # Database entities
├── DTOs/                 # Data Transfer Objects
├── Services/             # Business logic layer
│   ├── Auth/
│   ├── Project/
│   ├── Task/
│   └── Result/
├── Data/                 # Entity Framework context
├── Migrations/           # Database migrations
├── lib/                  # Utilities (JWT service)
├── Program.cs            # Application startup
├── appsettings.json      # Configuration
└── README.md             # This file
```

## 🔧 Configuration

### Environment Variables

Create an `appsettings.Development.json` file for local development:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagementDb_Dev;User=root;Password=yourpassword"
  },
  "AppSettings": {
    "Secret": "your-development-secret-key",
    "Issuer": "TaskManagementAPI",
    "Audience": "TaskManagementAPIUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

> **Important**: Never commit this file to Git. It's listed in `.gitignore` to protect sensitive data.

## 🧪 Testing the API

You can test the API using:

1. **Swagger UI**: Visit `https://localhost:7072/scalar/v1` after starting the server
2. **Postman**: Import the OpenAPI spec from `/openapi/v1.json`
3. **Thunder Client**: VS Code extension for testing REST APIs
4. **cURL or PowerShell**: See examples above

## 🐛 Troubleshooting

### "Connection refused" to database
- Ensure MySQL is running
- Check the connection string in `appsettings.json`
- Verify the username and password

### "No migrations pending"
- Run `dotnet ef database update` to apply migrations
- If migrations are missing, run `dotnet ef migrations add InitialCreate`

### JWT token expired or invalid
- Generate a new token by logging in again
- Ensure the token is included in the `Authorization: Bearer <token>` header

### Port already in use
- Change the port in `launchSettings.json`
- Or kill the process using port 7072

## 📝 Development Notes

- All passwords are stored as plain text for simplicity (in production, use bcrypt/hashing)
- Implement rate limiting for production deployments
- Add more comprehensive logging for debugging
- Consider implementing soft deletes for data retention

## 🤝 Contributing

Feel free to fork, improve, and submit pull requests! Here's how:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

If you encounter any issues or have questions, please open an issue on GitHub or contact the development team.

---

**Happy task managing! 🎉**

