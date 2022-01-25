using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : Controller
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = _productData.GetSection();
            return Ok(sections);
        }
        [HttpGet("section/{ID}")]
        public IActionResult GetSectionByID(int ID)
        {
            var section = _productData.GetSectionById(ID);
            if (section is null) 
                return NotFound();
            return Ok(section);
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = _productData.GetBrand();
            return Ok(brands);
        }

        [HttpGet("brand/{ID}")]
        public IActionResult GetBrandByID(int ID)
        {
            var brand = _productData.GetBrandById(ID);
            if (brand is null)
                return NotFound();
            return Ok(brand);
        }
        [HttpPost]
        public IActionResult GetProducts(ProductFilter? filter = null)
        {
            var products = _productData.GetProduct(filter);
            return Ok(products);
        }

        [HttpGet("{ID}")]
        public IActionResult GetProductByID(int ID)
        {
            var product = _productData.GetProductByID(ID);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost("new/{name}")]
        public IActionResult CreateProduct(string name, int order, decimal price, string imageURL, string section, string? brand = null)
        {
            var product = _productData.CreateProduct(name, order, price, imageURL, section, brand);
            return CreatedAtAction(nameof(GetProductByID), new { product.ID }, product);
        }
    }
}
