using Application.Services.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Presentation;


[TestFixture]
public class ClienteControllerTest
{
    private readonly IClienteService _clienteService;

   

    [Test]
    public void findClientByIdOkTest()
    {
        var result = _clienteService.GetClienteById(1);

        ClassicAssert.IsNotNull(result);
    }

}
