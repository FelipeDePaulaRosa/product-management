﻿using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Products.GetProducts;
using Application.Products.CreateProduct;
using Application.Products.GetProductByCode;
using Application.Products.SoftDeleteProduct;
using Application.Products.UpdateProduct;
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
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
        {
            request.Id = id;
            var updatedProduct = await Sender.Send(request);
            return Ok(updatedProduct);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDeleteProduct([FromRoute] Guid id)
        {
            var request = new SoftDeleteProductRequest(id);
            await Sender.Send(request);
            return NoContent();
        }
    }
}