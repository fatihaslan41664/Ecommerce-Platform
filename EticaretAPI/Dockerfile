# 1. .NET SDK image (build için)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 2. Solution dosyasýný ve projeleri kopyala
COPY *.sln ./
COPY EticaretAPI.Application/EticaretAPI.Application.csproj ./EticaretAPI.Application/
COPY EticaretAPI.Domain/EticaretAPI.Domain.csproj ./EticaretAPI.Domain/
COPY EticaretAPI.Infrastructure/EticaretAPI.Infrastructure.csproj ./EticaretAPI.Infrastructure/
COPY EticaretAPI.Persistence/EticaretAPI.Persistence.csproj ./EticaretAPI.Persistence/
COPY EticaretAPI.API/EticaretAPI.API.csproj ./EticaretAPI.API/

# 3. Restore iþlemi
RUN dotnet restore

# 4. Tüm dosyalarý kopyala
COPY . .

# 5. API projesini publish et
WORKDIR /app/EticaretAPI.API
RUN dotnet publish -c Release -o out

# 6. Runtime image (çalýþtýrmak için)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/EticaretAPI.API/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "EticaretAPI.API.dll"]
