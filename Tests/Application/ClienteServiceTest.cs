using Application.DataAccess;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class ClienteServiceTest
{
    private Mock<IClienteRepository> _ClienteRepository;


    [SetUp]
    public void SetUp()
    {
        _ClienteRepository = new Mock<IClienteRepository>();
    }

    [Test]
    public void clienteRepositoryOkTest()
    {
        Assert.Pass();
    }

}
