using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class NotificacionTest
    {
        [Fact]
        public void NotificacionModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var notificacion = new Notificacione
            {
                NotificacionId = 20,
                UsuarioId = 1,
                Titulo = "Reserva Confirmada",
                Mensaje = "Tu reserva ha sido confirmada.",
                FechaEnvio = DateTime.Now,
                Leido = false
            };

            // Assert
            notificacion.NotificacionId.Should().Be(20);
            notificacion.UsuarioId.Should().Be(1);
            notificacion.Titulo.Should().Be("Reserva Confirmada");
            notificacion.Mensaje.Should().Be("Tu reserva ha sido confirmada.");
            notificacion.FechaEnvio.Should().NotBeNull();
            notificacion.Leido.Should().BeFalse();
        }
    }
}
