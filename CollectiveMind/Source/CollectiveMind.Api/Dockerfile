#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Source/CollectiveMind.Api/CollectiveMind.Api.csproj", "Source/CollectiveMind.Api/"]
RUN dotnet restore "Source/CollectiveMind.Api/CollectiveMind.Api.csproj"
COPY . .
WORKDIR "/src/Source/CollectiveMind.Api"
RUN dotnet build "CollectiveMind.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CollectiveMind.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CollectiveMind.Api.dll"]
