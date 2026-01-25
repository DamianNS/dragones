# Use the .NET 10.0 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Editor/Editor.csproj", "Editor/"]
RUN dotnet restore "Editor/Editor.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/src/Editor"
RUN dotnet build "Editor.csproj" -c Debug -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Editor.csproj" -c Debug -o /app/publish /p:UseAppHost=false

# Build the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Editor.dll"]