#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/FrontOffice.Bff.API/FrontOffice.Bff.API.csproj", "ApiGateways/FrontOffice.Bff.API/"]
COPY ["BuildingBlocks/Infrastructure/Api.Customization/API.Customization.csproj", "BuildingBlocks/Infrastructure/Api.Customization/"]
RUN dotnet restore "ApiGateways/FrontOffice.Bff.API/FrontOffice.Bff.API.csproj"
COPY . .
WORKDIR "/src/ApiGateways/FrontOffice.Bff.API"
RUN dotnet build "FrontOffice.Bff.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FrontOffice.Bff.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrontOffice.Bff.API.dll"]