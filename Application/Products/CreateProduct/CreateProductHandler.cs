﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        public Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new CreateProductResponse());
        }
    }
}