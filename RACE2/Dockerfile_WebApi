#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RACE2.WebApi/RACE2.WebApi.csproj", "RACE2.WebApi/"]
COPY ["RACE2.Services/RACE2.Services.csproj", "RACE2.Services/"]
COPY ["RACE2.DataAccess/RACE2.DataAccess.csproj", "RACE2.DataAccess/"]
COPY ["RACE2.Dto/RACE2.Dto.csproj", "RACE2.Dto/"]
COPY ["RACE2.DataModel/RACE2.DataModel.csproj", "RACE2.DataModel/"]
COPY ["RACE2.Logging/RACE2.Logging.csproj", "RACE2.Logging/"]
RUN dotnet restore "RACE2.WebApi/RACE2.WebApi.csproj"
COPY . .
WORKDIR "/src/RACE2.WebApi"
RUN dotnet build "RACE2.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RACE2.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RACE2.WebApi.dll"]