#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# BEGIN COMMON SECTION
# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "AccessAllAgents.sln" "AccessAllAgents.sln"

COPY ["Common/AccessAllAgents.Email/AccessAllAgents.Email.csproj", "Common/AccessAllAgents.Email/"]
COPY ["Common/AccessAllAgents.Logging/AccessAllAgents.Logging.csproj", "Common/AccessAllAgents.Logging/"]
COPY ["Common/AccessAllAgents.MicroService.Common/AccessAllAgents.MicroService.Common.csproj", "Common/AccessAllAgents.MicroService.Common/"]
COPY ["Common/AccessAllAgents.MicroService.Template/AccessAllAgents.MicroService.Template.csproj", "Common/AccessAllAgents.MicroService.Template/"]
COPY ["Common/AccessAllAgents.Queue/AccessAllAgents.Queue.csproj", "Common/AccessAllAgents.Queue/"]
COPY ["Common/AccessAllAgents.Queue.Tests/AccessAllAgents.Queue.Tests.csproj", "Common/AccessAllAgents.Queue.Tests/"]

COPY ["Gateway/AccessAllAgents.ApiGateway/AccessAllAgents.ApiGateway.csproj", "Gateway/AccessAllAgents.ApiGateway/"]

COPY ["MicroServices/Logs/AccessAllAgents.MicroService.Logs/AccessAllAgents.MicroService.Logs.csproj", "MicroServices/Logs/AccessAllAgents.MicroService.Logs/"]

COPY ["MicroServices/Messages/AccessAllAgents.MicroService.Messages/AccessAllAgents.MicroService.Messages.csproj", "MicroServices/Messages/AccessAllAgents.MicroService.Messages/"]
COPY ["MicroServices/Messages/AccessAllAgents.MicroService.Messages.Services/AccessAllAgents.MicroService.Messages.Services.csproj", "MicroServices/Messages/AccessAllAgents.MicroService.Messages.Services/"]

COPY ["MicroServices/Notifications/AccessAllAgents.MicroService.Notifications/AccessAllAgents.MicroService.Notifications.csproj", "MicroServices/Notifications/AccessAllAgents.MicroService.Notifications/"]
COPY ["MicroServices/Notifications/AccessAllAgents.MicroService.Notifications.Services/AccessAllAgents.MicroService.Notifications.Services.csproj", "MicroServices/Notifications/AccessAllAgents.MicroService.Notifications.Services/"]
COPY ["MicroServices/Notifications/AccessAllAgents.MicroService.Notifications.Tests/AccessAllAgents.MicroService.Notifications.Tests.csproj", "MicroServices/Notifications/AccessAllAgents.MicroService.Notifications.Tests/"]

COPY ["MicroServices/Property/AccessAllAgents.MicroService.Property/AccessAllAgents.MicroService.Property.csproj", "MicroServices/Property/AccessAllAgents.MicroService.Property/"]
COPY ["MicroServices/Property/AccessAllAgents.MicroService.Property.Services/AccessAllAgents.MicroService.Property.Services.csproj", "MicroServices/Property/AccessAllAgents.MicroService.Property.Services/"]
COPY ["MicroServices/Property/AccessAllAgents.MicroService.Property.Tests/AccessAllAgents.MicroService.Property.Tests.csproj", "MicroServices/Property/AccessAllAgents.MicroService.Property.Tests/"]

COPY ["MicroServices/Users/AccessAllAgents.MicroService.Users/AccessAllAgents.MicroService.Users.csproj", "MicroServices/Users/AccessAllAgents.MicroService.Users/"]
COPY ["MicroServices/Users/AccessAllAgents.MicroService.Users.Services/AccessAllAgents.MicroService.Users.Services.csproj", "MicroServices/Users/AccessAllAgents.MicroService.Users.Services/"]
COPY ["MicroServices/Users/AccessAllAgents.MicroService.Users.Tests/AccessAllAgents.MicroService.Users.Tests.csproj", "MicroServices/Users/AccessAllAgents.MicroService.Users.Tests/"]

COPY ["UI/AccessAllAgents.UI.Angular/AccessAllAgents.UI.Angular.csproj", "UI/AccessAllAgents.UI.Angular/"]

COPY ["Kubernetes/AccessAllAgents.Kubernetes/AccessAllAgents.Kubernetes.csproj", "Kubernetes/AccessAllAgents.Kubernetes/"]

COPY ["docker-compose.dcproj", "docker-compose.dcproj"]

RUN dotnet restore "AccessAllAgents.sln"
# END COMMON SECTION

COPY . .
WORKDIR "/src/MicroServices/Users/AccessAllAgents.MicroService.Users"
RUN dotnet build "AccessAllAgents.MicroService.Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccessAllAgents.MicroService.Users.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccessAllAgents.MicroService.Users.dll"]
