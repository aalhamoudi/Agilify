# Architecture

## Overview

Agilify is client-server architectured application, structured as follows:

![](Architecture.jpeg)

## Client

The client is Xamarin.Forms MVVM mobile application, targeting both Android & iOS using mostly a single code base, written entirely in C#.

The client is composed of the following layers:

*   Views
*   View Models: handles all data manipulation on behalf of Views
*   Models
*   Services
    *   Managers
        *   Account Manager: handles all account related operations (signing in, refreshing token, etc...)
        *   Sync Manager: handles offline sync operations
    *   Helpers
        *   Cloud Client: Azure mobile sdk which provides an API for server communication
        *   Settings: stores data on devices as settings (Username, email, etc...)
        *   SQLite: stores syncable data on device

## Server

The server is an ASP.Net application, composed of the following layers:

*   Controllers
    *   Table Controllers: data sync
    *   API Controllers: operations (creating an account, adding a member to a team, etc...)
*   Repositories: handles data operations (Create, Delete, etc...)
*   Services:
    *   Account Manager: handles user management
    *   Provider Manager: handles retreiving user info from social identity providers
*   Providers: Social identity providers for authentication and user info

*   Models

*   Data Access Layer: Entity Framework database context that handles mapping models to sql tables (ORM)

*   Database: Centralized Azure SQL Server database (independent of backend)