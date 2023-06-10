# Running the C# Application

## Prerequisites
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/) (latest stable version)
- [PostgreSQL](https://www.postgresql.org/) (latest stable version)
- [.NET Framework or .NET Core](https://dotnet.microsoft.com/) (latest stable version)

## Steps
1. Open Microsoft Visual Studio, a powerful integrated development environment (IDE) for C#.
2. Click on the "Open a project" or "Open a solution" option, depending on your Visual Studio version.
3. Navigate to the directory where your C# project is located and select the project/solution file with the `.sln` extension.
4. Launch PostgreSQL, a robust and feature-rich open-source relational database management system.
5. Create a new database within PostgreSQL by right-clicking on the Databases folder and selecting the "New Database" option. Provide a suitable name for the database and confirm the creation.
6. Switch back to Visual Studio and locate the configuration file (e.g., `appsettings.json` or `web.config`) within your C# project.
7. Update the connection string in the configuration file to reflect the necessary details for your PostgreSQL server, including server address(`localhost`), port(`5432`), database name(`SalaFitness1`), username(`postgres`), and password(`facultatetheobrasov2022`).
8. Build the C# project by selecting the "Build" menu and choosing the "Build Solution" option or by pressing `Ctrl + Shift + B`.
9. Run the application by clicking on the "Debug" menu and selecting the "Start Debugging" option or by pressing `F5`.
10. Once the application is launched, you can interact with its features and functionalities directly within the user interface.

Make sure you have the necessary PostgreSQL drivers and libraries installed for seamless communication between your C# application and the PostgreSQL database.

## Utilized Software Versions
- Microsoft Visual Studio: Latest stable version (e.g., Visual Studio 2022)
- C# programming language: Latest stable version (e.g., C# 9.0)
- PostgreSQL: Latest stable version (e.g., PostgreSQL 15.0)
- .NET Framework or .NET Core: Latest stable version

## Utilized Extension Versions
- Entity Framework: 6.4.0
- Entity Framework Npgsql: 6.4.3
- Npgsql: 4.1.5
