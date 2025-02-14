using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class ReservaTest
    {
        [Fact]
        public void ReservaModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var reserva = new Reserva
            {
                ReservaId = 10,
                UsuarioId = 1,
                RecursoId = 5,
                FechaReserva = DateTime.Now,
                HoraInicio = DateTime.Now.AddHours(1),
                HoraFin = DateTime.Now.AddHours(2),
                Estado = "Pendiente",
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            // Assert
            reserva.ReservaId.Should().Be(10);
            reserva.UsuarioId.Should().Be(1);
            reserva.RecursoId.Should().Be(5);
            reserva.Estado.Should().Be("Pendiente");
            reserva.FechaReserva.Should().BeAfter(DateTime.MinValue);
            reserva.HoraInicio.Should().BeAfter(reserva.FechaReserva);
            reserva.HoraFin.Should().BeAfter(reserva.HoraInicio);
            reserva.Evaluaciones.Should().BeEmpty();
        }
    }
}
