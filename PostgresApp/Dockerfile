#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PostgresApp/PostgresApp.csproj", "PostgresApp/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "PostgresApp/PostgresApp.csproj"
COPY . .
WORKDIR "/src/PostgresApp"
RUN dotnet build "PostgresApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostgresApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostgresApp.dll"]