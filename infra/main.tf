terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0.0"
    }
  }
  required_version = ">= 0.14.9"
}

variable "suscription_id" {
    type = string
    description = "83fff94f-ea6d-44c3-a294-e78a99bee2f9"
}

variable "sqladmin_username" {
    type = string
    description = "jeanvalverde"
}

variable "sqladmin_password" {
    type = string
    description = "valverde24c++"
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
  subscription_id = var.suscription_id
}


resource "azurerm_resource_group" "rg" {
  name     = "arg-proyecto-valverde-lizarraga"
  location = "brazilsouth"
}

resource "azurerm_service_plan" "appserviceplan" {
  name                = "asp-proyecto-valverde-lizarraga"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "webapp" {
  name                  = "app-proyecto-valverde-lizarraga"
  location              = azurerm_resource_group.rg.location
  resource_group_name   = azurerm_resource_group.rg.name
  service_plan_id       = azurerm_service_plan.appserviceplan.id
  depends_on            = [azurerm_service_plan.appserviceplan]
  
  site_config {
    minimum_tls_version = "1.2"
    always_on = false
    application_stack {
      docker_image_name = "patrickcuadros/shorten:latest"
      docker_registry_url = "https://index.docker.io"      
    }
  }
}

resource "azurerm_mssql_server" "sqlsrv" {
  name                         = "bd-proyecto-valverde-lizarraga"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = var.sqladmin_username
  administrator_login_password = var.sqladmin_password
}

resource "azurerm_mssql_firewall_rule" "sqlaccessrule" {
  name             = "PublicAccess"
  server_id        = azurerm_mssql_server.sqlsrv.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "255.255.255.255"
}

resource "azurerm_mssql_database" "sqldb" {
  name      = "shorten"
  server_id = azurerm_mssql_server.sqlsrv.id
  sku_name  = "Free"
}

resource "azurerm_dns_zone" "dns" {
  name                = "dns-proyecto-valverde-lizarraga.com"
  resource_group_name = azurerm_resource_group.rg.name
}

resource "azurerm_dns_cname_record" "cname" {
  name                = "www"
  zone_name           = azurerm_dns_zone.dns.name
  resource_group_name = azurerm_resource_group.rg.name
  ttl                 = 300
  record              = azurerm_linux_web_app.webapp.default_hostname
}
