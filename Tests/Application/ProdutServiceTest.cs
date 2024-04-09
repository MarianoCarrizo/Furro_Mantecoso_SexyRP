using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Tests.Application;


[TestFixture]
public class ProdutServiceTest
{
   
    private readonly IProductService _productService;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IProductRepository> _productRepository;


    
    public ProdutServiceTest()
    {
        _productRepository = new Mock<IProductRepository>();    
        _mapper = new Mock<IMapper>();
        _productService = new ProductService(_productRepository.Object, _mapper.Object);
    }

    [Test]
    public void FindProductByIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void FindRawProductByIdOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }

    [Test]
    public void GetProductsOkTest()
    {
        //Arrange
        //Act
        //Assert
        Assert.Pass();
    }


}
