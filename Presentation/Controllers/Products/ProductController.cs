using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Products.CreateProduct;

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
    }
}