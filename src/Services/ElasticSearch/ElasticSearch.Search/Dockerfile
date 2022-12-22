#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/ElasticSearch/ElasticSearch.Search/ElasticSearch.Search.csproj", "Services/ElasticSearch/ElasticSearch.Search/"]
COPY ["BuildingBlocks/Infrastructure/Api.Customization/API.Customization.csproj", "BuildingBlocks/Infrastructure/Api.Customization/"]
COPY ["BuildingBlocks/Models/Domain.Seedwork/Domain.Seedwork.csproj", "BuildingBlocks/Models/Domain.Seedwork/"]
COPY ["Services/ElasticSearch/ElasticSearch.Shared/ElasticSearch.Shared.csproj", "Services/ElasticSearch/ElasticSearch.Shared/"]
RUN dotnet restore "Services/ElasticSearch/ElasticSearch.Search/ElasticSearch.Search.csproj"
COPY . .
WORKDIR "/src/Services/ElasticSearch/ElasticSearch.Search"
RUN dotnet build "ElasticSearch.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElasticSearch.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/app \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true \
    IsDockerRunning=true