
## Purpose

  

This is a simple URL shortener API built with ASP.NET Core and MongoDB.

It allows users to submit long URLs and receive a shortened version, which can later be used to redirect back to the original URL.

  
  

## The Technologies Used

  

- ASP.NET Core 9.0 — Web API framework

- MongoDB — NoSQL database for storing URLs

- Docker — For running MongoDB locally

- Swagger — For API documentation and testing

- React — For building the frontend user interface

  
  

## Dependencies (Only if NOT using Docker Compose - else jump to the bottom of this page)

  

First of all you need .NET 9.0 installed, via this link:

https://dotnet.microsoft.com/download

  ---

There are also .NET packages you have to install in the project's environment in order to run it.

You will need to run the following commands in the top folder "UrlShortenerApi":

  
```
dotnet add package MongoDB.Driver

dotnet add package Swashbuckle.AspNetCore
```
---  

For the front you need Node.js installed, get it via:

https://nodejs.org/

  
  
  

## Running Instructions (Jump to the bottom to use docker-compose)

  

For the mongoDB you will need to run mongo container via Docker using this command:

  
```
docker run -d -p 27017:27017 mongo
```
  

It will run the public default mongo container on port 27017 and start it automatically.

---

> Optional:

>Download MongoDB Compass for viewing and managing the MongoDB databse.

  ---

For running the front, run those commands:

  
```
cd urlshortenerfrontend -> go to the front top folder

npm install -> installs the app's dependencies

npm start
```
  
---

Secondly, you will need to run those commands:

```
cd UrlShortenerApi -> go to the api top folder

dotnet restore -> to install NuGet packages

dotnet run
```
  ---

The Swagger UI is accessible via:

http://localhost:5009/swagger/

---
**It is highly recommended to use the docker compose for easy build and run**
It will build the images and run them in separate containers. It will also create the network connecting them and the volume for the MongoDB.
In the Project's top folder, where the 'docker-compose.yml' is, run the following commands:
```
docker-compose build
docker-compose up # -d flag to run it in the background
```
And in order to kill and remove all images, containers, networks, volumes etc. run this command:
```
docker-compose down
```