FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MyPortfolio.API/MyPortfolio.API.csproj", "MyPortfolio.API/"]
COPY ["MyPortfolio.Application/MyPortfolio.Application.csproj", "MyPortfolio.Application/"]
COPY ["MyPortfolio.Infrastructure/MyPortfolio.Infrastructure.csproj", "MyPortfolio.Infrastructure/"]
COPY ["MyPortfolio.Domain/MyPortfolio.Domain.csproj", "MyPortfolio.Domain/"]

RUN dotnet restore "MyPortfolio.API/MyPortfolio.API.csproj"

COPY . .
RUN dotnet build "MyPortfolio.API/MyPortfolio.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyPortfolio.API/MyPortfolio.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPortfolio.API.dll"]