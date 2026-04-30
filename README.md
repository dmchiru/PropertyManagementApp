# Property Management App

Repository: https://github.com/dmchiru/PropertyManagementApp.git

## Project Summary

Property Management App is a full-stack web application created for a property management workflow. The system is designed to help landlords or property managers organize tenants, properties, rent collection, maintenance projects, invoices, communication logs, and eviction preparation records from one centralized dashboard.

The application includes an ASP.NET Core Web API backend, a Blazor WebAssembly frontend, a shared DTO project, SQL Server LocalDB integration, and JWT-based admin authentication. The goal of the project is to provide a workflow-first property management system where an administrator can log in, view records, manage CRUD operations, and track important rental business processes.

## Main Features

- Admin login with JWT authentication
- Protected API endpoints
- Dashboard with property management metrics
- Properties module
  - View properties
  - Choose active property
  - Add, edit, view details, and delete properties
- Tenants module
  - View tenants
  - Add, edit, view details, and delete tenants
  - View tenant ledger
- Rent Collection module
  - Track rent schedules
  - Mark rent as paid
  - Apply late fees
  - Send reminders
- Rent Records module
  - View tenant ledger
  - Print statement
  - Export CSV
- Maintenance module
  - Create maintenance projects
  - Assign projects to properties
  - Track project status
  - View work logs
- Invoicing module
  - Create sample invoice
  - Send invoice
  - Mark invoice as paid
  - Track invoice status
- Communications module
  - Review SMS, email, and call logs
- Eviction Prep module
  - Start eviction cases
  - Advance case workflow
  - Close cases
  - Includes a legal disclaimer

## Technologies Used

- C#
- ASP.NET Core Web API
- Blazor WebAssembly
- Entity Framework Core
- SQL Server LocalDB
- JWT Authentication
- Bootstrap
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
└── PropertyManagementApp.Api.slnx
