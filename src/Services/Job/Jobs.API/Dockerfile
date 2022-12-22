#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Job/Jobs.API/Jobs.API.csproj", "Services/Job/Jobs.API/"]
COPY ["Services/Job/Jobs.Application/Jobs.Application.csproj", "Services/Job/Jobs.Application/"]
COPY ["Services/Job/Jobs.Domain/Jobs.Domain.csproj", "Services/Job/Jobs.Domain/"]
COPY ["BuildingBlocks/Models/Domain.Seedwork/Domain.Seedwork.csproj", "BuildingBlocks/Models/Domain.Seedwork/"]
COPY ["BuildingBlocks/Infrastructure/Api.Customization/API.Customization.csproj", "BuildingBlocks/Infrastructure/Api.Customization/"]
RUN dotnet restore "Services/Job/Jobs.API/Jobs.API.csproj"
COPY . .
WORKDIR "/src/Services/Job/Jobs.API"
RUN dotnet build "Jobs.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jobs.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jobs.API.dll"]