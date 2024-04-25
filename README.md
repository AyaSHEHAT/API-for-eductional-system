# :rocket: ASP.NET Core API Project

An ASP.NET Core API project for managing Departments and students, with authentication using JWT.

## :computer: Technologies Used

- ASP.NET Core: Framework for building API applications.
- Entity Framework Core: ORM for data access.
- JWT Authentication: Secure API access using JSON Web Tokens.
- Swagger/OpenAPI: API documentation and testing tool.
- SQL Server: Database management system.

## :pencil: Description

This ASP.NET Core API project is designed to manage Departments and students, providing endpoints for CRUD operations and JWT authentication for secure access.

## :gear: Configuration

1. Clone this repository to your local machine.
2. Ensure you have the necessary dependencies installed, including Visual Studio and SQL Server.
3. Update the connection string in `appsettings.json` to point to your SQL Server instance.
4. Build and run the application.

## :key: Authentication Setup

- JWT authentication is configured using a secret key.
- Update the `HERE IS THE SECRET KEY FOR THIS App` placeholder with your actual secret key.

## :hammer_and_wrench: Features

- **Swagger Documentation**: API documentation and testing via Swagger UI.
- **JWT Authentication**: Secure API access using JSON Web Tokens.
- **Entity Framework Core**: Data access using ORM for SQL Server.
- **CORS Configuration**: Configured to allow cross-origin requests.

## :bulb: Usage

1. Launch the API project.
2. Use Swagger UI (`/swagger`) for API documentation and testing.
3. Authenticate using JWT to access protected endpoints.
4. Perform CRUD operations for Departments and students via API endpoints.

## :warning: Additional Notes

- Customize authentication and authorization logic as per your application's requirements.
- Implement proper error handling and validation for API endpoints.
- Ensure CORS policy is set up securely based on your application's needs.

---

This README provides an overview of the ASP.NET Core API project, its configuration, features, usage in
