using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class OrdenServiceTest
{
   
    private readonly IOrdenService _ordenService;
    private readonly Mock<IOrdenRepository> _ordenRepository;


    
    public OrdenServiceTest()
    {
        _ordenRepository = new Mock<IOrdenRepository>();  
        _ordenService = new OrdenService(_ordenRepository.Object);
    }

    [Test]
    public void CreateOrdenTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetOrderOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

   


}
