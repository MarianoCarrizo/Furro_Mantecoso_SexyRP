using Application.Services.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Presentation;


[TestFixture]
public class ClienteServiceTest
{
    private readonly ICarritoService _carritoService;

   

    [Test]
    public void getCarritoByClientIdOkTest()
    {
        var result = _carritoService.GetCarritoByClientId(1);

        ClassicAssert.IsNotNull(result);
    }

}
