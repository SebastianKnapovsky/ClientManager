# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ClientManager.API/ClientManager.API.csproj", "ClientManager.API/"]
COPY ["ClientManager.Infrastructure/ClientManager.Infrastructure.csproj", "ClientManager.Infrastructure/"]
COPY ["ClientManager.Core/ClientManager.Core.csproj", "ClientManager.Core/"]
RUN dotnet restore "ClientManager.API/ClientManager.API.csproj"

COPY . .
WORKDIR "/src/ClientManager.API"
RUN dotnet publish "ClientManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClientManager.API.dll"]