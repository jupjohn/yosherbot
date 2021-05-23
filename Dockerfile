FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY src ./
RUN ls
RUN dotnet restore "Jammehcow.YosherBot.Console/Jammehcow.YosherBot.Console.csproj"
WORKDIR "/src/Jammehcow.YosherBot.Console"
RUN dotnet build "Jammehcow.YosherBot.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jammehcow.YosherBot.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jammehcow.YosherBot.Console.dll"]
