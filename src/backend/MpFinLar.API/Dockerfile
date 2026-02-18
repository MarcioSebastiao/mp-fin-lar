# Base Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Copia apenas arquivos necessários para restore
COPY ["MpFinLar.API/MpFinLar.API.csproj", "MpFinLar.API/"]

RUN dotnet restore MpFinLar.API/MpFinLar.API.csproj

# Copia o resto do código
COPY . .

WORKDIR /src/MpFinLar.API
RUN dotnet build MpFinLar.API.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish MpFinLar.API.csproj -c Release -o /app/publish

# Imagem Final 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MpFinLar.API.dll"]
