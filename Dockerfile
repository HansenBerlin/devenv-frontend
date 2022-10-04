FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MinimalFrontend/MinimalFrontend.csproj", "MinimalFrontend/"]
RUN dotnet restore "MinimalFrontend/MinimalFrontend.csproj"
COPY . .
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
WORKDIR "/src/MinimalFrontend"
RUN dotnet build "MinimalFrontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalFrontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:80"

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalFrontend.dll"]
