# VideoGameApi/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the csproj first (leverages Docker cache)
COPY ["VideoGameApi.csproj", "./"]
RUN dotnet restore "VideoGameApi.csproj"

# Copy the rest of the source
COPY . .

# Build and publish
WORKDIR "/src"
RUN dotnet build "VideoGameApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VideoGameApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VideoGameApi.dll"]
