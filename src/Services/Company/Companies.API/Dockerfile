#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Company/Companies.API/Companies.API.csproj", "Services/Company/Companies.API/"]
COPY ["Services/Company/Companies.Domain/Companies.Domain.csproj", "Services/Company/Companies.Domain/"]
COPY ["BuildingBlocks/Infrastructure/Api.Customization/API.Customization.csproj", "BuildingBlocks/Infrastructure/Api.Customization/"]
COPY ["BuildingBlocks/Models/Domain.Seedwork/Domain.Seedwork.csproj", "BuildingBlocks/Models/Domain.Seedwork/"]
RUN dotnet restore "Services/Company/Companies.API/Companies.API.csproj"
COPY . .
WORKDIR "/src/Services/Company/Companies.API"
RUN dotnet build "Companies.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Companies.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Companies.API.dll"]