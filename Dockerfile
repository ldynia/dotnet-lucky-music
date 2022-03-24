# Build artifact image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS BUILDER

WORKDIR /src
COPY app/ /src/app
COPY tests/ /src/tests

RUN dotnet restore /src/app/lucky-music.csproj
RUN dotnet publish /src/app/lucky-music.csproj -c Release -o /src/app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

# Setup build args
ARG ASPNETCORE_ENVIRONMENT=Production \
    PORT=8080

# Setup environment variables
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT \
    ASPNETCORE_URLS=http://+:$PORT \
    PORT=$PORT

WORKDIR /app

COPY --from=BUILDER /src/app/out ./

EXPOSE $PORT

ENTRYPOINT ["dotnet", "lucky-music.dll"]