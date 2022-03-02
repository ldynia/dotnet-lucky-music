# Build artifact image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS BUILDER

WORKDIR /app
COPY app/ ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

WORKDIR /app

COPY --from=BUILDER /app/out ./
RUN ls -l /app

ARG ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

ENTRYPOINT ["dotnet", "dotnet-lucky-music.dll"]