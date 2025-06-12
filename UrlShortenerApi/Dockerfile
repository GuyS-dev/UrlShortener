# ----------- Build stage -----------
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ----------- Runtime stage -----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5009
ENV ASPNETCORE_URLS=http://+:5009
ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
