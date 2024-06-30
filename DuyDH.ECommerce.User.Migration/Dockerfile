FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DuyDH.ECommerce.User.Migration/DuyDH.ECommerce.User.Migration.csproj", "DuyDH.ECommerce.User.Migration/"]
RUN dotnet restore "DuyDH.ECommerce.User.Migration/DuyDH.ECommerce.User.Migration.csproj"
COPY . .
WORKDIR "/src/DuyDH.ECommerce.User.Migration"
RUN dotnet build "DuyDH.ECommerce.User.Migration.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DuyDH.ECommerce.User.Migration.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DuyDH.ECommerce.User.Migration.dll"]
