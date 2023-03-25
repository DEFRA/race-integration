#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RACE2.FrontEndWeb/RACE2.FrontEndWeb.csproj", "RACE2.FrontEndWeb/"]
COPY ["RACE2.DataModel/RACE2.DataModel.csproj", "RACE2.DataModel/"]
COPY ["RACE2.Dto/RACE2.Dto.csproj", "RACE2.Dto/"]
RUN dotnet restore "RACE2.FrontEndWeb/RACE2.FrontEndWeb.csproj"
COPY . .
WORKDIR "/src/RACE2.FrontEndWeb"
RUN dotnet build "RACE2.FrontEndWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RACE2.FrontEndWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RACE2.FrontEndWeb.dll"]