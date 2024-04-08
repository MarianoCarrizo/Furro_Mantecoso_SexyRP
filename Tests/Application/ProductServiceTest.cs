using Application.DataAccess;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class ProductServiceTest
{
    private Mock<IProductRepository> _ProductRepository;


    [SetUp]
    public void SetUp()
    {
        _ProductRepository = new Mock<IProductRepository>();
    }

    [Test]
    public void productRepositoryOkTest()
    {
        Assert.Pass();
    }

}
