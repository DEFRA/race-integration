FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY RACE2.FrontEnd/RACE2.FrontEnd.csproj RACE2.FrontEnd/
COPY RACE2.DataAccess/RACE2.DataAccess.csproj RACE2.DataAccess/
COPY RACE2.DataModel/RACE2.DataModel.csproj RACE2.DataModel/
COPY RACE2.Dto/RACE2.Dto.csproj RACE2.Dto/
RUN dotnet restore RACE2.FrontEnd/RACE2.FrontEnd.csproj
COPY . .
RUN dotnet build RACE2.FrontEnd/RACE2.FrontEnd.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish RACE2.FrontEnd/RACE2.FrontEnd.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY RACE2.FrontEnd/wwwroot/appSettings.Production.json ./appSettings.json
COPY RACE2.FrontEnd/nginx.conf /etc/nginx/nginx.conf