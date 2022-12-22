#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/AdministrationSettings/AdministrationSettings.API/AdministrationSettings.API.csproj", "Services/AdministrationSettings/AdministrationSettings.API/"]
COPY ["BuildingBlocks/Infrastructure/Api.Customization/API.Customization.csproj", "BuildingBlocks/Infrastructure/Api.Customization/"]
RUN dotnet restore "Services/AdministrationSettings/AdministrationSettings.API/AdministrationSettings.API.csproj"
COPY . .
WORKDIR "/src/Services/AdministrationSettings/AdministrationSettings.API"
RUN dotnet build "AdministrationSettings.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdministrationSettings.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdministrationSettings.API.dll"]