using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class ClienteServiceTest
{
    private readonly IClienteService _clienteService;
    private readonly Mock<IClienteRepository> _clienteRepository;
    private readonly Mock<IMapper> _mapper;

    public ClienteServiceTest()
    {
        _clienteRepository = new Mock<IClienteRepository>();
        _mapper = new Mock<IMapper>();
        _clienteService = new ClienteService(_clienteRepository.Object, _mapper.Object);
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
    public void CreateClientOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void CreateClientClientExistsTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

}
