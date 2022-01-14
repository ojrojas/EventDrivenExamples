FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY "EventDrivenDesign.sln" "EventDrivenDesign.sln"

COPY "BuildingBlocks/EventBus/EventBus.csproj" "BuildingBlocks/EventBus/EventBus.csproj"
COPY "BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "Rest1/Rest1.csproj" "Rest1/Rest1.csproj"
COPY "Rest2/Rest2.csproj" "Rest2/Rest2.csproj"

COPY "Rest1Test/Rest1Test.csproj" "Rest1Test/Rest1Test.csproj"
COPY "Rest2Test/Rest2Test.csproj" "Rest2Test/Rest2Test.csproj"

RUN dotnet clean "EventDrivenDesign.sln"
RUN dotnet restore "EventDrivenDesign.sln"

COPY . .
WORKDIR /src/Rest2
RUN dotnet publish --no-restore -c Release -o /app

FROM build as unittest
WORKDIR /src/Rest2test

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rest2.dll"]