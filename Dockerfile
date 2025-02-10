# Set the major version of dotnet
ARG DOTNET_VERSION=8.0
# Set the major version of nodejs
ARG NODEJS_VERSION_MAJOR=22

# Build assets
# FROM "node:${NODEJS_VERSION_MAJOR}-bullseye-slim" AS assets
# WORKDIR /app
# COPY ./src/DfE.ManageSchoolImprovement/wwwroot /app
# RUN npm install
# RUN npm run build

# Build the app using the dotnet SDK
FROM "mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-azurelinux3.0" AS build
WORKDIR /build
COPY ./Directory.Build.props /build
COPY ./scripts/docker-entrypoint.sh /app/docker-entrypoint.sh

## START: Restore Packages
ARG PROJECT_NAME="DfE.ManageSchoolImprovement"
COPY ./${PROJECT_NAME}.sln ./Directory.Build.props ./
COPY ./src/${PROJECT_NAME}.Frontend/${PROJECT_NAME}.Frontend.csproj             ./src/${PROJECT_NAME}.Frontend/
COPY ./src/${PROJECT_NAME}.Api/${PROJECT_NAME}.Api.csproj                       ./src/${PROJECT_NAME}.Api/
COPY ./src/${PROJECT_NAME}.Api.Client/${PROJECT_NAME}.Api.Client.csproj         ./src/${PROJECT_NAME}.Api.Client/
COPY ./src/${PROJECT_NAME}.Application/${PROJECT_NAME}.Application.csproj       ./src/${PROJECT_NAME}.Application/
COPY ./src/${PROJECT_NAME}.Domain/${PROJECT_NAME}.Domain.csproj                 ./src/${PROJECT_NAME}.Domain/
COPY ./src/${PROJECT_NAME}.Infrastructure/${PROJECT_NAME}.Infrastructure.csproj ./src/${PROJECT_NAME}.Infrastructure/
COPY ./src/${PROJECT_NAME}.Utils/${PROJECT_NAME}.Utils.csproj                   ./src/${PROJECT_NAME}.Utils/

COPY ./src/Tests/${PROJECT_NAME}.Application.Tests/${PROJECT_NAME}.Application.Tests.csproj         ./src/Tests/${PROJECT_NAME}.Application.Tests/
COPY ./src/Tests/${PROJECT_NAME}.Domain.Tests/${PROJECT_NAME}.Domain.Tests.csproj                   ./src/Tests/${PROJECT_NAME}.Domain.Tests/
COPY ./src/Tests/${PROJECT_NAME}.Frontend.Tests/${PROJECT_NAME}.Frontend.Tests.csproj               ./src/Tests/${PROJECT_NAME}.Frontend.Tests/
COPY ./src/Tests/${PROJECT_NAME}.Infrastructure.Tests/${PROJECT_NAME}.Infrastructure.Tests.csproj   ./src/Tests/${PROJECT_NAME}.Infrastructure.Tests/
COPY ./src/Tests/${PROJECT_NAME}.Tests.Common/${PROJECT_NAME}.Tests.Common.csproj                   ./src/Tests/${PROJECT_NAME}.Tests.Common/

# Mount GitHub Token as a Docker secret so that NuGet Feed can be accessed
RUN --mount=type=secret,id=github_token dotnet nuget add source --username USERNAME --password $(cat /run/secrets/github_token) --store-password-in-clear-text --name github "https://nuget.pkg.github.com/DFE-Digital/index.json"
RUN ["dotnet", "restore", "DfE.ManageSchoolImprovement.sln"]
## END: Restore Packages

WORKDIR /build/src
COPY ./src/ .
RUN ["dotnet", "publish", "DfE.ManageSchoolImprovement", "-c", "Release", "--no-restore", "-o", "/app"]

# Generate an Entity Framework bundle
FROM build AS efbuilder
ENV PATH=$PATH:/root/.dotnet/tools
RUN ["mkdir", "/sql"]
RUN ["dotnet", "tool", "install", "--global", "dotnet-ef"]
RUN ["dotnet", "ef", "migrations", "bundle", "-r", "linux-x64", "-p", "DfE.ManageSchoolImprovement", "--configuration", "Release", "--no-build", "-o", "/sql/migratedb"]

# Create a runtime environment for Entity Framework
FROM "mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-azurelinux3.0" AS initcontainer
WORKDIR /sql
COPY --from=efbuilder /app/appsettings.json /DfE.ManageSchoolImprovement/
COPY --from=efbuilder /sql /sql
RUN chown "$APP_UID" "/sql" -R
USER $APP_UID

# Build a runtime environment
FROM "mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-azurelinux3.0" AS final
WORKDIR /app
LABEL org.opencontainers.image.source="https://github.com/DFE-Digital/regional-improvement-for-standards-and-excellence"

COPY --from=build /app /app
# COPY --from=assets /app /app/wwwroot
RUN ["chmod", "+x", "./docker-entrypoint.sh"]

USER $APP_UID
