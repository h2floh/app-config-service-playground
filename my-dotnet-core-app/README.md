# My .NET Core Application

This project is a .NET Core application that connects to the Azure Configuration Service to access feature flags. It is structured to provide a clear separation of concerns with controllers, models, and services.

## Project Structure

- **Controllers**: Contains the `HomeController` which handles HTTP requests.
- **Models**: Contains the `FeatureFlag` model representing feature flags.
- **Services**: Contains the `FeatureFlagService` which interacts with the Azure Configuration Service.
- **Configuration**: The `appsettings.json` file holds configuration settings.
- **Entry Point**: The application starts in `Program.cs` and is configured in `Startup.cs`.

## Features

- Access feature flags from Azure Configuration Service.
- Simple and clean architecture.
- Easy to extend and maintain.

## Getting Started

1. Clone the repository.
2. Open the project in your preferred IDE.
3. Restore the dependencies using `dotnet restore`.
4. Run the application using `dotnet run`.

## Development

This project includes a development container configuration for a consistent development environment. 

### Dev Container

- The `.devcontainer` folder contains the configuration files for setting up the development environment using Docker.

## License

This project is licensed under the MIT License.