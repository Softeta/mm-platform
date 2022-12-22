#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Candidate/Candidates.API/Candidates.API.csproj", "Services/Candidate/Candidates.API/"]
COPY ["Services/Candidate/Candidates.Domain/Candidates.Domain.csproj", "Services/Candidate/Candidates.Domain/"]
COPY ["BuildingBlocks/Models/Domain.Seedwork/Domain.Seedwork.csproj", "BuildingBlocks/Models/Domain.Seedwork/"]
COPY ["BuildingBlocks/Infrastructure/Api.Customization/API.Customization.csproj", "BuildingBlocks/Infrastructure/Api.Customization/"]
RUN dotnet restore "Services/Candidate/Candidates.API/Candidates.API.csproj"
COPY . .
WORKDIR "/src/Services/Candidate/Candidates.API"
RUN dotnet build "Candidates.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Candidates.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Candidates.API.dll"]