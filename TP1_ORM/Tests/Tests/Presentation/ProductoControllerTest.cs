using Application.Services.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Presentation;


[TestFixture]
public class ProductoControllerTest
{
    private readonly IProductService _productService;

   
    [Test]
    public void findRawProductByIdOkTest()
    {
        var result = _productService.FindRawProductById(1);

        ClassicAssert.IsNotNull(result);
    }

}
