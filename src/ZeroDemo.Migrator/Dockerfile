#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/ZeroDemo.Migrator/ZeroDemo.Migrator.csproj", "src/ZeroDemo.Migrator/"]
COPY ["src/ZeroDemo.EntityFrameworkCore/ZeroDemo.EntityFrameworkCore.csproj", "src/ZeroDemo.EntityFrameworkCore/"]
COPY ["src/ZeroDemo.Core/ZeroDemo.Core.csproj", "src/ZeroDemo.Core/"]
COPY ["src/ZeroDemo.Core.Shared/ZeroDemo.Core.Shared.csproj", "src/ZeroDemo.Core.Shared/"]
RUN dotnet restore "src/ZeroDemo.Migrator/ZeroDemo.Migrator.csproj"
COPY . .
WORKDIR "/src/src/ZeroDemo.Migrator"
RUN dotnet build "ZeroDemo.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZeroDemo.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZeroDemo.Migrator.dll"]
