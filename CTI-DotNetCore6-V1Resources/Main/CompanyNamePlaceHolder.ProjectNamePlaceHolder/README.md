# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started

1. [Optional] Generate a self-signed certificate and store in the local machine
    - You can generate a self-signed certificate using the self-cert utility you can download from this [site](https://www.pluralsight.com/blog/software-development/selfcert-create-a-self-signed-certificate-interactively-gui-or-programmatically-in-net).
    - Refer to this [link](https://improveandrepeat.com/2018/12/how-to-fix-the-keyset-does-not-exist-cryptographicexception) if you encounter "Keyset does not exist" error

1. [Optional] Set up external login providers
    - [Google](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins) instructions
    - [Microsoft](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins) instructions

1. Open the solution in Visual Studio. After the packages are restored, update the configuration in appsettings.Development.json for both the Web API and Web projects.

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

1. Open the Package Manager Console and apply the EF Core migrations

    ```powershell
    Update-Database -Context IdentityContext
    Update-Database -Context ApplicationContext
    ```

1. Out of the box, the application assumes that the following URLs are configured
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

# Default web credentials

User: system@admin  
Password: &lt;set in appsettings.json&gt;

# Generating access tokens

The Web project implements an OpenID Connect server and token authentication using the OpenIddict library. It supports 
authorization code flow, device authorization flow, client credentials and password grant.

Download the Postman collection below for samples of how to generate access tokens using the supported flows. You can
use the generated access token to authenticate API requests.

[OpenID Postman Collection](https://dev.azure.com/fai-dev-team/CompanyNamePlaceHolder%20Alabang%20Apps/_git/CompanyNamePlaceHolder.ProjectNamePlaceHolder?version=GBmain&path=%2Fdocs%2FUploads%2FOpenIdDict.postman_collection.json)

# Contribute
You can contribute to the project in 2 ways:
- Submit bugs and feature requests
- Clone the repository, create your feature branch and submit a pull request
