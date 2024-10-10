# StackNServe

StackNServe is an interactive game developed as a course project for CS455: Introduction to Software Engineering during the 2024-25 Semester I, under the guidance of [Prof. Sruti Ragavan](https://sruti-s-ragavan.github.io).

The application is designed to evaluate players' memory, time management, and risk assessment capabilities through an engaging gameplay experience. StackNServe features a user-friendly interface, ensuring accessibility and a smooth user experience.

## Development Team

This project was developed by : [Aditi Khandelia (220061)](https://github.com/AditiKhandelia) and [Kushagra Srivastava (220573)](https://github.com/whizdor).

## Deployment

The web app is deployed at: https://cs455-assignment-1.github.io/StackNServe/

## How to run the software locally?

* Make sure you have .NET 8 SDK and C# Development Enviroment installed in your system.

Clone the repository-

```bash
gh repo clone CS455-Assignment-1/StackNServe
```

### [Terminal 1]

Navigate to the project directory:

```bash
cd StackNServe
cd client
```
Launch the application using the .NET CLI:
```bash
dotnet run
```

### [Terminal 2]

Navigate to the project directory:

```bash
cd StackNServe
cd Server
```
Launch the application using the .NET CLI:
```bash
python server.py
```
The application will start and be accessible via your local host at port 5000, with the server at port 8000.

For further information or to report issues, please refer to the project's GitHub repository.

## Code Quality Metrics

In order to keep a check on code quality two tools are used 

### 1. SonarCloud
SonarCloud is integrated to perform static code analysis and continuously assess the overall quality of the codebase. Key metrics tracked include:

- **Code Smells** : Bad practices are identified and flagged.
- **Reliability Rating** : Measures the likelihood of the code to produce bugs or unexpected behaviors.
- **Maintainability Rating** : It tracks how easy it is to modify and extend the code.
- **Code Coverage** : Test coverage is monitored to ensure that the majority of the codebase is covered by unit tests.

The parameters at which the Quality Gate fails is
```
[New Code]
- Coverage < 50%
- Duplicated Lines > 3%
- Maintainability Rating < A
- Reliability Rating < A

[Old Code]
- Coverage < 50%
- Duplicated Lines > 3%
```

The Static Report that is generated can be accessed at the link : [SonarCloud](https://sonarcloud.io/project/overview?id=CS455-Assignment-1_StackNServe)

### 2. dotnet format Linter
We use the `dotnet format` linter to enforce consistent code style and formatting across the codebase. 

- Adherence to the project's predefined coding standards.
- Automated formatting of files for consistency.
- Detection of minor issues such as incorrect indentation, unused usings, or code structure deviations.

```bash
dotnet format src/StackNServe/StackNServe.csproj --verify-no-changes
```

Both of these tools are run in the CI/CD Pipeline, on GitHub Actions. 

## Tests and Coverage

### How to Run Tests Locally

To execute the test suite for StackNServe (client):

```bash
dotnet tests/client.Tests/StackNServe.Tests.csproj
```

To execute the test suite for StackNServe (server):

```bash
pytest tests/server.Tests
```

### Mocking with Moq

Where necessary, we mock external dependencies using Moq to ensure our unit tests focus only on the functionality of the component under test. For example, services like GlobalStringListService and SelectionButtonService are mocked to isolate their behaviors.

### Code Coverage
We utilize Coverlet to track code coverage for our test cases. The generated reports provide insights into how much of the codebase is covered by unit tests, helping us identify any untested paths or potential blind spots.

We aim for at least 50% coverage for both new and old code.

a. Client
```bash
dotnet dotcover test tests/client.Tests/StackNServe.Tests.csproj --dcReportType=HTML
```

b. Server
```bash
coverage run --source=server -m pytest tests/server.Tests
coverage xml -o coverage.xml
```

## Continuous Integration (CI) with GitHub Actions
The tests are automatically executed in the CI/CD pipeline as part of the GitHub Actions workflow. Each commit triggers the following actions:

- Build the project.
- Run the tests to ensure nothing is broken.
- Runs Code Quality Tests.
- Generate a code coverage report.

## Reports
Detailed Code Analysis Reports can be found in the `reports` folder

- Report on Date of Submission : `report/Report_Homework3.pdf`
- Report at the beginning : `report/Report_HomeWork1.pdf`

The results are also available at the Actions/Workflow Dashboard.
