using Microsoft.AspNetCore.Mvc;
using Product_Management.DTOs;
using Product_Management.Services;

namespace Product_Management.Controller
{
    [ApiController]
    [Route("api/[Controller]")]

    public class ProductsController : ControllerBase
    {
        protected readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/api/Products")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetActiveProducts()
        {
            var products = await _service.GetActiveProductsAsync();

            return Ok(products);
        }


        [HttpGet]
        [Route("Name/{name}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductByName(string name)
        {
            var products = await _service.GetDataByNameAsync(name);

            return Ok(products);
        }

        [HttpGet]
        [Route("Category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(string categoryName)
        {
            var products = await _service.GetProductsByCategory(categoryName);

            return Ok(products);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ProductDTO>> AddProduct(CreateProductDTO data)
        {
            var newProduct = await _service.AddDataAsync(data);

            return CreatedAtAction(nameof(GetProductByName), new { name = newProduct.ProductName }, newProduct);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(UpdateProductDTO data)
        {
            var product = await _service.UpdateDataAsync(data);

            return Ok(product);
        }
    }
    }
