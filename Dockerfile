FROM mcr.microsoft.com/dotnet/sdk:6.0 AS backend
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o publish src/BackendApp.Web/BackendApp.Web.csproj

FROM node:18.14.2-alpine AS frontend
WORKDIR /app
COPY src/BackendApp.Web/app .
RUN yarn install
ENV PATH="./node_modules/.bin:$PATH"
RUN yarn build

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=backend app/publish .
COPY --from=frontend app/dist app/dist
EXPOSE 80
ENTRYPOINT ["dotnet", "BackendApp.Web.dll"]
