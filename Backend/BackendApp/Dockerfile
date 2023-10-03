FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BackendApp/BackendApp.csproj", "BackendApp/"]
RUN dotnet restore "BackendApp/BackendApp.csproj"
COPY . .
WORKDIR "/src/BackendApp"
RUN dotnet build "BackendApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackendApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/BackendApp/init.sql /app

ENTRYPOINT ["dotnet", "BackendApp.dll"]