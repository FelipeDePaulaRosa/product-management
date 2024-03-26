using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Products.Contracts;
using Domain.Products.Entities;

namespace Application.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        
        public CreateProductHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        
        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.CreateAsync(product);
            return new CreateProductResponse();
        }
    }
}