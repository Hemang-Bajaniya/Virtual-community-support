# Virtual Community Support Platform

This platform will enable the company to plan and manage efforts that motivate and empower employees to effectively serve community needs through the leadership of the employer.

This repository contains both the **Frontend** (Angular) and **Backend** (ASP.NET Core Web API) for the Virtual Community Support platform. The system enables users to register, manage their profiles, explore and apply for missions, and for admins to manage missions, users, and related data.

---

## Table of Contents
- [Project Structure](#project-structure)
- [Frontend (Angular)](#frontend-angular)
  - [Features](#frontend-features)
  - [Getting Started](#frontend-getting-started)
- [Backend (ASP.NET Core Web API)](#backend-aspnet-core-web-api)
  - [Features](#backend-features)
  - [Getting Started](#backend-getting-started)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

---

## Project Structure

```
Mission_FrontEnd/
├── src/
│   ├── app/           # Angular application source code
│   ├── assets/        # Images and static assets
│   └── environments/  # Angular environment configs
├── README.md          # This file
└── ...
Backend (suggested structure):
├── Controllers/
├── Models/
├── Data/
├── Migrations/
├── Program.cs
├── appsettings.json
└── ...
```

---

## Frontend (Angular)

### Features
- User registration, login, and JWT authentication
- User profile management (edit profile, change password, add skills)
- Mission listing (grid/list), search, sort, and pagination
- Mission application and status tracking
- Admin dashboard for mission/user management
- Responsive UI with modern design

### Getting Started
1. **Install dependencies:**
   ```sh
   npm install
   ```
2. **Run the development server:**
   ```sh
   ng serve
   ```
   Navigate to `http://localhost:4200/` in your browser.

3. **Environment Configuration:**
   - API base URL and other settings are in `src/environments/environment.ts` (or `environment.staging.ts`, `environment.prod.ts`).

4. **Build for production:**
   ```sh
   ng build --configuration production
   ```

---

## Backend (ASP.NET Core Web API)

### Features
- RESTful API for missions, users, applications, skills, themes, countries, and cities
- JWT authentication and authorization
- Entity Framework Core for database access
- Sorting, filtering, and pagination for mission lists
- Admin endpoints for mission and user management

### Getting Started
1. **Install .NET SDK:**
   - [.NET 8 SDK or later](https://dotnet.microsoft.com/download)
2. **Configure the database:**
   - Update `appsettings.json` with your PostgreSQL (or other RDBMS) connection string.
3. **Apply migrations:**
   ```sh
   dotnet ef database update
   ```
4. **Run the API:**
   ```sh
   dotnet run
   ```
   The API will be available at `http://localhost:5003` by default.

---

## Database Schema
- `User`, `UserDetail`, `UserSkills`
- `Mission`, `MissionApplication`, `MissionSkill`, `MissionTheme`
- `Country`, `City`

See the `/Migrations` folder or your SQL scripts for full schema details.

---

## API Endpoints (Sample)

### Authentication & User
- `POST /api/auth/register` — Register a new user
- `POST /api/auth/login` — User login
- `GET /api/user/{id}` — Get user by ID
- `POST /api/user/update-profile` — Update user profile

### Missions
- `GET /api/mission` — List all missions
- `POST /api/mission/client-list` — List missions with sorting/filtering
- `POST /api/mission/apply` — Apply for a mission

### Skills & Themes
- `GET /api/skills` — List all skills
- `GET /api/themes` — List all themes

### Countries & Cities
- `GET /api/countries` — List all countries
- `GET /api/cities/{countryId}` — List cities by country

---

## Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

---

## License
This project is licensed under the MIT License.

---

**For any questions or support, please contact the project maintainer.**
