# Chatbot
## Table of contents
* [General info](#general-info)
* [Features](#features)
* [Technologies](#technologies)
* [Prerequisites](#prerequisites)
* [Setup](#setup)
* [Usage](#usage)

---

## General info
This project is designed to complete a technical test of back-end web technologies for Jobsity.
The goal of this exercise is to create a simple browser-based chat application using .NET.
---

## Features
* Allow registered users to log in and talk with other users in a chatroom.
*  Chat messages ordered by their timestamps. When a user gets connected to the chatroom the last 50 messages are displayed.
* Use of .NET Identity for authentication.
* Messages that are not understood or any exceptions raised within the bot are handled.
---

## Technologies
The usted thecnologies listed below:

* Angular 8.2.12
* .NET Core 3.1
* SQL Server
* RabbitMQ
* SignalR
* Bootstrap
---

## Prerequisites
* Visual Studio 2019
* SQL Server
* Instance of RabbitMQ
---

## Setup
Follow the next steps to run this project locally:

1. Make sure you can run C# and Angular apps in your computer. For this, you'll need to have installed NodeJS, Angular version 8.2.12 or higher and .NET SDK version 3.1 or higher.

2. Open the project solution in Visual Studio, look inside the **Webchat** for the `appsettings.json` and set the connection string in the `appsettings.Development.json`.

3. Open the **Package Manager Console**, if you can't find it, click Tools in the menu and click `NuGet Package Manager > Package Manager Console`, then select **Webchat** in the Default Project dropdown from the Package Manager Console and run:
```
Update-Database
```

4. Now you can run the application.

---

## Usage
Once the application is running, you just need to register as an user and login into the app to access the chatroom.

### Considerations

To register, your password must have at least one:

* At least one upper case English letter
* At least one lower case English letter
* At least one digit
* At least one special character
* Minimum eight in length
---