using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddresses.Products)]
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
            return Ok(sections.ToDTO());
        }
        [HttpGet("section/{ID}")]
        public IActionResult GetSectionByID(int ID)
        {
            var section = _productData.GetSectionById(ID);
            if (section is null) 
                return NotFound();
            return Ok(section.ToDTO());
        }

        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = _productData.GetBrand();
            return Ok(brands.ToDTO());
        }

        [HttpGet("brand/{ID}")]
        public IActionResult GetBrandByID(int ID)
        {
            var brand = _productData.GetBrandById(ID);
            if (brand is null)
                return NotFound();
            return Ok(brand.ToDTO());
        }
        [HttpPost]
        public IActionResult GetProducts(ProductFilter? filter = null)
        {
            var products = _productData.GetProduct(filter);
            return Ok(products.ToDTO());
        }

        [HttpGet("{ID}")]
        public IActionResult GetProductByID(int ID)
        {
            var product = _productData.GetProductByID(ID);
            if (product is null)
                return NotFound();
            return Ok(product.ToDTO());
        }
        [HttpPost("new/{name}")]
        public IActionResult CreateProduct(CreateProductDTO model)
        {
            var product = _productData.CreateProduct(model.Name, model.Order, model.Price, model.ImageUrl, model.Section, model.Brand);
            return CreatedAtAction(nameof(GetProductByID), new { product.ID }, product.ToDTO());
        }
    }
}
