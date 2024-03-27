using System;
using MediatR;

namespace Application.Products.SoftDeleteProduct
{
    public class SoftDeleteProductRequest : IRequest<SoftDeleteProductResponse>
    {
        public SoftDeleteProductRequest(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}