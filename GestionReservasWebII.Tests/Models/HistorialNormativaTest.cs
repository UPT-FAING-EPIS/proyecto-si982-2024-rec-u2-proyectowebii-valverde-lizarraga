using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class HistorialNormativaTest
    {
        [Fact]
        public void HistorialNormativaModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var normativa = new HistorialNormativa
            {
                NormativaId = 1,
                UsuarioId = 100,
                FechaAceptacion = DateTime.Now
            };

            // Assert
            normativa.NormativaId.Should().Be(1);
            normativa.UsuarioId.Should().Be(100);
            normativa.FechaAceptacion.Should().NotBeNull();
        }
    }
}
