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

### Books & Video Series

- **[Microsoft Blazor: Building Web Applications in .NET 6 and Beyond](https://learning.oreilly.com/library/view/microsoft-blazor-building/9781484278451/)**: Reading this book helps you learn to build user interfaces and present data to a user for display and modification, capturing the user痴 changes via data binding. The book shows how to access a rich library of .NET functionality such as a component model for building a composable user interface, including how to develop reusable components that can be used across many pages and websites. Also covered is data exchange with a server using REST, SignalR, and gRPC, giving you access to microservices and database services.

- **[Blazor WebAssembly - A Practical Guide to Full Stack Development With .NET](https://code-maze.com/blazor-webassembly-course/)**: The lessons start with the basic concepts of building a simple but practical application with Blazor WebAssembly. We cover all the things we need in order to build a real-world application and we quickly transition to building full-fledged application parts, slowly introducing the concepts as we go.

- **[Guide to NoSQL with Azure Cosmos DB](https://www.packtpub.com/product/guide-to-nosql-with-azure-cosmos-db/9781789612899)**: By the end of the book, you will be able to build an application that works with a Cosmos DB NoSQL document database with C#, the .NET Core SDK, LINQ, and JSON.

- **[Blazor Train](https://blazortrain.com/)**: Blazor Train is an extensive class on Microsoft Blazor, an extremely productive technology for developing web applications using HTML markup and the C# programming language.

### Articles

- **[Deploying Blazor WebAssembly into Azure Static Web Apps](https://code-maze.com/deploying-blazor-webassembly-into-azure-static-web-apps/)**: In this article, we are going to learn about Azure Static Web Apps, which is a great solution for publishing static web applications into the cloud.

- **[Azure Static Web Apps, Blazor, Authentication and Visual Studio 2022](https://scottkuhl.medium.com/azure-static-web-apps-blazor-authentication-and-visual-studio-2022-40364cc543b7)**: Azure Static Web Apps is a very easy way to create a secured Blazor Web Assembly application with a backend API based on Azure Functions. I will walk you through getting this all setup and running on your local workstation using Visual Studio 2022.

- **[Azure Cosmos DB with ASP.NET Core Web API](https://code-maze.com/azure-cosmos-db-with-asp-net-core-web-api/)**: We値l start by learning what an Azure Cosmos DB is and the various APIs that it supports. Then we値l create a Cosmos DB from the portal. After that, we値l learn how to create an ASP.NET Core application that connects with the Cosmos DB using the Core (SQL) API. First, we値l test it locally and then deploy it to Azure. Finally, we値l verify that everything works well together in the cloud environment.

- **[Azure BLOB Storage with ASP.NET Core and Angular](https://code-maze.com/azure-blob-storage-with-asp-net-core-and-angular/)**: In this article, we are going to take a look at the Azure Storage Platform and learn how to work with Azure BLOB Storage.

- **[30 Days of SWA](https://www.azurestaticwebapps.dev/blog)**: Simply put, it's a month-long series of blog posts that provides you a curated and structured tour through the universe of Azure Static Web Apps.

### Documentation

- **[Azure Static Web Apps](https://docs.microsoft.com/en-us/azure/static-web-apps/)**: Azure Static Web Apps allows you to build modern web applications that automatically publish to the web as your code changes.

- **[Blazor WebAssembly](https://docs.microsoft.com/aspnet/core/blazor/)**: Blazor is a framework for building interactive client-side web UI with .NET.

- **[Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/)**: Fast NoSQL database with SLA-backed speed and availability, automatic and instant scalability, and open-source APIs for MongoDB and Cassandra.

- **[Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/)**: Azure Functions is a cloud service available on-demand that provides all the continually updated infrastructure and resources needed to run your applications. You focus on the pieces of code that matter most to you, and Functions handles the rest. Functions provides serverless compute for Azure. You can use Functions to build web APIs, respond to database changes, process IoT streams, manage message queues, and more.

- **[Azure Blob Storage](https://docs.microsoft.com/en-us/azure/storage/blobs/)**: Azure Blob Storage is Microsoft's object storage solution for the cloud. Blob storage is optimized for storing massive amounts of unstructured data.

### Other

- **[Azure Updates](https://azure.microsoft.com/en-gb/updates/?query=Static%20Web%20App)**: Keep up to date with the latest changes in Azure Static Web Apps.

- **[See Static Web Apps In Action](https://nitya.github.io/static-web-apps-gallery-code-samples/showcase)**: A community-contributed app showcase to learn from!

- **[Static Web App Dev Community](https://dev.to/t/staticwebapps)**: Static Web App blog posts from the Azure team.