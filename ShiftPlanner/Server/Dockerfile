#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0.4-alpine3.17-amd64 AS base
RUN apk add --no-cache tzdata
ENV TZ=Europe/Brussels
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShiftPlanner/Server/ShiftPlanner.Server.csproj", "ShiftPlanner/Server/"]
COPY ["ShiftPlanner/Client/ShiftPlanner.Client.csproj", "ShiftPlanner/Client/"]
COPY ["ShiftPlanner/Shared/ShiftPlanner.Shared.csproj", "ShiftPlanner/Shared/"]
RUN dotnet restore "ShiftPlanner/Server/ShiftPlanner.Server.csproj"
COPY . .
WORKDIR "/src/ShiftPlanner/Server"
RUN dotnet build "ShiftPlanner.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShiftPlanner.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShiftPlanner.Server.dll"]
