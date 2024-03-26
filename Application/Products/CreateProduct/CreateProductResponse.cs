using System;

namespace Application.Products.CreateProduct
{
    public class CreateProductResponse
    {
        public CreateProductResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}