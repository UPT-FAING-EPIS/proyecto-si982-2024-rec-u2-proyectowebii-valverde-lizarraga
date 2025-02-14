using System;
using Xunit;
using GestionReservasWebII.Models;
using FluentAssertions;

namespace GestionReservasWebII.Tests.Models
{
    public class ErrorViewModelTest
    {
        [Fact]
        public void ErrorViewModel_ShouldShowRequestId_WhenRequestIdIsNotEmpty()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel
            {
                RequestId = "12345"
            };

            // Assert
            errorViewModel.ShowRequestId.Should().BeTrue();
        }

        [Fact]
        public void ErrorViewModel_ShouldNotShowRequestId_WhenRequestIdIsEmpty()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel
            {
                RequestId = ""
            };

            // Assert
            errorViewModel.ShowRequestId.Should().BeFalse();
        }
    }
}
