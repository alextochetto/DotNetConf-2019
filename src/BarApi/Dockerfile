FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/BarApi/BarApi.csproj", "src/BarApi/"]
RUN dotnet restore "src/BarApi/BarApi.csproj"
COPY . .
WORKDIR "/src/src/BarApi"
RUN dotnet build "BarApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BarApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarApi.dll"]