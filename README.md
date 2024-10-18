# Fruit Catalog API - Onion Architecture
A Dotnet Core 2.2 simple API created using TDD and Onion Architecture principles
This CRUD API manages fruits and fruit types using Swagger API

For more detailed information about the request flow of this architecture, check out our [Onion Architecture Documentation](./docs/Onion.md).


## Description
This project was created for studies purposes, testing tools, practice some programming concepts and also serve as a reference for other projects.


## Technologies and Tools

- [**FluentValidation**](https://fluentvalidation.net/): For validating domain objects.
- [**LINQ**](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/): For querying collections.
- [**Entity Framework Core 2.2**](https://docs.microsoft.com/en-us/ef/core/): For data access.

## Design Principles

- [**SOLID Principles**](https://en.wikipedia.org/wiki/SOLID): For maintainable and scalable architecture.
- [**Clean Code**](https://amzn.to/3xLdpwE): A handbook of agile software craftsmanship by Robert C. Martin.
- [**Clean Architecture**](https://amzn.to/3Ek7ecB): A guide to building software architectures that are both maintainable and adaptable.
- [**Onion Architecture**](https://www.clarity-ventures.com/articles/onion-based-software-architecture): A software design pattern that emphasizes the separation of concerns.



## Tests
- [**TDD**](https://en.wikipedia.org/wiki/Test-driven_development): Test-Driven Development.
- [**Moq**](https://github.com/moq/moq4): For mocking dependencies in unit tests.
- [**NUnit**](https://nunit.org/): For writing unit and integration tests.
- [**Unit Tests**](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test): Automated tests that verify individual units of code.
- [**Integration Tests**](https://docs.microsoft.com/en-us/dotnet/core/testing/integration-testing): Automated tests that verify interactions between different pieces of the application.

## Getting Started

### Prerequisites

- .NET Core 2.2 SDK or later
- (optional) Postgres if you don't want to use InMemory database.

### Setup and Run

1. **Clone the Repository**:

    ```bash
    git clone https://github.com/YourUsername/YourProjectName.git
    cd YourProjectName
    ```

2. **Build the Project**:

    ```bash
    dotnet build
    ```

3. **Set up the Database**:
   
   If you’re using a database like SQL Server for production, configure your connection string in `appsettings.json`:
   
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"
      }
    }
    ```

4. **Run Database Migrations** (if applicable):

    ```bash
    dotnet ef database update
    ```

5. **Run the Application**:

    ```bash
    dotnet run --project API/YourProjectName.API.csproj
    ```

6. **Run Unit Tests**:

    ```bash
    dotnet test Tests/UnitTests/YourProjectName.UnitTests.csproj
    ```

7. **Run Integration Tests**:

    ```bash
    dotnet test Tests/IntegrationTests/YourProjectName.IntegrationTests.csproj
    ```

8. **Swagger UI**:

    Navigate to `http://localhost:5000` (or the port your application is running on) to see the Swagger UI. This will give you a user-friendly interface to interact with your API and see the documentation.


### Contributing

1. Create an Issue.
2. Fork the repository.
3. Create a new branch.
4. Make your changes.
5. Submit a pull request.

### License
This project is licensed under the MIT License.

Happy coding! 🚀