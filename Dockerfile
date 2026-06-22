FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/MyPortfolio.API/MyPortfolio.API.csproj", "src/MyPortfolio.API/"]
COPY ["src/MyPortfolio.Application/MyPortfolio.Application.csproj", "src/MyPortfolio.Application/"]
COPY ["src/MyPortfolio.Infrastructure/MyPortfolio.Infrastructure.csproj", "src/MyPortfolio.Infrastructure/"]
COPY ["src/MyPortfolio.Domain/MyPortfolio.Domain.csproj", "src/MyPortfolio.Domain/"]

RUN dotnet restore "src/MyPortfolio.API/MyPortfolio.API.csproj"

COPY . .
RUN dotnet build "src/MyPortfolio.API/MyPortfolio.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/MyPortfolio.API/MyPortfolio.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPortfolio.API.dll"]