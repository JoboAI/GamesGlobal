FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Web/GamesGlobal.Web.Api/GamesGlobal.Web.Api.csproj", "src/Web/GamesGlobal.Web.Api/"]
RUN dotnet restore "src/Web/GamesGlobal.Web.Api/GamesGlobal.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Web/GamesGlobal.Web.Api"
RUN dotnet build "GamesGlobal.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GamesGlobal.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamesGlobal.Web.Api.dll"]
