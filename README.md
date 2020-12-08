# ⚡ ChatClient 💬

<!-- Badges -->
![Back-End](https://github.com/AndriWandres/ChatClient/workflows/.NET%20Core/badge.svg?branch=master)
![Front-End](https://github.com/AndriWandres/ChatClient/workflows/Angular/badge.svg?branch=master)

Instant-Messenger Chat Client that is built ontop of ASP.NET Core 3.1 and Angular 10. It leverages design patterns such as CQRS and Domain-Driven-Design.

## ⚙ Technologies
### 💥 Back-End
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/csharp/csharp.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/dotnet/dotnet.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/96943574ba0c0340ba6ea1e6f768e9abe43e34e1/topics/sql-server/sql-server.png"></code>

The back-end is built ontop of the .NET Core ecosystem and serves as a web API with a purely RESTful design. It utilizes design patterns such as CQRS and Domain-Driven-Design. For realizing chat-like functionality, it also uses web sockets for Server-To-Client communication. The database provider of choice is SQL Server.
* ASP.NET Core 3.1 (RESTful Web API)
* SignalR (Web Socket)
* MediatR (CQRS)
* Swagger (Open API)
* SQL Server (Database)
* xUnit (Testing)

### 💥 Front-End
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/angular/angular.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/typescript/typescript.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/96943574ba0c0340ba6ea1e6f768e9abe43e34e1/topics/javascript/javascript.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/96943574ba0c0340ba6ea1e6f768e9abe43e34e1/topics/sass/sass.png"></code>

The front-end is built as a Single-Page-Application using the Angular Framework from Google. It uses a state management solution similar to Redux for persisting local state. The design pattern follows mostly Google's material design, with a few exceptions here and there.
* Angular 10
* TypeScript
* NGRX (State Management)
* Jasmine (Testing)


## 🎯 Usage
