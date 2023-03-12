# e2e-tests-example

## How to generate certs

Generate pfx

```shell
dotnet dev-certs https -ep tests/e2e/https/app.pfx -p test
```

Generate crt

```shell
dotnet dev-certs https --trust --export-path tests/e2e/https/ca.cert.crt
```

Enter **system** (not pfx!) password
