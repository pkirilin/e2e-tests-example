FROM mcr.microsoft.com/dotnet/sdk:6.0 AS backend
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o publish src/BackendApp.Web/BackendApp.Web.csproj

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=backend app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "BackendApp.Web.dll"]
