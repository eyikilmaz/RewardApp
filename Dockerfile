
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7130

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Api/WebApi/RewardApp.Api.WebApi/RewardApp.Api.WebApi.csproj", "src/Api/WebApi/RewardApp.Api.WebApi/"]
COPY ["src/ExceptionHandlingExtension/RewardApp.ExceptionHandlingExtension/RewardApp.ExceptionHandlingExtension.csproj", "src/ExceptionHandlingExtension/RewardApp.ExceptionHandlingExtension/"]
COPY ["src/Api/Core/RewardApp.Api.Application/RewardApp.Api.Application.csproj", "src/Api/Core/RewardApp.Api.Application/"]
COPY ["src/Common/RewardApp.Common/RewardApp.Common.csproj", "src/Common/RewardApp.Common/"]
COPY ["src/Api/Core/RewardApp.Api.Domain/RewardApp.Api.Domain.csproj", "src/Api/Core/RewardApp.Api.Domain/"]
COPY ["src/Api/Infrastructure/RewardApp.Infrastructure.Persistence/RewardApp.Infrastructure.Persistence.csproj", "src/Api/Infrastructure/RewardApp.Infrastructure.Persistence/"]
RUN dotnet restore "src/Api/WebApi/RewardApp.Api.WebApi/RewardApp.Api.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Api/WebApi/RewardApp.Api.WebApi"
RUN dotnet build "RewardApp.Api.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RewardApp.Api.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RewardApp.Api.WebApi.dll"]