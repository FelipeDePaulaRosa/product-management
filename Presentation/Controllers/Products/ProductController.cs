using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Products.CreateProduct;
using Application.Products.GetProductByCode;
using Application.Products.GetProducts;
using CrossCutting.Paged;
using Presentation.Controllers.Products.DTOs;

namespace Presentation.Controllers.Products
{
    public class ProductController : ApiController
    {
        public ProductController(ISender sender) : base(sender)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var createdProduct = await Sender.Send(request);
            return Created(nameof(CreateProduct), createdProduct);
        }
        
        [HttpGet("{code}")]
        public async Task<IActionResult> GetProduct(long code)
        {
            var request = new GetProductByCodeRequest(code);
            var product = await Sender.Send(request);
            return Ok(product);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsDto dto)
        {
            var request = new GetProductsRequest(dto.PageNumber,
                dto.PageSize,
                dto.StartCode,
                dto.EndCode,
                dto.Description,
                dto.SupplierCode,
                dto.StartManufactureDate,
                dto.EndManufactureDate,
                dto.StartDueDate,
                dto.EndDueDate,
                dto.SupplierCnpj,
                dto.SupplierDescription);
            
            var products = await Sender.Send(request);
            return Ok(products);
        }
    }
}