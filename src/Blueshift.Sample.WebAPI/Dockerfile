#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG DOCKER_REGISTRY=mcr.microsoft.com/

FROM ${DOCKER_REGISTRY}dotnet/aspnet:8.0 AS base 
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM ${DOCKER_REGISTRY}dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Blueshift.Sample.WebAPI/Blueshift.Sample.WebAPI.csproj", "src/Blueshift.Sample.WebAPI/"]
COPY ["src/Blueshift.Sample/Blueshift.Sample.csproj", "src/Blueshift.Sample/"]
COPY ["src/Blueshift.Sample.Adapters.Repositories.SqlServer/Blueshift.Sample.Adapters.Repositories.SqlServer.csproj", "src/Blueshift.Sample.Adapters.Repositories.SqlServer/"]
RUN dotnet restore "src/Blueshift.Sample.WebAPI/Blueshift.Sample.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Blueshift.Sample.WebAPI"
RUN dotnet build "Blueshift.Sample.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blueshift.Sample.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blueshift.Sample.WebApi.dll"]
