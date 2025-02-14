using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class UsuarioTest
    {
        [Fact]
        public void UsuarioModel_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            var usuario = new Usuario
            {
                UsuarioId = 1,
                CodigoUniversitario = "U123456",
                Nombre = "Jean",
                Apellido = "Valverde",
                Correo = "jean@example.com",
                ContraseñaHash = "hashedpassword",
                Rol = "admin",
                Facultad = "Ingeniería",
                Carrera = "Sistemas",
                FechaRegistro = DateTime.Now,
                UltimaConexion = DateTime.Now
            };

            // Assert
            usuario.UsuarioId.Should().Be(1);
            usuario.CodigoUniversitario.Should().Be("U123456");
            usuario.Nombre.Should().Be("Jean");
            usuario.Apellido.Should().Be("Valverde");
            usuario.Correo.Should().Be("jean@example.com");
            usuario.ContraseñaHash.Should().Be("hashedpassword");
            usuario.Rol.Should().Be("admin");
            usuario.Facultad.Should().Be("Ingeniería");
            usuario.Carrera.Should().Be("Sistemas");
            usuario.FechaRegistro.Should().NotBeNull();
            usuario.UltimaConexion.Should().NotBeNull();
            usuario.Reservas.Should().BeEmpty();
            usuario.Notificaciones.Should().BeEmpty();
        }
    }
}
