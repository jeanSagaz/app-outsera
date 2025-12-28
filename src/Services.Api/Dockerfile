# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./Movielist.csv /Movielist.csv
COPY ["src/Core/Core.csproj", "src/Core/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Infra.Data/Infra.Data.csproj", "src/Infra.Data/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Services.Api/Services.Api.csproj", "src/Services.Api/"]
RUN dotnet restore "./src/Services.Api/Services.Api.csproj"
COPY . .
WORKDIR "/src/src/Services.Api"
RUN dotnet build "./Services.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Movielist.csv .
ENTRYPOINT ["dotnet", "Services.Api.dll"]