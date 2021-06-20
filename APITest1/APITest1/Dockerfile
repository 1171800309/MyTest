#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["APITest1/APITest1.csproj", "APITest1/"]
RUN dotnet restore "APITest1/APITest1.csproj"
COPY . .
WORKDIR "/src/APITest1"
RUN dotnet build "APITest1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APITest1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APITest1.dll"]