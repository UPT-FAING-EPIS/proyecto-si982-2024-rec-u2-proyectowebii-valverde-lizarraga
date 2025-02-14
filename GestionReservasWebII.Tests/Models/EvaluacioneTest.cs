using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class EvaluacioneTest
    {
        [Fact]
        public void EvaluacioneModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var evaluacion = new Evaluacione
            {
                EvaluacionId = 1,
                ReservaId = 10,
                UsuarioId = 100,
                Puntuacion = 5,
                Comentarios = "Excelente servicio",
                FechaEvaluacion = DateTime.Now
            };

            // Assert
            evaluacion.EvaluacionId.Should().Be(1);
            evaluacion.ReservaId.Should().Be(10);
            evaluacion.UsuarioId.Should().Be(100);
            evaluacion.Puntuacion.Should().Be(5);
            evaluacion.Comentarios.Should().Be("Excelente servicio");
            evaluacion.FechaEvaluacion.Should().NotBeNull();
        }
    }
}
