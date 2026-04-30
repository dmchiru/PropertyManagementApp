# Property Management App

## Project Summary

Property Management App is a full-stack web application designed to help landlords and property managers manage rental property workflows from one centralized system. The project includes modules for properties, tenants, rent collection, rent records, maintenance projects, invoices, communication logs, and eviction preparation.

The application was built using an ASP.NET Core Web API backend, a Blazor WebAssembly frontend, a shared DTO project, SQL Server LocalDB, Entity Framework Core, and JWT-based admin authentication.

## Repository

```text
https://github.com/dmchiru/PropertyManagementApp.git
```

## Admin Login Credentials

The project includes a demo administrator account for testing and grading purposes.

```text
Email: admin@propertymanagement.local
Password: Admin123!
```

These credentials allow the user to log in as an administrator and access the protected areas of the application.

## Main Features

### Authentication

- Admin login
- JWT-based authentication
- Protected API endpoints
- Logout functionality
- Session-based token storage

### Dashboard

- Overview of tenants, rent due, invoices, maintenance, and eviction cases
- Navigation cards for the main system modules

### Properties Module

- View all properties
- Choose an active property
- Add new properties
- Edit existing properties
- View property details
- Delete properties when they are not connected to tenants or maintenance projects

### Tenants Module

- View all tenants
- Search tenants
- Add new tenants
- Edit tenant information
- View tenant details
- Delete tenants
- Access tenant rent ledger

### Rent Collection Module

- View rent schedules
- Track rent status
- Mark rent as paid
- Apply late fees
- Send rent reminders
- Filter and search rent records

### Rent Records Module

- View tenant ledger
- Review charges, payments, and balances
- Print statements
- Export ledger records to CSV

### Maintenance Module

- Create maintenance projects
- Assign projects to properties
- Track vendors, bids, and project status
- Start and close maintenance work
- View work logs

### Invoicing Module

- View invoices
- Create sample invoices
- Send invoices
- Mark invoices as paid
- Track invoice status and outstanding balances

### Communications Module

- View SMS, email, and call logs
- Search communication history
- Filter by communication channel

### Eviction Prep Module

- Start eviction cases
- Track eviction workflow steps
- Advance case status
- Close eviction cases
- Includes a legal disclaimer for academic and informational purposes

## Technologies Used

- C#
- ASP.NET Core Web API
- Blazor WebAssembly
- Entity Framework Core
- SQL Server LocalDB
- JWT Authentication
- Bootstrap
- HTML
- CSS
- JavaScript
- Visual Studio
- GitHub

## Project Structure

```text
PropertyManagementApp
│
├── PropertyManagementApp.Api
│   ├── Controllers
│   ├── Data
│   ├── Models
│   ├── Security
│   ├── Services
│   └── Program.cs
│
├── PropertyManagementApp.Client
│   ├── Auth
│   ├── Layout
│   ├── Pages
│   ├── Services
│   └── wwwroot
│
├── PropertyManagementApp.Shared
│   └── DTOs
│
├── PropertyManagementDB_Script.sql
└── PropertyManagementApp.Api.slnx
```

## How to Run the Project

### Requirements

- Visual Studio 2022 or later
- .NET SDK
- SQL Server LocalDB
- Entity Framework Core packages
- Git

### Steps

1. Clone the repository.

```bash
git clone https://github.com/dmchiru/PropertyManagementApp.git
```

2. Open the solution in Visual Studio.

```text
PropertyManagementApp.Api.slnx
```

3. Restore NuGet packages.

4. Make sure the API connection string in `PropertyManagementApp.Api/appsettings.json` points to SQL Server LocalDB.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PropertyManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

5. If the database has not been created yet, run the SQL script included in the project.

```text
PropertyManagementDB_Script.sql
```

6. In Visual Studio, configure both projects to start together.

```text
Right click Solution > Configure Startup Projects > Multiple startup projects
```

Set these projects to **Start**:

```text
PropertyManagementApp.Api
PropertyManagementApp.Client
```

7. Run the application.

8. Open the Blazor client in the browser and log in using the admin credentials.

## Development URLs

The API runs on:

```text
https://localhost:7108
http://localhost:5138
```

The Blazor client uses the API base URL configured in:

```text
PropertyManagementApp.Client/wwwroot/appsettings.json
```

Example:

```json
{
  "ApiBaseUrl": "https://localhost:7108/"
}
```

## Important Application Routes

```text
/login
/properties
/tenants
/rent-collection
/rent-records
/maintenance
/invoicing
/communications
/eviction-prep
```

## Notes About Security

This project uses demo credentials for academic testing and grading. In a production environment, the administrator password should not be stored directly in `appsettings.json`. A production version should use secure password hashing, ASP.NET Core Identity, environment variables, role management, and secret management.

## AI Assistance Disclosure

AI assistance was used as guided support to understand JWT authentication, Blazor WebAssembly integration, Web API security, CRUD workflow, and Visual Studio/GitHub setup. The implementation was reviewed, edited, tested, and validated by the student.

## Author

Dianelsa Chiru and Monica Monterrosa
CS458 
University of Southern Indiana
