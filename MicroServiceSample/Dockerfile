#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroServiceSample/MicroServiceSample.csproj", "MicroServiceSample/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "MicroServiceSample/MicroServiceSample.csproj"
COPY . .
WORKDIR "/src/MicroServiceSample"
RUN dotnet build "MicroServiceSample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroServiceSample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServiceSample.dll"]