using Application.DataAccess;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Presentation.Controllers;

namespace Tests.Presentation;


[TestFixture]
public class OrdenControllerTest
{
    private readonly OrdenController _ordenController;
    private readonly Mock<ICarritoService> _carritoService;
    private readonly Mock<IClienteService> _clienteService;
    private readonly Mock<IOrdenService> _ordenService;
    private readonly Mock<IMapper> _mapper;


    public OrdenControllerTest()
    {
        _carritoService = new Mock<ICarritoService>();
        _clienteService = new Mock<IClienteService>();
        _ordenService = new Mock<IOrdenService>();
        _mapper = new Mock<IMapper>();
        _ordenController = new OrdenController(_carritoService.Object, _ordenService.Object,_clienteService.Object, _mapper.Object);
    }

    [Test]
    public void AddOrdenOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddOrdenClientNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddOrdenEmptyCarritoTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddOrdenBadRequestTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetOrdenesOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetOrdenesNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }



}
