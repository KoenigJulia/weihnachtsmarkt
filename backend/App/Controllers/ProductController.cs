using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Model.Product;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ITransactionProvider _transactionProvider;

    public ProductController(ILogger<ProductController> logger, IMapper mapper, IProductService productService,
        ITransactionProvider transactionProvider)
    {
        _productService = productService;
        _transactionProvider = transactionProvider;
    }

    [HttpGet]
    [Route("product")]
    public async Task<IActionResult> GetById(string productId)
    {
        Product? product;

        if (string.IsNullOrWhiteSpace(productId) ||
            (product = await _productService.GetProductById(new ObjectId(productId))) == null)
            return BadRequest();

        return Ok(product);
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<Product>>> GetAll()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpPost]
    [Route("product")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0 ||
            string.IsNullOrWhiteSpace(request.VendorId))
            return BadRequest();

        using var transaction = await _transactionProvider.BeginTransaction();
        var product = await _productService.AddProduct(request.Name, request.Price, new ObjectId(request.VendorId));
        await transaction.CommitAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id.ToString() }, product);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var product = await _productService.GetProductById(new ObjectId(id));
        if (product == null) return BadRequest();

        await _productService.DeleteProduct(new ObjectId(id));
        return Ok(true);
    }
}