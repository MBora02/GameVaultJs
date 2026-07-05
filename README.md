# 🎮 GameVault

[![.NET Version](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![Database](https://img.shields.io/badge/Database-SQL_Server-red.svg)](https://www.microsoft.com/en-us/sql-server)
[![ORM](https://img.shields.io/badge/ORM-EF_Core-purple.svg)](https://learn.microsoft.com/en-us/ef/core/)
[![UI](https://img.shields.io/badge/UI-Bootstrap_5.3.8-brightgreen.svg)](https://getbootstrap.com/)
[![PDF Generation](https://img.shields.io/badge/PDF_Generation-QuestPDF-orange.svg)](https://www.questpdf.com/)
[![Excel Export](https://img.shields.io/badge/Excel_Export-EPPlus-blueviolet.svg)](https://epplussoftware.com/)

> A modern, robust, and asynchronous web portal designed for gaming catalog curation, genre categorization, and publication of game-related news. GameVault incorporates role-based access control (RBAC), custom client-side AJAX routines, and high-quality document reporting to ensure a premium user experience for game store administrators and users alike.

---

## 🛠️ Technology Stack

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Core Framework** | ASP.NET Core MVC (.NET 10.0) | High-performance, cross-platform web application design using Model-View-Controller architecture. |
| **Database Engine** | Microsoft SQL Server | Relational database engine hosting game, user, genre, and news datasets. |
| **Object-Relational Mapper (ORM)** | Entity Framework Core 10.0.9 | Database-first & code-first migrations with fluent mappings. |
| **Front-End Styling** | Bootstrap 5.3.8 & FontAwesome 6.7.2 | Premium responsive layout components and modern vector icons. |
| **Client-Side Scripting** | jQuery 3.7.1 & jQuery.Validation 1.21.0 | Asynchronous HTTP requests (AJAX) and dynamic DOM manipulation. |
| **PDF Reporting** | QuestPDF 2026.6.1 | High-speed layout engine for generating rich PDF documents on-the-fly. |
| **Excel Exporting** | EPPlus 8.6.1 | Programmatic generation and styling of spreadsheets (Office Open XML format). |

---

## ✨ Key Features

- **🔐 Secure Role-Based Authentication**
  - Custom Cookie-based Authentication framework.
  - Distinct access scopes: `Admin` and `User`.
  - Automatic redirection: Administrators land on the Game inventory control, while standard users land on the News portal.
- **🎮 Game Inventory Catalog (Admin Exclusive)**
  - Full CRUD capabilities managed dynamically via jQuery AJAX.
  - Clean modal forms for adding and editing games with zero-page-reload states.
  - Interactive grid displaying game titles, developer information, targeted platforms, prices, and description logs.
- **🏷️ Genre Organization & Analytics (Admin Exclusive)**
  - Category curation tracks active game counts per genre.
  - Modal-based asynchronous operations (Create, Read, Update, Delete).
- **📋 Premium Document Generation**
  - **PDF Export**: Generates custom styled PDF lists of genres utilizing QuestPDF.
  - **Excel Export**: Produces high-fidelity tables with customized headers, cell autofits, and styling using EPPlus.
- **📰 Gaming News Feed (Accessible by All Logged-In Users)**
  - Curation panel to publish and update hot news articles.
  - Interactive layouts and publishing date flags.

---

### 📸 Application Interface Previews

#### 1. Security Login & Registration Portal
![Login Interface](https://github-production-user-asset-6210df.s3.amazonaws.com/placeholder-login.png)
*Instruction: Upload a screenshot of the login form / portal here.*

#### 2. Game Inventory & Catalog Control (Admin Panel)
![Game Inventory View](https://github-production-user-asset-6210df.s3.amazonaws.com/placeholder-games.png)
*Instruction: Upload a screenshot of the Game management list dashboard with the dynamic modal popup active.*

#### 3. Curation of Gaming Genres with Document Action Center
![Genre Curation Panel](https://github-production-user-asset-6210df.s3.amazonaws.com/placeholder-genres.png)
*Instruction: Upload a screenshot of the Genre curation catalog page highlighting the Export PDF and Export Excel buttons.*

#### 4. Gamer News Portal Feed (User Hub)
![News Feed View](https://github-production-user-asset-6210df.s3.amazonaws.com/placeholder-news.png)
*Instruction: Upload a screenshot of the main News list showing published updates and article headers.*

---

## 🏗️ Architecture & Folder Structure

GameVault uses a structured **MVC (Model-View-Controller)** pattern backed by an asynchronous database layer powered by EF Core. The application uses a hybrid approach: standard MVC controllers serve index layouts, while child endpoints operate asynchronously via `JsonResult` responses, mapped onto client-side views dynamically through jQuery DOM operations.

```
GameVaultJs/
├── GameVaultJs/                 # Core ASP.NET Core Project Folder
│   ├── Controllers/             # Directs incoming requests to actions (AJAX & View triggers)
│   │   ├── GameController.cs
│   │   ├── GenreController.cs
│   │   ├── HomeController.cs
│   │   ├── LoginController.cs
│   │   └── NewsController.cs
│   ├── Data/                    # Infrastructure / Database Context Configuration
│   │   └── ApplicationDbContext.cs
│   ├── Helpers/                 # General-purpose utility systems
│   │   └── HashHelper.cs        # Cryptographic password hashing & verification
│   ├── Migrations/              # Entity Framework Core Code-First Migrations
│   ├── Models/                  # Application entities (Game, Genre, News, User)
│   ├── Properties/              # Launch environment variables (launchSettings.json)
│   ├── Views/                   # Razor Views and Multi-role layouts
│   │   ├── Game/
│   │   ├── Genre/
│   │   ├── Home/
│   │   ├── Login/
│   │   ├── News/
│   │   └── Shared/              # Shared layouts (_Layout, _AdminLayout, _UserLayout)
│   ├── wwwroot/                 # Public web assets
│   │   ├── css/                 # Custom styling layouts (site.css)
│   │   ├── js/                  # Client-side controller scripts (Game.js, Genre.js, News.js)
│   │   └── lib/                 # Standard framework scripts (bootstrap, jquery)
│   ├── appsettings.json         # Storage of credentials & connection strings
│   └── Program.cs               # Host configuration, services register, and app execution
├── GameVaultJs.slnx             # Lightweight XML-based solution definition
└── project5 ajax/               # Empty sandbox placeholder directory
```

---

## ⚙️ Setup & Installation Guide

### 📋 Prerequisites

To run this application locally, please verify you have the following installed:
- **.NET 10.0 SDK** or newer (Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download))
- **Microsoft SQL Server** (LocalDB, Express edition, or higher)
- **Visual Studio 2022 / JetBrains Rider** (or VS Code with appropriate C# extension sets)

### 🚀 Getting Started

#### 1. Clone the Project
Open a shell terminal and clone the repository:
```bash
git clone https://github.com/MBora02/GameVaultJs.git
cd GameVaultJs
```

#### 2. Local Database Configuration
Navigate to [GameVaultJs/appsettings.json](file:///C:/Users/Bora/Desktop/GameVaultJs/GameVaultJs/appsettings.json) and modify the `Default` connection string in `ConnectionStrings` to match your local SQL Server instance:

```json
"ConnectionStrings": {
  "Default": "Server=YOUR_SERVER_NAME;Database=GameVaultDb;TrustServerCertificate=true;Trusted_Connection=True"
}
```

> [!NOTE]
> Replace `YOUR_SERVER_NAME` (e.g., `DESKTOP-XXXX\\SQLEXPRESS` or `(localdb)\\MSSQLLocalDB`) with your actual SQL Server address.

#### 3. Database Migration
Apply the prepared migrations to automatically create the database schemas and seed default credentials:

**Via dotnet CLI:**
```bash
dotnet ef database update --project GameVaultJs
```

**Via Package Manager Console (inside Visual Studio):**
```powershell
Update-Database -Project GameVaultJs
```

#### 4. Run the Application
Start the .NET development server using the following command:

```bash
dotnet run --project GameVaultJs
```

Once running, open your web browser and navigate to the application:
- **HTTPS endpoint**: `https://localhost:7064`
- **HTTP endpoint**: `http://localhost:5204`

---

## 🔑 Default Credentials

The database seeding process automatically provisions the following default users:

| Email | Password | Assigned Role | Access Rights |
| :--- | :--- | :--- | :--- |
| **admin@gamevault.com** | `adminpassword` | `Admin` | Full CRUD privileges over Games, Genres, and News catalog. |
| **user@gamevault.com** | `userpassword` | `User` | Access to the News Portal feed. |

---

## 📄 License & Attribution

This project is licensed under the **MIT License**. For more information, please see the standard license conditions.

**Attribution:**
- PDF generation leverages the Community License of [QuestPDF](https://www.questpdf.com/).
- Excel spreadsheet functionality utilizes the Non-Commercial Personal license of [EPPlus](https://epplussoftware.com/).
