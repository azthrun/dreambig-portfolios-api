FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 7288

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
WORKDIR /src
COPY ["src/DreamBig.Portfolios.Web.Api/DreamBig.Portfolios.Web.Api.csproj", "DreamBig.Portfolios.Web.Api/"]
COPY ["src/DreamBig.Portfolios.Web.Application/DreamBig.Portfolios.Web.Application.csproj", "DreamBig.Portfolios.Web.Application/"]
COPY ["src/DreamBig.Portfolios.Web.Domain/DreamBig.Portfolios.Web.Domain.csproj", "DreamBig.Portfolios.Web.Domain/"]
COPY ["src/DreamBig.Portfolios.Web.Infrastructure/DreamBig.Portfolios.Web.Infrastructure.csproj", "DreamBig.Portfolios.Web.Infrastructure/"]
COPY ["src/DreamBig.Portfolios.Web.Persistent.MySQL/DreamBig.Portfolios.Web.Persistent.MySQL.csproj", "DreamBig.Portfolios.Web.Persistent.MySQL/"]
RUN dotnet restore "DreamBig.Portfolios.Web.Api/DreamBig.Portfolios.Web.Api.csproj"
COPY ./src .
WORKDIR "/src/DreamBig.Portfolios.Web.Api"
RUN dotnet build "DreamBig.Portfolios.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DreamBig.Portfolios.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=7288
EXPOSE 7288
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./DreamBig.Portfolios.Web.Api"]
