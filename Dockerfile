# Set the base image to the official .NET 7 SDK image from Docker Hub
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the solution file and restore dependencies
COPY *.sln .
COPY Tanks.Api/Tanks.Api.csproj Tanks.Api/
COPY Tanks.Domain/Tanks.Domain.csproj Tanks.Domain/
COPY Tanks.Application/Tanks.Application.csproj Tanks.Application/
COPY Tanks.Infrastructure/Tanks.Infrastructure.csproj Tanks.Infrastructure/
RUN dotnet restore

# Copy the entire project directory to the container
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Set the base image to the official .NET 7 runtime image from Docker Hub
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the build output from the build stage to the runtime stage
COPY --from=build /app/out .

# Expose the desired port(s) for the application
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Tanks.Api.dll"]
