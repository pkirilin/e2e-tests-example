# e2e-tests-example

## Run the application locally

Start database in docker:

```shell
docker run -p 3306:3306 --name mysql -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=e2e_tests_example -d mysql:8.0.32
```

Run database migrations:

```shell
cd src/BackendApp.Migrator
dotnet run
```

Start backend app:

```shell
cd src/BackendApp.Web
dotnet run
```

Start frontend app:

```shell
cd src/BackendApp.Web/app
yarn
yarn start
```

Navigate to <https://localhost:11000>

## Run tests with docker-compose

```shell
docker-compose up -d --build
cd tests/e2e
yarn test
```

## How to generate certs for HTTPS

Generate pfx:

```shell
dotnet dev-certs https -ep tests/e2e/https/app.pfx -p test
```

Generate crt:

```shell
dotnet dev-certs https --trust --export-path tests/e2e/https/ca.cert.crt
```

Enter **system** (not pfx!) password
