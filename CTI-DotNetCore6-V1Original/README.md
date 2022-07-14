# ASP<span>.</span>NET Core Web App Solution Template
A multi-project solution template for creating an ASP<span>.</span>NET Core application with example content using current standards 
and best practices. It is based on clean architecture and includes essential packages. The solution contains a Web App
and a Web API project.

## Features
### Common
- .NET 6, C# 10
- Nullable reference types
- Serilog w/ HTTP Context Enrichers and Seq sink
- Application monitoring via Application Insights
- Liveness and readiness probes using health checks
- CQRS implementation using Mediatr library
- Fluent validation
- Pagination using X.PagedList
- C# functional programming extensions using language-ext library
- OpenID Connect server and token validation using OpenIddict library
- Authorization via role claims
- Multi-tenant
- Base classes
- Paged Request/Response
- Switch to use in-memory DB for development
- Helper/utility classes
- Audit trail
- Feature folders
- Sample README file
### Web
- AdminLTE 3.2 Bootstrap template with dark mode
- ASP<span>.</span>NET Core Identity with roles and permissions
- Localization support
- Admin UI
- Base PageModel
- Areas to organize functionality
- Datatables Javascript library
- Show release name and build number
- Configurable banner color which can be used to configure different color per environment
- Google and MS authentication
- Helper/utility Javascript functions
- LibMan to manage client-side libraries
### Web API
- API documentation with Swagger
- Base Controller
- API versioning
- Default API conventions 
- Error handler
- Thin Controllers
- JWT authentication and claims-based authorization
- API metadata endpoint
- Standard error responses
### Security
- Recommended security headers
- Log successful and invalid login attempts
- GDPR support
- Strong passwords
- Passwords are encrypted in db
- Secure cookies
- HTTPS required
- Anti-forgery tokens
- Audit and application logs
- Hide sensitive info in logs

## Getting started
### Prerequisites
- .NET 6 SDK or above
- Visual Studio 2022 or above

### Using the template

1. [Optional] Generate a self-signed certificate and store in the local machine
    - You can generate a self-signed certificate using the self-cert utility you can download from this [site](https://www.pluralsight.com/blog/software-development/selfcert-create-a-self-signed-certificate-interactively-gui-or-programmatically-in-net).
    - Refer to this [link](https://improveandrepeat.com/2018/12/how-to-fix-the-keyset-does-not-exist-cryptographicexception) if you encounter "Keyset does not exist" error

2. [Optional] Set up external login providers
    - [Google](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins) instructions
    - [Microsoft](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins) instructions

3. Open the solution in Visual Studio. After the packages are restored, update the configuration in appsettings.Development.json for both the Web API and Web projects.

    Web API:

    ```json
    {
      "Serilog": {
        "MinimumLevel": {
          "Default": "Information",
          "Override": {
            "Microsoft.AspNetCore": "Warning"
          }
        },
        "WriteTo": [
          { "Name": "Console" },
          {
            "Name": "Seq",
            "Args": {
              "serverUrl": "",
              "apiKey": ""
            }
          }
        ]
      },
      "ApplicationInsights": {
        "InstrumentationKey": ""
      },
      "UseInMemoryDatabase": false
    }
    ```

    Web:

    ```json
    {
      "DetailedErrors": true,
      "Serilog": {
        "MinimumLevel": {
          "Default": "Information",
          "Override": {
            "Microsoft.AspNetCore": "Warning"
          }
        },
        "WriteTo": [
          { "Name": "Console" },
          {
            "Name": "Seq",
            "Args": {
              "serverUrl": "",
              "apiKey": ""
            }
          }
        ]
      },
      "ApplicationInsights": {
        "InstrumentationKey": ""
      },
      "DefaultPassword": "",
      "DefaultClient": {
        "ClientId": "",
        "ClientSecret": ""
      },
      "SslThumbprint": "",
      "UseInMemoryDatabase": false,
      "NavbarColor": "orange",
      "SmtpSettings": {
        "Host": "",
        "Port": 587,
        "Email": "",
        "Password": ""
      },
      "Authentication": {
        "Microsoft": {
          "ClientId": "",
          "ClientSecret": ""
        },
        "Google": {
          "ClientId": "",
          "ClientSecret": ""
        }
      }
    }
    ```

4. Open the Package Manager Console and apply the EF Core migrations

    ```powershell
	Add-Migraation -Context ApplicationContext InitialDatabaseStructure
    Update-Database -Context IdentityContext
    Update-Database -Context ApplicationContext
    ```

5. Out of the box, the application assumes that the following URLs are configured
    - Web: https://localhost:5001
    - Web API: https://localhost:44379  

      To configure Visual Studio to use the above URLs, edit *launchSettings.json* for the Web API and Web projects.

      Web API
      ```
      {
        "profiles": {
          "MyApp.API": {
            "commandName": "Project",
            "launchBrowser": true,
            "launchUrl": "swagger",
            "environmentVariables": {
              "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "applicationUrl": "https://localhost:44379;http://localhost:44378"
          }
        }
      }    
      ```

      Web
      ```
      {
        "profiles": {
          "MyApp.Web": {
            "commandName": "Project",
            "launchBrowser": true,
            "environmentVariables": {
              "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "applicationUrl": "https://localhost:5001;http://localhost:5000"
          }
        }
      }
      ```

      Or you can configure the automatically generated ports in the *appsettings.json* of the Web API project and the Web Admin UI.

      appsettings.json
      ```
      "Authentication": {
        "Issuer": "https://localhost:5001/",
        "Audience": "https://localhost:44379"
      }
      ```

      Admin UI
      ![Admin UI](https://dev.azure.com/fai-dev-team/5d14b026-fdfb-4687-b8c9-85758d482332/_apis/git/repositories/69898df6-0594-4c8c-9185-acfac62ebf46/items?path=/docs/images/demo_api.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0)

1. Build and run your application.

### Default web credentials

User: system@admin  
Password: &lt;set in appsettings.json&gt;

### Generating access tokens

The Web project implements an OpenID Connect server and token authentication using the OpenIddict library. It supports 
authorization code flow, device authorization flow, client credentials and password grant.

Download the Postman collection below for samples of how to generate access tokens using the supported flows. You can
use the generated access token to authenticate API requests.

### API
This is the Web API project. It is an ASP<span>.</span>NET Core application with an example Controller for a RESTful HTTP 
service. It uses claims-based authorization to secure the endpoints.

### Application
This project contains business logic codes that is meant to be shared, e.g. between Web and Web API projects.

### Core
This project contains the domain models. If using a functional programming approach, domain models should be immutable
records. Business logic codes should be placed in static classes. It is done this way to separate data and logic in
accordance with the functional programming approach.

### Infrastructure
This project contains code for communicating with external sources such as databases or external services.

### Web
This is the Web project. It implements ASP<span>.</span>NET Core Identity and uses Areas to organize functionality. It
also implements an OpenID server and token authentication using the OpenIddict library. It uses claims-based authorization
to secure the pages.
