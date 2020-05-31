#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SpeedTest.Server.WebAPI/SpeedTest.Server.WebAPI.csproj", "SpeedTest.Server.WebAPI/"]
RUN dotnet restore "SpeedTest.Server.WebAPI/SpeedTest.Server.WebAPI.csproj"
COPY . .
WORKDIR "/src/SpeedTest.Server.WebAPI"
RUN dotnet build "SpeedTest.Server.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpeedTest.Server.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpeedTest.Server.WebAPI.dll"]