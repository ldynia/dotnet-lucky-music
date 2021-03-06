# DotNet

```bash
git clone git@github.com:ldynia/dotnet-lucky-music.git
cd dotnet-lucky-music/app

# Install Newtonsoft package
dotnet add package xunit --version 2.4.2-pre.12
dotnet add package xunit.runner.visualstudio --version 2.4.3
dotnet add package Newtonsoft.Json --version 13.0.1
dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 6.0.2
dotnet add package Microsoft.NET.Test.Sdk --version 17.1.0
dotnet dev-certs https --trust
dotnet build /src/app/
dotnet run /src/app/
dotnet test /src/tests/
```

## Installation

```bash
# Cloning the source code
git clone https://github.com/ldynia/dotnet-lucky-music.git
cd dotnet-lucky-music

# Building and running docker container
docker build --tag lucky-music-dev -f docker/Dockerfile.dev --build-arg ASPNETCORE_ENVIRONMENT=Development .
docker build --tag lucky-music-prod -f docker/Dockerfile.prod --build-arg ASPNETCORE_ENVIRONMENT=Production .
docker run --detach --name music-app-dev --publish 80:80 --rm lucky-music-dev
docker run --detach --name music-app-prod --publish 80:80 --rm lucky-music-prod
docker ps
```

# API

```bash
curl http://localhost/api/v1/music/recommend
curl http://localhost/api/v1/music/recommend
```

# Redis

```bash
docker network create app-backend
docker run --detach --name redis --network app-backend --rm redis redis-server --save 60 1 --loglevel warning
docker run --detach --name music-app-dev --publish 80:80 --rm --network app-backend lucky-music
docker exec music-app nc -zvw 1 redis 6379
```