using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class LogsAuditoriumTest
    {
        [Fact]
        public void LogsAuditoriumModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var log = new LogsAuditorium
            {
                LogId = 1,
                UsuarioId = 100,
                Accion = "Inicio de sesión",
                FechaAccion = DateTime.Now,
                Detalles = "Usuario autenticado correctamente"
            };

            // Assert
            log.LogId.Should().Be(1);
            log.UsuarioId.Should().Be(100);
            log.Accion.Should().Be("Inicio de sesión");
            log.FechaAccion.Should().NotBeNull();
            log.Detalles.Should().Be("Usuario autenticado correctamente");
        }
    }
}
