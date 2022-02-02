# OriginFinancial.Assignment
This is the backend take home assignment solution for a hiring process from Origin.

## Instructions to run the code
### Installing .NET Core 6
This assigment was developed using Microsoft .NET 6. Therefore it's necessary to download and install the .NET 6 SDK. This can be done using this [link](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

### Running the application
Once the .NET 6 SDK is installed, it's necessary to go to the API project path using a terminal: 

    ../src/ScoreCalculationEngine/ScoreCalculationEngine.Api

and run the following commands:

    dotnet restore
    dotnet build
    dotnet run
    
 Now the API is running on:
 
    https://localhost:7058
    
This API has a documentation page created using OpenAPI Specification ([link](https://swagger.io/specification/)). This is the documentation page's URL:

    https://localhost:7058/swagger/index.html
    
This documentation page contains useful information about the API and a good description about its endpoints and DTOs schemas: 

![image](https://user-images.githubusercontent.com/31359544/152155167-a5946ad2-5227-45a1-80c9-5cc2336dac8f.png)

On this doc page is possible to make a post to the endpoint. To do this, click on the endpoint's collapse and click on the ``` Try it out ``` button. Then put your payload inside the request body input area and click on the ``` Execute ``` button. The request's response can be seen right below the ``` Execute ``` button:

![vivaldi_gUp03i9u52](https://user-images.githubusercontent.com/31359544/152157469-f1804a32-827d-4646-9f61-57650a86d24e.gif)

It's also possible to make a request to the endpoint using a API client (Postman or Insomnia). For this, create a POST request with this URL:

    https://localhost:7058/ScoreCalculation/getRiskProfile

and pass the payload on the request's body.

## Main technical decisions
### Solution's framework
I decided to develop this solution using .NET 6 because I have been working with this framework for the last seven years.

### Solution's architecture
I decided to develop this solution using a Hexagonal architecture/Clean architecture approach. I have been using these concepts for a quite long time and I think this is a very good approach either for small or big projects. The layers defined by the Clean architecture are very helpful to keep the solution well decoupled and with great level of maintainability and extensibility. In addition, the Hexagonal architecture allows you to decouple external dependencies in ports and adapters. This gives you more flexibility to update or even change these dependencies in the future and helps a lot in unit testing as it is easier to mock dependencies behaviour.

This is the solution's structure. I like to divide the solution into folders to keep the projects ordered according to their layers:

![image](https://user-images.githubusercontent.com/31359544/152162386-b8e90d99-ed0a-4e24-96b5-a2e4af767d85.png)

#### Domain
The domain project should contains all definitions related to the scope. Models, entities, services, core business rules, exceptions and many more. All things placed on the domain project can be used across many applications, because they define scope concepts and not applications specific behaviour. 

The domain project must not depend on any other project.

#### Application
The application project should contains all definitions related to the application. Models, DTOs, services, business rules related to this application and many more. The things defined here cannot be used by different applications because they are strictly related to the application in context.

Here we can add the adapter's ports (interfaces) used by the application (data access adapter, cache addapter, logging adapter, etc.).

This Application project should depend on the Domain project.

#### Infra
Here we can have many projects. This projects are adapters implementations and should define the external depencies. Some examples are Data Access Adapter, Cache Adapter, Logging Adapter and many others. 

On this solution I created the Authentication Adapter because I had the intention to publish this API to a cloud server (Heroku probably). Therefore, it would be good to add an API Key authentication to give some level of security to the deployed API. But due the deadline, I was not able to do the deployment, but I decided to keep the adapter on the solution to give more view about the Hexagonal architecure approach. 

On good thing about this approach is that is pretty simple to disable/remove the auth adapter or change to a different type of authentication.

#### Presentation
This is where the API project is placed. It is the interface to the outside word. This is a .NET 6 Web API project, which enables creating a good, simple and fast web API.

This API project should depend on the Application project and on the adapter projects (for dependency injection configuration only).

#### Tests
Here we can have any type of automated tests to test the solution. I created some unit tests for the Application project only. Since this is a small application, the business rules are place there and there is no integration to external dependencies (no need for integrations tests).



