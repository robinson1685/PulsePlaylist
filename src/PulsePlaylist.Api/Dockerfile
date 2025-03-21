# Stage 1: Build the API Application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/PulsePlaylist.Api/PulsePlaylist.Api.csproj", "src/PulsePlaylist.Api/"]
COPY ["src/PulsePlaylist.Application/PulsePlaylist.Application.csproj", "src/PulsePlaylist.Application/"]
COPY ["src/PulsePlaylist.Domain/PulsePlaylist.Domain.csproj", "src/PulsePlaylist.Domain/"]
COPY ["src/PulsePlaylist.Infrastructure/PulsePlaylist.Infrastructure.csproj", "src/PulsePlaylist.Infrastructure/"]
COPY ["src/PulsePlaylist.ServiceDefaults/PulsePlaylist.ServiceDefaults.csproj", "src/PulsePlaylist.ServiceDefaults/"]
COPY ["src/Migrators/Migrators.MSSQL/Migrators.MSSQL.csproj", "src/Migrators/Migrators.MSSQL/"]
COPY ["src/Migrators/Migrators.PostgreSQL/Migrators.PostgreSQL.csproj", "src/Migrators/Migrators.PostgreSQL/"]
COPY ["src/Migrators/Migrators.SQLite/Migrators.SQLite.csproj", "src/Migrators/Migrators.SQLite/"]

RUN dotnet restore "src/PulsePlaylist.Api/PulsePlaylist.Api.csproj"

COPY . .
WORKDIR /src/src/PulsePlaylist.Api
RUN dotnet publish "./PulsePlaylist.Api.csproj" -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/publish .

# Install OpenSSL
RUN apt-get update && apt-get install -y openssl

# Generate a self-signed certificate
RUN mkdir -p /app/https && \
    openssl req -x509 -newkey rsa:4096 -sha256 -days 3650 -nodes \
    -keyout /app/https/private.key -out /app/https/certificate.crt \
    -subj "/C=US/ST=State/L=City/O=Organization/CN=localhost" && \
    openssl pkcs12 -export -out /app/https/aspnetapp.pfx \
    -inkey /app/https/private.key -in /app/https/certificate.crt \
    -password pass:CREDENTIAL_PLACEHOLDER


# Setup environment variables for the application to find the certificate
ENV ASPNETCORE_URLS=http://+:80;https://+:443
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="CREDENTIAL_PLACEHOLDER"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/app/https/aspnetapp.pfx"


# Expose ports
EXPOSE 80 443

# Set the environment variable for ASP.NET Core to use Production settings
ENV ASPNETCORE_ENVIRONMENT=Development

# Start the application
ENTRYPOINT ["dotnet", "PulsePlaylist.Api.dll"]
