#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/EmailService/EmailService.WebHook/EmailService.WebHook.csproj", "Services/EmailService/EmailService.WebHook/"]
COPY ["BuildingBlocks/Infrastructure/Persistence.Customization/Persistence.Customization.csproj", "BuildingBlocks/Infrastructure/Persistence.Customization/"]
COPY ["BuildingBlocks/Models/Domain.Seedwork/Domain.Seedwork.csproj", "BuildingBlocks/Models/Domain.Seedwork/"]
COPY ["Services/EmailService/EmailService.Shared/EmailService.Shared.csproj", "Services/EmailService/EmailService.Shared/"]
COPY ["BuildingBlocks/Infrastructure/Api.Customization/API.Customization.csproj", "BuildingBlocks/Infrastructure/Api.Customization/"]
COPY ["BuildingBlocks/Infrastructure/EventBus/EventBus.csproj", "BuildingBlocks/Infrastructure/EventBus/"]
RUN dotnet restore "Services/EmailService/EmailService.WebHook/EmailService.WebHook.csproj"
COPY . .
WORKDIR "/src/Services/EmailService/EmailService.WebHook"
RUN dotnet build "EmailService.WebHook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailService.WebHook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true