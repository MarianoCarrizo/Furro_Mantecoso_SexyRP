using Application.DataAccess;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Presentation.Controllers;

namespace Tests.Presentation;


[TestFixture]
public class ProductoControllerTest
{
  
    private readonly ProductoController _productoController;
    private readonly Mock<IProductService> _productService;


    public ProductoControllerTest()
    {
        _productService = new Mock<IProductService>();
        _productoController = new ProductoController(_productService.Object);

    }

    [Test]
    public void GetProductOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetProductNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetProductInternalServerErrorTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetProductosOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetProductosNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }


}
