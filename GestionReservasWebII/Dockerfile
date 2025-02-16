# Etapa base para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ✅ Corregido: Copiar directamente el archivo de proyecto SIN una subcarpeta adicional
COPY GestionReservasWebII.csproj ./

# Restaurar paquetes
RUN dotnet restore "GestionReservasWebII.csproj"

# Copiar el resto del código fuente
COPY . .

# Establecer directorio de trabajo en la raíz del código
WORKDIR "/src"

# Compilar la aplicación
RUN dotnet build "GestionReservasWebII.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar la aplicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GestionReservasWebII.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestionReservasWebII.dll"]
