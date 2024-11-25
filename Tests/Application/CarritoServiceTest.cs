using Application.DataAccess;
using Application.Services;
using Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Tests.Application;

/// <summary>
/// Unit tests for CarritoService.
/// </summary>
[TestFixture]
public class CarritoServiceTests
{
    private Mock<ICarritoRepository> _mockRepository;
    private CarritoService _service;

    /// <summary>
    /// Sets up the test environment by initializing mocks and the service.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<ICarritoRepository>();
        _service = new CarritoService(_mockRepository.Object);
    }

    /// <summary>
    /// Tests that GetCarritoByClientId returns the expected carrito when found.
    /// </summary>
    [Test]
    public async Task GetCarritoByClientId_ShouldReturnCarrito_WhenFound()
    {
        // Arrange
        int clientId = 1;
        var expectedCarrito = new Carrito { CarritoId = Guid.NewGuid(), ClienteId = clientId };
        _mockRepository.Setup(repo => repo.GetCarritoByClientId(clientId))
            .ReturnsAsync(expectedCarrito);

        // Act
        var result = await _service.GetCarritoByClientId(clientId);

        // Assert
        Assert.That(result is not null);
        Assert.That(expectedCarrito == result);
        _mockRepository.Verify(repo => repo.GetCarritoByClientId(clientId), Times.Once);
    }

    /// <summary>
    /// Tests that GetCarritoById returns the expected carrito when found.
    /// </summary>
    [Test]
    public async Task GetCarritoById_ShouldReturnCarrito_WhenFound()
    {
        // Arrange
        var carritoId = Guid.NewGuid();
        var expectedCarrito = new Carrito { CarritoId = carritoId };
        _mockRepository.Setup(repo => repo.GetCarritoById(carritoId))
            .ReturnsAsync(expectedCarrito);

        // Act
        var result = await _service.GetCarritoById(carritoId);

        // Assert
        Assert.That(result is not null);
        Assert.That(expectedCarrito == result);
        _mockRepository.Verify(repo => repo.GetCarritoById(carritoId), Times.Once);
    }

    /// <summary>
    /// Tests that UpdateCarrito returns the updated carrito.
    /// </summary>
    [Test]
    public async Task UpdateCarrito_ShouldReturnUpdatedCarrito()
    {
        // Arrange
        var carrito = new Carrito { CarritoId = Guid.NewGuid() };
        _mockRepository.Setup(repo => repo.UpdateCarrito(carrito))
            .ReturnsAsync(carrito);

        // Act
        var result = await _service.UpdateCarrito(carrito);

        // Assert
        Assert.That(result is not null);
        Assert.That(carrito == result);
        _mockRepository.Verify(repo => repo.UpdateCarrito(carrito), Times.Once);
    }

    /// <summary>
    /// Tests that DeleteCarritoProducto returns the deleted carrito producto when successful.
    /// </summary>
    [Test]
    public async Task DeleteCarritoProducto_ShouldReturnDeletedProducto_WhenSuccessful()
    {
        // Arrange
        var carritoProducto = new CarritoProducto { CarritoId = Guid.NewGuid(), ProductoId = 1 };
        _mockRepository.Setup(repo => repo.DeleteCarritoProducto(carritoProducto))
            .ReturnsAsync(carritoProducto);

        // Act
        var result = await _service.DeleteCarritoProducto(carritoProducto);

        // Assert
        Assert.That(result is not null);
        Assert.That(carritoProducto == result);
        _mockRepository.Verify(repo => repo.DeleteCarritoProducto(carritoProducto), Times.Once);
    }

    /// <summary>
    /// Tests that DeleteCarrito returns the deleted carrito when it exists.
    /// </summary>
    [Test]
    public async Task DeleteCarrito_ShouldReturnDeletedCarrito_WhenFound()
    {
        // Arrange
        var carritoId = Guid.NewGuid();
        var carrito = new Carrito { CarritoId = carritoId };
        _mockRepository.Setup(repo => repo.GetRawCarritoById(carritoId))
            .ReturnsAsync(carrito);
        _mockRepository.Setup(repo => repo.DeleteCarritoById(carritoId))
            .ReturnsAsync(carrito);

        // Act
        var result = await _service.DeleteCarrito(carritoId);

        // Assert
        Assert.That(result is not null);
        Assert.That(carrito == result);
        _mockRepository.Verify(repo => repo.GetRawCarritoById(carritoId), Times.Once);
        _mockRepository.Verify(repo => repo.DeleteCarritoById(carritoId), Times.Once);
    }

    /// <summary>
    /// Tests that DeleteCarrito throws a FileNotFoundException when the carrito does not exist.
    /// </summary>
    [Test]
    public void DeleteCarrito_ShouldThrowFileNotFoundException_WhenCarritoNotFound()
    {
        // Arrange
        var carritoId = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.GetRawCarritoById(carritoId))
            .ReturnsAsync((Carrito)null); // Simulate carrito not found

        // Act & Assert
        var ex = Assert.ThrowsAsync<FileNotFoundException>(async () =>
            await _service.DeleteCarrito(carritoId));

        Assert.That(ex.Message, Is.EqualTo($"CarritoId : {carritoId} no encontrado"));
        _mockRepository.Verify(repo => repo.GetRawCarritoById(carritoId), Times.Once);
        _mockRepository.Verify(repo => repo.DeleteCarritoById(It.IsAny<Guid>()), Times.Never);
    }

    /// <summary>
    /// Tests that CreateCarrito returns the newly created carrito.
    /// </summary>
    [Test]
    public async Task CreateCarrito_ShouldReturnCreatedCarrito()
    {
        // Arrange
        var carrito = new Carrito { CarritoId = Guid.NewGuid() };
        _mockRepository.Setup(repo => repo.CreateCarrito(carrito))
            .ReturnsAsync(carrito);

        // Act
        var result = await _service.CreateCarrito(carrito);

        // Assert
        Assert.That(result is not null);
        Assert.That(carrito == result);
        _mockRepository.Verify(repo => repo.CreateCarrito(carrito), Times.Once);
    }
}
