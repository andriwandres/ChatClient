# ⚡ ChatClient 💬

<!-- Badges -->
![Back-End](https://github.com/AndriWandres/ChatClient/workflows/.NET%20Core/badge.svg?branch=master)
![Front-End](https://github.com/AndriWandres/ChatClient/workflows/Angular/badge.svg?branch=master)

<!-- Intro section -->
Instant-Messenger Chat Client that is built ontop of ASP.NET Core 3.1 and Angular 10. It leverages design patterns such as CQRS and Domain-Driven-Design.

<!-- Table of contents -->
## Table of Contents
* [Getting started](https://github.com/AndriWandres/ChatClient#-technologies)
  * [Prerequisites](https://github.com/AndriWandres/ChatClient#-prerequisites)
  * [Installation](https://github.com/AndriWandres/ChatClient#-installation)
  * [Testing](https://github.com/AndriWandres/ChatClient#-testing)
  * [Continuous Integration](https://github.com/AndriWandres/ChatClient#-prerequisites)
* [Technologies](https://github.com/AndriWandres/ChatClient#-technologies)
  * [Back-End](https://github.com/AndriWandres/ChatClient#-back-end)
  * [Front-End](https://github.com/AndriWandres/ChatClient#-front-end)

<br/>

<!-- How to setup application -->
## 🎯 Getting started
Content will follow soon...

### Prerequisites

### Installation

### Testing

### Continuous Integration

<br/>

<!-- Technology listing -->
## ⚙ Technologies
### 💥 Back-End
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/csharp/csharp.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/dotnet/dotnet.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/96943574ba0c0340ba6ea1e6f768e9abe43e34e1/topics/sql-server/sql-server.png"></code>

The back-end is built ontop of the .NET Core ecosystem and serves as a web API with a purely RESTful design. It utilizes design patterns such as CQRS and Domain-Driven-Design. For realizing chat-like functionality, it also uses web sockets for Server-To-Client communication. The database provider of choice is SQL Server.
* [ASP.NET Core 3.1](https://dotnet.microsoft.com/learn/aspnet/what-is-aspnet-core) (RESTful Web API)
* [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) (Web Socket)
* [MediatR](https://github.com/jbogard/MediatR) (CQRS)
* [Swagger](https://swagger.io/tools/swagger-ui) (Open API)
* [SQL Server](https://www.microsoft.com/en-gb/sql-server/sql-server-2019) (Database)
* [xUnit](https://xunit.net) (Testing)

### 💥 Front-End
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/angular/angular.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/typescript/typescript.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/96943574ba0c0340ba6ea1e6f768e9abe43e34e1/topics/javascript/javascript.png"></code>
<code><img height="50" src="https://raw.githubusercontent.com/github/explore/96943574ba0c0340ba6ea1e6f768e9abe43e34e1/topics/sass/sass.png"></code>

The front-end is built as a Single-Page-Application using the Angular Framework from Google. It uses a state management solution similar to Redux for persisting local state. The design pattern follows mostly Google's material design, with a few exceptions here and there.
* [Angular](https://angular.io) 10
* [TypeScript](https://www.typescriptlang.org)
* [NGRX](https://ngrx.io) (State Management)
* [Jasmine](https://jasmine.github.io/) (Testing)



