# Online Book Ordering Website

This project is a simple Online Book Ordering website built using ASP.NET Core 7.0.4 with SQLite as the database. It provides an admin panel to manage book products (create, edit, and remove), a cart system, search functionality, and a homepage to list all available products.

## Features
- Admin panel for managing books (Create, Edit, Delete)
- List of all books on the homepage
- Search functionality for books
- Simple shopping cart system

## Prerequisites
- .NET 7.0.4 SDK or later
- SQLite database
- Visual Studio Code or Visual Studio
- Git (optional)

## Screen Shots
![image](https://github.com/user-attachments/assets/69be508b-bfe8-40db-8a8d-7e4ade19df71)
![image](https://github.com/user-attachments/assets/86c9bc1d-6b8a-4999-87f7-c63753c03739)
![image](https://github.com/user-attachments/assets/c14d3c66-5142-4dc9-9cde-ff1e607f47ea)


## Installation

### 1. Clone the Repository

```bash
git clone https://github.com/Oguzoz1/.Net-Simple-WebPage
```
### 2. Install .NET 7 SDK
If you don't have .NET 7 installed, download and install it from the official [.NET website](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

### 3. Restore Dependencies
Navigate to the project folder and restore all dependencies using:
```bash
dotnet restore
```
### 4. Update the Database Connection String
Update the `appsettings.json` file with the SQLite connection string. Here is a sample connection string for SQLite:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=bookstore.db"
  }
}
```

### 5. Run Entity Framework Migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 6. Run The Application
```bash
dotnet run
```
