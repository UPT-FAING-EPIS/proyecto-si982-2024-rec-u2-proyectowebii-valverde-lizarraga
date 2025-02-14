using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class RecursoTest
    {
        [Fact]
        public void RecursoModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var recurso = new Recurso
            {
                RecursoId = 3,
                Nombre = "Sala de reuniones",
                Tipo = "Sala",
                Descripcion = "Una sala de reuniones con capacidad para 10 personas.",
                Estado = "Disponible",
                Ubicacion = "Edificio B",
                Caracteristicas = "Pizarra, Proyector",
                FechaRegistro = DateTime.Now,
                UltimaActualizacion = DateTime.Now
            };

            // Assert
            recurso.RecursoId.Should().Be(3);
            recurso.Nombre.Should().Be("Sala de reuniones");
            recurso.Tipo.Should().Be("Sala");
            recurso.Descripcion.Should().Be("Una sala de reuniones con capacidad para 10 personas.");
            recurso.Estado.Should().Be("Disponible");
            recurso.Ubicacion.Should().Be("Edificio B");
            recurso.Caracteristicas.Should().Be("Pizarra, Proyector");
            recurso.FechaRegistro.Should().NotBeNull();
            recurso.UltimaActualizacion.Should().NotBeNull();
            recurso.Reservas.Should().BeEmpty();
        }
    }
}
