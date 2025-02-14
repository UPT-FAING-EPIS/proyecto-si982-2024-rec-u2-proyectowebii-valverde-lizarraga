using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class IncidenciaTest
    {
        [Fact]
        public void IncidenciaModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var incidencia = new Incidencia
            {
                IncidenciaId = 1,
                RecursoId = 5,
                UsuarioId = 100,
                Descripcion = "Pantalla dañada",
                FechaReporte = DateTime.Now,
                Estado = "Pendiente",
                FechaResolucion = null
            };

            // Assert
            incidencia.IncidenciaId.Should().Be(1);
            incidencia.RecursoId.Should().Be(5);
            incidencia.UsuarioId.Should().Be(100);
            incidencia.Descripcion.Should().Be("Pantalla dañada");
            incidencia.FechaReporte.Should().NotBeNull();
            incidencia.Estado.Should().Be("Pendiente");
            incidencia.FechaResolucion.Should().BeNull();
        }
    }
}
