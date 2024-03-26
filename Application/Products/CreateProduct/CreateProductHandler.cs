using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
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
            var code = await _productRepository.GetNextCode();
            product.SetCode(code);
            await _productRepository.CreateAsync(product);
            return new CreateProductResponse(product.Id);
        }
    }
}