FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/WebBar/WebBar.csproj", "src/WebBar/"]
RUN dotnet restore "src/WebBar/WebBar.csproj"
COPY . .
WORKDIR "/src/src/WebBar"
RUN dotnet build "WebBar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBar.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBar.dll"]