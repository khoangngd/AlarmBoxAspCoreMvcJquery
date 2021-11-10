#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/ZeroDemo.Web.Host/ZeroDemo.Web.Host.csproj", "src/ZeroDemo.Web.Host/"]
COPY ["src/ZeroDemo.Web.Core/ZeroDemo.Web.Core.csproj", "src/ZeroDemo.Web.Core/"]
COPY ["src/ZeroDemo.Application/ZeroDemo.Application.csproj", "src/ZeroDemo.Application/"]
COPY ["src/ZeroDemo.Application.Shared/ZeroDemo.Application.Shared.csproj", "src/ZeroDemo.Application.Shared/"]
COPY ["src/ZeroDemo.Core.Shared/ZeroDemo.Core.Shared.csproj", "src/ZeroDemo.Core.Shared/"]
COPY ["src/ZeroDemo.Core/ZeroDemo.Core.csproj", "src/ZeroDemo.Core/"]
COPY ["src/ZeroDemo.EntityFrameworkCore/ZeroDemo.EntityFrameworkCore.csproj", "src/ZeroDemo.EntityFrameworkCore/"]
COPY ["src/ZeroDemo.GraphQL/ZeroDemo.GraphQL.csproj", "src/ZeroDemo.GraphQL/"]
RUN dotnet restore "src/ZeroDemo.Web.Host/ZeroDemo.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/ZeroDemo.Web.Host"
RUN dotnet build "ZeroDemo.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZeroDemo.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZeroDemo.Web.Host.dll"]
