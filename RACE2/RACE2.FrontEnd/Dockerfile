FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY RACE2.FrontEnd.csproj .
RUN dotnet restore RACE2.FrontEnd.csproj
COPY . .
RUN dotnet build RACE2.FrontEnd.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish RACE2.FrontEnd.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf