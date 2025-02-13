terraform { 
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0.0"
    }
    null = {
      source = "hashicorp/null"
    }
  }
  required_version = ">= 0.14.9"
}

provider "azurerm" {
  features {}
  subscription_id = var.subscription_id
}

# Grupo de recursos en Azure con nombre fijo
resource "azurerm_resource_group" "rg" {
  name     = "upt-proyecto-valverde-lizarrag"
  location = "westus"
}

# Plan de servicio de aplicaciones
resource "azurerm_service_plan" "appserviceplan" {
  name                = "upt-proyecto-valverde-lizarrag"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Linux"
  sku_name            = "B1"
}

# Aplicación web en Azure App Service
# Aplicación web en Azure App Service
resource "azurerm_linux_web_app" "webapp" {
  name                  = "upt-proyecto-valverde-lizarrag"
  location              = azurerm_resource_group.rg.location
  resource_group_name   = azurerm_resource_group.rg.name
  service_plan_id       = azurerm_service_plan.appserviceplan.id
  depends_on            = [azurerm_service_plan.appserviceplan]

  site_config {
    minimum_tls_version = "1.2"
    always_on           = true
    application_stack {
      dotnet_version = "8.0"
    }
  }
}

# Dominio DNS personalizado en Azure
resource "azurerm_dns_zone" "dns" {
  name                = "valverdelizarraga.azurewebsites.net"
  resource_group_name = azurerm_resource_group.rg.name
}

# Registro CNAME para la aplicación web
resource "azurerm_dns_cname_record" "cname" {
  name                = "www"
  zone_name           = azurerm_dns_zone.dns.name
  resource_group_name = azurerm_resource_group.rg.name
  ttl                 = 300
  record              = azurerm_linux_web_app.webapp.default_hostname
}

# Conexión a la base de datos existente en Azure SQL Server
resource "azurerm_mssql_server" "sqlserver" {
  name                         = "svrjvalverde"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = var.sqladmin_username
  administrator_login_password = var.sqladmin_password
}

resource "azurerm_mssql_database" "sqldb" {
  name           = "BDWEBIIPROYECTO"
  server_id      = azurerm_mssql_server.sqlserver.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  license_type   = "LicenseIncluded"
  max_size_gb    = 5
  sku_name       = "Basic"
}

# Infracost para cálculo de costos
resource "null_resource" "infracost" {
  provisioner "local-exec" {
    command = <<EOT
      infracost breakdown --path . --format table
    EOT
  }
}

# Variables para manejar recursos existentes
variable "subscription_id" {
  description = "ID de la suscripción de Azure"
  type        = string
}

variable "sqladmin_username" {
  description = "Usuario administrador de SQL Server"
  type        = string
}

variable "sqladmin_password" {
  description = "Contraseña del usuario administrador de SQL Server"
  type        = string
  sensitive   = true
}
