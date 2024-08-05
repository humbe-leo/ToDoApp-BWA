# ToDoApp

Welcome to the ToDoApp repository! This application consists of two main projects:

1. **ToDoApp.Api**: An ASP.NET Core API project that handles the backend logic.
2. **ToDoApp.Client**: A Blazor WebAssembly project that serves as the frontend.

Follow the instructions below to set up and run the application.

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/humbe-leo/ToDoApp-BWA.git
cd ToDoApp-BWA
```
### 2. Backend Setup (ToDoApp.Api)

1. **Navigate to ToDoApp.Api project:**
- Open the `appsettings.json` file located in the `ToDoApp.Api` project.
- Find the `DefaultConnection` value under the `ConnectionStrings` section.
- Replace the existing connection string with your SQL Server connection details.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=ToDoAppDB;User Id=your_user;Password=your_password;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```
2. **Run Entity Framework Migrations:**
- Open a terminal in the `ToDoApp.Api` project directory and run the following command to create the database:
```bash
dotnet ef database update
```
> **Tip:** If you're using Visual Studio Package Manager Console, make sure to set the Default project to `ToDoApp.Api` before running `Update-Database`.

### 3. Frontend Setup (ToDoApp.Client)
1. Open the `ToDoApp.Client` project.
2. Navigate to the `wwwroot` folder and open the `appsettings.json` file.
3. Update the `ApiUrl` value with the URL where your API is running:

```json
{
  "ApiUrl": "https://localhost:5001"
}
```
> **Note:** The actual URL may vary depending on your local setup. Make sure it matches the URL where your `ToDoApp.Api` project is running.

### 4. Running the Application
#### Using Visual Studio 2022:
1. Right-click on the solution in Solution Explorer.
2. Select "Set Startup Projects".
3. Choose "Multiple startup projects".
4. Set both `ToDoApp.Api` and `ToDoApp.Client` to "Start".
5. Click "Apply" and "OK".
6. Press F5 or click the "Start" button to run both projects simultaneously.
#### Using Visual Studio Code:
1. Open the root folder of the project in VS Code.
2. Create a `.vscode/launch.json` file with the following content:
```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Launch API",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/ToDoApp.Api/bin/Debug/net7.0/ToDoApp.Api.dll",
      "args": [],
      "cwd": "${workspaceFolder}/ToDoApp.Api",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    {
      "name": "Launch Client",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/ToDoApp.Client/bin/Debug/net7.0/ToDoApp.Client.dll",
      "args": [],
      "cwd": "${workspaceFolder}/ToDoApp.Client",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  ],
  "compounds": [
    {
      "name": "API + Client",
      "configurations": ["Launch API", "Launch Client"]
    }
  ]
}
```
3. Press F5 or go to the Run and Debug view and select "API + Client" to run both projects simultaneously.

## Additional Tips
- Make sure both projects are using the same port numbers consistently across the configuration files.
- If you encounter any CORS issues, ensure that the API project has the correct CORS configuration to allow requests from the client application.
- For production deployment, remember to update the connection strings and API URLs accordingly.
- It's recommended to use environment variables or user secrets for storing sensitive information like connection strings in a production environment.
## Troubleshooting
If you encounter any issues while setting up or running the application, please check the following:
1. Ensure all NuGet packages are restored for both projects.
2. Verify that the SQL Server instance is running and accessible.
3. Check if the ports specified in the project settings are not being used by other applications.
4. Make sure you have the latest .NET 7.0 SDK installed.


If problems persist, please open an issue in this repository with a detailed description of the error and the steps to reproduce it.

## Contributing
Feel free to submit issues or pull requests if you find bugs or have suggestions for improvements.

## License
This project is licensed under the MIT License - see the [LICENSE](https://github.com/git/git-scm.com/blob/main/MIT-LICENSE.txt) file for details.

Happy coding!