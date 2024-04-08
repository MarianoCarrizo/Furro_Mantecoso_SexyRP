using Application.Services.Interfaces;
using Domain.Entities;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Presentation;


[TestFixture]
public class OrdenControllerTest
{
    private readonly IOrdenService _ordenService;

   

    [Test]
    public void createOrderOkTest()
    {
        var result = _ordenService.CreateOrden(new Orden()); // Crear un Orden para que se asegure que se cree, implementar algun When para que devuelve ok??? aplica a todos.

        ClassicAssert.IsNotNull(result);
    }

}
