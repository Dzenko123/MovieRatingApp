FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY MovieRating.sln ./
COPY MovieRating/ ./MovieRating/
COPY MovieRating.Model/ ./MovieRating.Model/
COPY MovieRating.Services/ ./MovieRating.Services/

WORKDIR /src/MovieRating

RUN dotnet restore ../MovieRating.sln
RUN dotnet build "MovieRating.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MovieRating.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieRating.dll"]
