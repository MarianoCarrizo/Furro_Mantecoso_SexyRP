using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class OrdenServiceTest
{

    
    private readonly Mock<IOrdenRepository> _ordenRepository;



    public OrdenServiceTest()
    {
        _ordenRepository = new Mock<IOrdenRepository>();
       
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
