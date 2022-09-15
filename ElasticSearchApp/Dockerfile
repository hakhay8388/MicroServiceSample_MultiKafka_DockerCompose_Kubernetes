#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ElasticSearchApp/ElasticSearchApp.csproj", "ElasticSearchApp/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "ElasticSearchApp/ElasticSearchApp.csproj"
COPY . .
WORKDIR "/src/ElasticSearchApp"
RUN dotnet build "ElasticSearchApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElasticSearchApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElasticSearchApp.dll"]