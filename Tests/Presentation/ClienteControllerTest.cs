using Application.DataAccess;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Presentation.Controllers;

namespace Tests.Presentation;


[TestFixture]
public class ClienteControllerTest
{
    private readonly ClienteController _clienteController;

    private readonly Mock<IClienteService> _clienteService;


    public ClienteControllerTest()
    {
        _clienteService = new Mock<IClienteService>();
        _clienteController = new ClienteController( _clienteService.Object );
      
    }

    [Test]
    public void GetClienteByIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetClienteByIdNotFoundTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetClienteByIdInternalServerErrorTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddClienteOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddClienteDniExistsTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void AddClienteBadRequestTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

}
