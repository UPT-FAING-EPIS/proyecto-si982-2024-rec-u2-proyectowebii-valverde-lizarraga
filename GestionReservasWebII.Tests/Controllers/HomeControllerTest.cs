using GestionReservasWebII.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Xunit;

namespace GestionReservasWebII.Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _controller = new HomeController();
        }

        [Fact]
        public void Index_GetRequest_ShouldReturnViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Theory]
        [InlineData("student", "DashboardUsuario")]
        [InlineData("teacher", "DocenteDashboard")]
        [InlineData("admin", "DashboardAdministrador")]
        public void Index_PostRequest_WithRole_ShouldRedirectToExpectedDashboard(string role, string expectedAction)
        {
            // Act
            var result = _controller.Index(role) as RedirectToActionResult;

            // Assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be(expectedAction);
            result.ControllerName.Should().Be("Usuario");
        }

        [Fact]
        public void Index_PostRequest_WithInvalidRole_ShouldReturnViewResult()
        {
            // Act
            var result = _controller.Index("invalidRole");

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}
