# BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution file and restore as distinct layers for caching
COPY BookStore.sln .
COPY src/BookStore.Api/BookStore.Api.csproj src/BookStore.Api/
COPY src/BookStore.Application/BookStore.Application.csproj src/BookStore.Application/
COPY src/BookStore.Domain/BookStore.Domain.csproj src/BookStore.Domain/
COPY src/BookStore.Infrastructure/BookStore.Infrastructure.csproj src/BookStore.Infrastructure/
COPY tests/BookStore.Tests/BookStore.Tests.csproj tests/BookStore.Tests/

# Restore dependencies
RUN dotnet restore BookStore.sln

# Copy all files after restoring dependencies
COPY src ./src
COPY tests ./tests

# Build and publish the API
RUN dotnet publish src/BookStore.Api/BookStore.Api.csproj --no-restore -c Release -o /app/publish

# RUNTIME STAGE
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy only the published output
COPY --from=build /app/publish .

# Start the API
ENTRYPOINT ["dotnet", "BookStore.Api.dll"]
