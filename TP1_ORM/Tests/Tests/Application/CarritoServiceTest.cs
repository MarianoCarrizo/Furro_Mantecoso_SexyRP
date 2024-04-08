using Application.DataAccess;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class CarritoServiceTest
{
    private Mock<ICarritoRepository> _CarritoRepository;


    [SetUp]
    public void SetUp()
    {
        _CarritoRepository = new Mock<ICarritoRepository>();
    }

    [Test]
    public void carritoRepositoryOkTest()
    {
        Assert.Pass();
    }

}
