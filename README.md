## Purpose

This is a simple URL shortener API built with ASP.NET Core and MongoDB. It allows users to submit long URLs and receive a shortened version, which can later be used to redirect back to the original URL.


## The Technologies Used

- ASP.NET Core 9.0 — Web API framework
- MongoDB — NoSQL database for storing URLs
- Docker — For running MongoDB locally
- Swagger — For API documentation and testing


## Dependencies

First of all you need .NET 9.0 installed, via this link:
    https://dotnet.microsoft.com/download

There are also .NET packages you have to install in the project's environment in order to run it.
You will need to run the following commands in the top folder "UrlShortenerApi":

    dotnet add package MongoDB.Driver
    dotnet add package Swashbuckle.AspNetCore


## Running Instructions

For the mongoDB you will need to run mongo container via Docker using this command:
    docker run -d -p 27017:27017 mongo
It will run the public default mongo container on port 27017 and start it automatically.
Optional: download MongoDB Compass for viewing and managing 

Secondly, you will need to run those commands:
    cd UrlShortenerApi - go to the project's top folder
    dotnet restore - to install NuGet packages
    dotnet run

The Swagger UI is accessible via:
    http://localhost:5009/swagger/