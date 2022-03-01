# Dot Net

```bash
$ git clone git@github.com:ldynia/dotnet-lucky-music.git
$ cd dotnet-lucky-music/app

# Install Newtonsoft package
$ dotnet add package xunit --version 2.4.2-pre.12
$ dotnet add package xunit.runner.visualstudio --version 2.4.3
$ dotnet add package Newtonsoft.Json --version 13.0.1
$ dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 6.0.2
$ dotnet add package Microsoft.NET.Test.Sdk --version 17.1.0
$ dotnet build
$ dotnet run
```

# API

```bash
$ curl http://localhost:5087/api/v1/music/recommend
$ curl https://localhost:7262/api/v1/music/recommend
```