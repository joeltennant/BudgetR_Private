# Basic BudgetR

## Introduction

This is a sample application for a full-stack .Net app. It also serves as a playground for creating a budgeting and financial application.

The application is using MediatR to facilate a CRQS approach. Because the application is using Blazor Server, the mediator pattern helps to isolate the back-end processes behind a logical wall that can only be accessed through MediatR handlers.

## Running Locally

The only challenge to setting up this aplication is getting your own Azure B2C tenant setup. Once that is complete add your keys to the appsettings.json file. Not recommended adding the keys directly, use secrets.

Steps:

1. Clone Repository
2. Setup Azure B2C tenant
   A. Add Keys to Secrets in VS
3. Set BasicBudgetR as the start up project
4. Run Migrations
   1. Note - This requires SQL's localdb. If you have a different edition go to appsettings.Development.json and change the connection string as needed.
   2. Open up the Package Manager Console
   3. Within the console, set the default project to BasicBudgetR.Server.Infrastructure
   4. Run the commandupdate-database
5. Run the application
6. When the app runs, it will prompt you to sign-in or sign-up. Sign up and then proceed from there.
