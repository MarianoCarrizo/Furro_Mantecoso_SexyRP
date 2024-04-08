using Application.DataAccess;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class OrdenServiceTest
{
    private Mock<IOrdenRepository> _OrdenRepository;


    [SetUp]
    public void SetUp()
    {
        _OrdenRepository = new Mock<IOrdenRepository>();
    }

    [Test]
    public void ordenRepositoryOkTest()
    {
        Assert.Pass();
    }

}
