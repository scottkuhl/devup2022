# Blazor Starter Application

This template contains an example .NET 6 [Blazor WebAssembly](https://docs.microsoft.com/aspnet/core/blazor/?view=aspnetcore-6.0#blazor-webassembly) client application, a .NET 6 C# [Azure Functions](https://docs.microsoft.com/azure/azure-functions/functions-overview), and a C# class library with shared code.

## Getting Started

*Optional: Setup Windows Terminal to close the process exits, fails or crashes instead of closing only when the process exits successfully.
This will close the Azure Functions window when you stop debugging.*

### Visual Studio 2022

1. Setup your workstation to develop with **[Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)**.  You will also need the **.NET WebAssembly build tools** optional component.

1. Add the **Azure development workload** to Visual Studio.

1. Install the **[Azure Static Web Apps CLI](https://techcommunity.microsoft.com/t5/apps-on-azure-blog/introducing-the-azure-static-web-apps-cli/ba-p/2257581)**.

1. Install the **[Azure Cosmos DB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator)**.  This will launch automatically if it is not already running when the project is opened using the Command Task Runner extension.

1. Install the **[Markdown Editor](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor64)** extension.  You can use this to preview markdown files like this one.

1. Install the **[Command Task Runner (64-bit)](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CommandTaskRunner64)** extension.

Once you clone the project, open the solution in [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) and follow these steps:

1. Right-click on the solution and select **Set Startup Projects...**.

1. Select **Multiple startup projects** and set the following actions for each project:
    - *Api* - **Start**
    - *Client* - **Start**
    - *Shared* - None

1. Press **F5** to launch both the client application and the Functions API app.

### Visual Studio Code with Azure Static Web Apps CLI

1. Install the [Azure Static Web Apps CLI](https://www.npmjs.com/package/@azure/static-web-apps-cli) and [Azure Functions Core Tools CLI](https://www.npmjs.com/package/azure-functions-core-tools).

1. Open the folder in Visual Studio Code.

1. In the VS Code terminal, run the following command to start the Static Web Apps CLI, along with the Blazor WebAssembly client application and the Functions API app:

    ```bash
    swa start http://localhost:5000 --run "dotnet run --project Client/Client.csproj" --api-location Api
    ```

    The Static Web Apps CLI (`swa`) first starts the Blazor WebAssembly client application and connects to it at port 5000, and then starts the Functions API app.

1. Open a browser and navigate to the Static Web Apps CLI's address at `http://localhost:4280`. You'll be able to access both the client application and the Functions API app in this single address. When you navigate to the "Fetch Data" page, you'll see the data returned by the Functions API app.

1. Enter Ctrl-C to stop the Static Web Apps CLI.

## Template Structure

- **Client**: The Blazor WebAssembly sample application
- **Api**: A C# Azure Functions API, which the Blazor application will call
- **Shared**: A C# class library with a shared data model between the Blazor and Functions application

### Api

- **Data**: Cosmos DB is used to store data.
A common repository is setup for shared logic.
Bogus will seed test data in the local environment on startup.

- **Exceptions**: Common custom exceptions are available.
Functions inherit from a base Function class which implements global exception handling.

- **Uploads**: File uploads are stored in Azure Storage.
The local Azurite emulator is installed with Visual Studio 2022 and the API project is configured to run it.

### Shared

- **Exceptions**: When exceptions happen in the API layer, a custom ErrorDetails class is returned.

- **Models**: Models inherit from a common base Model class.

- **Services**: A date / time and guid service are available to make automated testing easier.

## Deploy to Azure Static Web Apps

This application can be deployed to [Azure Static Web Apps](https://docs.microsoft.com/azure/static-web-apps), to learn how, check out [our quickstart guide](https://aka.ms/blazor-swa/quickstart).

## Reference

### Articles

- **[Deploying Blazor WebAssembly into Azure Static Web Apps](https://code-maze.com/deploying-blazor-webassembly-into-azure-static-web-apps/)**: In this article, we are going to learn about Azure Static Web Apps, which is a great solution for publishing static web applications into the cloud.

- **[Azure Static Web Apps, Blazor, Authentication and Visual Studio 2022](https://scottkuhl.medium.com/azure-static-web-apps-blazor-authentication-and-visual-studio-2022-40364cc543b7)**: Azure Static Web Apps is a very easy way to create a secured Blazor Web Assembly application with a backend API based on Azure Functions. I will walk you through getting this all setup and running on your local workstation using Visual Studio 2022.

- **[Azure Cosmos DB with ASP.NET Core Web API](https://code-maze.com/azure-cosmos-db-with-asp-net-core-web-api/)**: We値l start by learning what an Azure Cosmos DB is and the various APIs that it supports. Then we値l create a Cosmos DB from the portal. After that, we値l learn how to create an ASP.NET Core application that connects with the Cosmos DB using the Core (SQL) API. First, we値l test it locally and then deploy it to Azure. Finally, we値l verify that everything works well together in the cloud environment.

### Documentation

- **[Azure Static Web Apps](https://docs.microsoft.com/en-us/azure/static-web-apps/)**: Azure Static Web Apps allows you to build modern web applications that automatically publish to the web as your code changes.

- **[Blazor WebAssembly](https://docs.microsoft.com/aspnet/core/blazor/)**: Blazor is a framework for building interactive client-side web UI with .NET.

- **[Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/)**: Fast NoSQL database with SLA-backed speed and availability, automatic and instant scalability, and open-source APIs for MongoDB and Cassandra.