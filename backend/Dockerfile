FROM mongo:6.0.3-focal AS base
EXPOSE 27017

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN apt update && apt install -y clang zlib1g-dev 
COPY ["App/MongoDBWeihnachtsmarktApp.csproj", "App/"]
RUN dotnet restore "App/MongoDBWeihnachtsmarktApp.csproj"
COPY . .
WORKDIR "/src/App"
RUN dotnet build "MongoDBWeihnachtsmarktApp.csproj" -c Release -r linux-x64 -o /app/build -p:PublishAot=true

FROM build AS publish
RUN dotnet publish "MongoDBWeihnachtsmarktApp.csproj" -c Release -r linux-x64 -o /app/publish -p:PublishAot=true


FROM base AS final
WORKDIR /mongo
COPY --from=publish /app/publish .
RUN chmod +x MongoControl
ENTRYPOINT ["/mongo/App"]