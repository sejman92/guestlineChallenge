FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GuestlineChallenge/GuestlineChallenge.csproj", "GuestlineChallenge/"]
RUN dotnet restore "GuestlineChallenge/GuestlineChallenge.csproj"
COPY . .
WORKDIR "/src/GuestlineChallenge"
RUN dotnet build "GuestlineChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GuestlineChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GuestlineChallenge.dll"]
