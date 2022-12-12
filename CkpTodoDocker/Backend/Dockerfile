FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
MAINTAINER "INF-NIES-SEM3-RA2223"

WORKDIR /source

COPY CkpTodoApp/*.csproj .
RUN dotnet restore -r linux-musl-x64

COPY CkpTodoApp/. .
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine-amd64
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./CkpTodoApp"]