using Application.DataAccess;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Presentation.Controllers;

namespace Tests.Presentation;


[TestFixture]
public class CarritoControllerTest
{
    private readonly CarritoController _carritoController;
    private readonly Mock<ICarritoService> _carritoService;
    private readonly Mock<IProductService> _productService;
    private readonly Mock<IClienteService> _clienteService;
    private readonly Mock<IMapper> _mapper;


    public CarritoControllerTest()
    {
        _carritoService = new Mock<ICarritoService>();
        _clienteService = new Mock<IClienteService>();
        _productService = new Mock<IProductService>();
        _mapper = new Mock<IMapper>();
        _carritoController = new CarritoController(_carritoService.Object, _productService.Object, _clienteService.Object, _mapper.Object);
    }

    [Test]
    public void getCarritoByIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void getCarritoByIdNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void getCarritoByIdInternalServerErrorTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddProductoOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddProductoClientIdNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddProductoIdNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddProductoNegativeOrOneQuantityTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddProductoBadRequestTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void ModifyProductOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void ModifyProductIdNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void ModifyProductoNegativeOrOneQuantityTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void ModifyProductoEmptyCarritoTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void ModifyProductoProductNotInCarritoTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void ModifyProductoBadRequestTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void DeleteProductoClientNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void DeleteProductoProductoNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void DeleteProductoCarritoNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void DeleteProductoProductNotInCarritoTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void DeleteProductoBadRequestTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

}
