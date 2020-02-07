FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY *.sln ./
COPY main/Port.Adapter/In/Api/In.Api.csproj main/Port.Adapter/In/Api/
COPY main/Application/Application.csproj main/Application/
COPY main/Domain.Model/Domain.Model.csproj main/Domain.Model/
COPY main/Port.Adapter/IO/Persistence/Events/SQLite/SQLite.csproj main/Port.Adapter/IO/Persistence/Events/SQLite/
COPY main/Port.Adapter/IO/Persistence/Events/Events.csproj main/Port.Adapter/IO/Persistence/Events/
RUN dotnet restore
COPY . .
WORKDIR /src/main/Port.Adapter/In/Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "works.ei8.Cortex.Diary.Nucleus.Port.Adapter.In.Api.dll"]