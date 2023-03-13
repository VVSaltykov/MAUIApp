#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MAUI_API/MAUI_API.csproj", "MAUI_API/"]
RUN dotnet restore "MAUI_API/MAUI_API.csproj"
COPY . .
WORKDIR "/src/MAUI_API"
RUN dotnet build "MAUI_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MAUI_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MAUI_API.dll"]