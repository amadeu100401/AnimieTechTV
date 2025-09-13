# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY AnimieTechTV.sln ./
COPY src/backend/AnimieTechTv.API/AnimieTechTv.API.csproj src/backend/AnimieTechTv.API/
COPY src/backend/AnimieTechTv.Application/AnimieTechTv.Application.csproj src/backend/AnimieTechTv.Application/
COPY src/backend/AnimieTechTv.Domain/AnimieTechTv.Domain.csproj src/backend/AnimieTechTv.Domain/
COPY src/backend/AnimieTechTv.Infrastructure/AnimieTechTv.Infrastructure.csproj src/backend/AnimieTechTv.Infrastructure/
COPY src/shared/AnimieTechTv.Communication/AnimieTechTv.Communication.csproj src/shared/AnimieTechTv.Communication/
COPY src/shared/AnimieTechTv.Exceptions/AnimieTechTv.Exceptions.csproj src/shared/AnimieTechTv.Exceptions/
COPY test/AnimieTechTv.Tests/AnimieTechTv.Tests.csproj test/AnimieTechTv.Tests/

RUN dotnet restore AnimieTechTV.sln

COPY . .

RUN dotnet publish src/backend/AnimieTechTv.API/AnimieTechTv.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AnimieTechTv.API.dll"]
