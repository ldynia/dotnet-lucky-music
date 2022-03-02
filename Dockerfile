FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder

WORKDIR /app
COPY app/ ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=builder /app/out ./
RUN ls -l /app

ENTRYPOINT ["dotnet", "dotnet-lucky-music.dll"]