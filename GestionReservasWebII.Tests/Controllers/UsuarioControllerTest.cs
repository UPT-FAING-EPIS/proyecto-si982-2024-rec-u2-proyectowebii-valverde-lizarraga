using GestionReservasWebII.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Xunit;

namespace GestionReservasWebII.Tests
{
    public class UsuarioControllerTests
    {
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _controller = new UsuarioController();
        }

        [Fact]
        public void DashboardUsuario_ShouldReturnViewResult()
        {
            // Act
            var result = _controller.DashboardUsuario();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void DocenteDashboard_ShouldReturnViewResult()
        {
            // Act
            var result = _controller.DocenteDashboard();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void DashboardAdministrador_ShouldReturnViewResult()
        {
            // Act
            var result = _controller.DashboardAdministrador();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}
