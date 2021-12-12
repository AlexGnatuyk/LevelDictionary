FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LevelDictionary/LevelDictionary.csproj", "LevelDictionary/"]
RUN dotnet restore "LevelDictionary/LevelDictionary.csproj"
COPY . .
WORKDIR "/src/LevelDictionary"
RUN dotnet build "LevelDictionary.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LevelDictionary.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LevelDictionary.dll"]
