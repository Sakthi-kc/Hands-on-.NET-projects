using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Product_Management.DTOs;
using Product_Management.Services;

namespace Product_Management.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ProductsController : ControllerBase
    {
        protected readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetActiveProducts()
        {
            var products = await _service.GetActiveProductsAsync();

            return Ok(products);
        }


        [HttpGet]
        [Route("Name/{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductByName(string name)
        {
            var product = await _service.GetProductByNameAsync(name);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(string categoryName)
        {
            var products = await _service.GetProductsByCategory(categoryName);

            if (products.IsNullOrEmpty())
                return NotFound("Products not found, please re-verify the category name");


            return Ok(products);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDTO>> AddProduct(CreateProductDTO data)
        {
            var newProduct = await _service.AddDataAsync(data);

            return CreatedAtAction(nameof(GetProductByName), new { name = newProduct.ProductName }, newProduct);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Update")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(UpdateProductDTO data)
        {
            var product = await _service.UpdateDataAsync(data);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }
    }
    }
