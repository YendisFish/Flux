FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Flux/Flux.csproj", "Flux/"]
RUN dotnet restore "Flux/Flux.csproj"
COPY . .
WORKDIR "/src/Flux"
RUN dotnet build "Flux.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flux.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flux.dll"]
