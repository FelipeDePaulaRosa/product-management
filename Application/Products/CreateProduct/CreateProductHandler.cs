using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Products;

namespace Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly IMapper _mapper;
        public CreateProductHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);   
            return Task.FromResult(new CreateProductResponse());
        }
    }
}