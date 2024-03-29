FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RACE2.Api/RACE2.Api.csproj", "src/RACE2.Api/"]
COPY ["RACE2.Services/RACE2.Services.csproj", "src/RACE2.Services/"]
COPY ["RACE2.DataAccess/RACE2.DataAccess.csproj", "src/RACE2.DataAccess/"]
COPY ["RACE2.DataModel/RACE2.DataModel.csproj", "src/RACE2.DataModel/"]
RUN dotnet restore "src/RACE2.Api/RACE2.Api.csproj"
COPY . .
WORKDIR "/src/RACE2.Api"
RUN dotnet build "RACE2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RACE2.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RACE2.Api.dll"]
