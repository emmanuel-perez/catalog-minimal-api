# Use the official .NET 8 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the app
COPY . ./

# Build the app
RUN dotnet publish -c Release -o out

# Use the official .NET 8 runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port on which the app runs (adjust if needed)
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "MinimalCatalogApi.dll"]
