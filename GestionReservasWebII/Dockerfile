# Etapa Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de Construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ✅ Copia correctamente el archivo de proyecto
COPY GestionReservasWebII/GestionReservasWebII.csproj GestionReservasWebII/
RUN dotnet restore "GestionReservasWebII/GestionReservasWebII.csproj"

# ✅ Copia todos los archivos de la aplicación
COPY . .
WORKDIR "/src/GestionReservasWebII"

# ✅ Construcción de la aplicación
RUN dotnet build "GestionReservasWebII.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de Publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GestionReservasWebII.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa Final (Imagen Ejecutable)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestionReservasWebII.dll"]
