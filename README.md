# TaskHub - Task Management Web Application

TaskHub is a robust and scalable web application designed to help users manage their tasks efficiently. The application follows the Designed architecture with the MVC pattern, ensuring a clear separation of concerns and scalability. It is built using ASP.NET Core 7.0 for developing robust web APIs and leverages Entity Framework (ORM) for seamless database integration.

## Features

- User Management: Create, update, and delete user profiles.
- Project Management: Organize tasks into projects for better categorization.
- Task Tracking: Easily create, assign, and track tasks within projects.
- Comment System: Foster collaboration by adding comments to tasks and projects.

## Architecture Overview

The application follows a Designed architecture with the MVC (Model-View-Controller) pattern, ensuring a modular and maintainable codebase. The separation of concerns allows for easier development, testing, and scalability of the application.

### Technologies Used

- **ASP.NET Core 7.0:** Used for building robust and high-performance web APIs.
- **Entity Framework (ORM):** Seamless integration with the database for efficient data management.
- **Microsoft SQL Server:** Chosen as the Database Management System for its simplicity and efficiency.

## Getting Started

To get started with TaskHub, follow these steps:

### Prerequisites

- Install [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) on your machine.
- Ensure you have Microsoft SQL Server installed.

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/WinnieNgina/TaskHub
   ```

2. Navigate to the project directory:

   ```bash
   cd taskhub
   ```

3. Update the database connection string in the `appsettings.json` file:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskHubDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. Run the database migrations to create the necessary tables:

   ```bash
   dotnet ef database update
   ```

5. Run the application:

   ```bash
   dotnet run
   ```

6. Open your browser and navigate to `https://localhost:5001` to access TaskHub.

## Models

### User

- Represents a user of the TaskHub application.
- Properties: UserId, Username, Email, Password, etc.

### Project

- Represents a project within TaskHub.
- Properties: ProjectId, ProjectName, Description, etc.

### ProjectTasks

- Represents tasks associated with a specific project.
- Properties: TaskId, TaskName, Description, Assignee, Status, etc.

### Comments

- Represents comments associated with tasks or projects.
- Properties: CommentId, TaskId (or ProjectId), UserId, Content, Timestamp, etc.

## Contributing

Feel free to contribute to TaskHub by opening issues or creating pull requests. 

## Acknowledgments

- Special thanks to the contributors and open-source community for their valuable contributions.

