FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/WebFoo/WebFoo.csproj", "src/WebFoo/"]
RUN dotnet restore "src/WebFoo/WebFoo.csproj"
COPY . .
WORKDIR "/src/src/WebFoo"
RUN dotnet build "WebFoo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebFoo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebFoo.dll"]