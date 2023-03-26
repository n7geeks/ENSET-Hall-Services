FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ENSETHall.ScolarityService/ENSETHall.ScolarityService.csproj", "ENSETHall.ScolarityService/"]
RUN dotnet restore "ENSETHall.ScolarityService/ENSETHall.ScolarityService.csproj"
COPY . .
WORKDIR "/src/ENSETHall.ScolarityService"
RUN dotnet build "ENSETHall.ScolarityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ENSETHall.ScolarityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ENSETHall.ScolarityService.dll"]
