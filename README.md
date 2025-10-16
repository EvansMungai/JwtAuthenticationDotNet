# JwtAuthenticationDotNet

A minimal ASP.NET Core API demonstrating JWT authentication and role-based authorization with Identity, Entity Framework Core, and Swagger UI integration.

## Features

- **User Registration and Login** with JWT token issuance.
- **Role-Based and Policy-Based Authorization**.
- **Custom User Model** extending IdentityUser.
- **In-Memory Database (default) or SQLite support** for demo and testing.
- **Token generation with claims and role embedding**.
- **Swagger UI integration** with JWT authentication support.
- **Clean architecture with modular service registration and middleware configuration.**

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- IDE (Visual Studio 2022, VS Code, JetBrains Rider)
- (Optional) SQLite CLI if you switch from In-Memory to SQLite

### Setup

1. **Clone the repository**

   ```bash
   git clone https://github.com/yourusername/JwtAuthenticationDotNet.git
   cd JwtAuthenticationDotNet
2. **Configure Secrets**
   The app uses user secrets or appsettings.json for JWT configuration. Add your JWT settings:
   
   ```bash
   {
      "Jwt": {
        "Key": "YourSuperSecretKeyThatIsLongEnoughAndSecure123!@#",
        "Issuer": "JwtAuthDemo",
        "Audience": "JwtAuthDemoClient"
      }
   }
3. **Run the Application**

       ```bash
       dotnet run
   
    The API will be available at https://localhost:{port}.
  
4. **Access Swagger UI**
    Navigate to https://localhost:{port}/swagger to explore and test the API.
    

## Architecture Overview
### Authentication and Authorization
* Uses ASP.NET Core Identity with a custom AppUser class.
* JWT tokens are generated on registration and login via TokenService.
* Tokens embed user roles and platform claims.
* Authorization policies control access to endpoints, e.g., Admin, Customer, WebUser.
* Middleware handles authentication and authorization flow.
### Data Storage
* Uses Entity Framework Core.
* Default: In-Memory database for ease of testing.
* Can be switched to SQLite by changing configuration in ServiceRegistration.
### Project Structure
* Data: EF Core DbContext and User model.
* Services: Token generation, role seeding, authentication & authorization configuration.
* Extensions: Middleware and route configuration extensions.
* Routes: Route definitions separated for clarity.
* Program.cs: Main entry point wiring up DI, middleware, and routes.

## API Endpoints
### Public Routes (No Authentication Required)
* POST /register — Register a new user and receive a JWT token.
* POST /login — Authenticate user and get JWT token.
### Protected Routes (Required Authentication and/or Authorization)
* GET /profile — Retrieve current user profile (authenticated users).
* GET /admin — Admin-only route.
* GET /customer — Customer-only route.
* GET /web — Web user with specific policy.

## Notes & Recommendations
* JWT Key must be sufficiently long and kept secret.
* Role Seeding happens on app startup.
* Tokens expire after 2 hours by default.
* Expand policies and roles as per your app’s needs.
* Add database migrations and switch to a persistent DB for production.

## Contributions
Contributions are welcome! Please fork the repo and submit a pull request.
