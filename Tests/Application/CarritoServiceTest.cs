using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class CarritoServiceTest
{
    private readonly ICarritoService _carritoService;
    private readonly Mock<ICarritoRepository> _carritoRepository;



    
    public CarritoServiceTest()
    {
        _carritoRepository = new Mock<ICarritoRepository>();
        _carritoService = new CarritoService(_carritoRepository.Object);
    }

    [Test]
    public void GetCarritoByClientIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetCarritoByIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void UpdateCarritoOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void DeleteCarritoProductoOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetCarritoProductoByIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void CreateCarritoOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void UpdateCarritoProductoOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetRawCarritoByIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }



}
