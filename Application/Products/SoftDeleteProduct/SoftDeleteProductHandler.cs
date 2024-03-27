using MediatR;
using System.Threading;
using Domain.Exceptions;
using System.Threading.Tasks;
using Domain.Products.Contracts;

namespace Application.Products.SoftDeleteProduct
{
    public class SoftDeleteProductHandler : IRequestHandler<SoftDeleteProductRequest, SoftDeleteProductResponse>
    {
        private readonly IProductRepository _productRepository;
        
        public SoftDeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<SoftDeleteProductResponse> Handle(SoftDeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdOrDefaultAsync(request.Id);
            
            if(product is null)
                throw new ApiNotFoundException($"Product of id:{request.Id} not found");
            
            product.SoftDelete();
            await _productRepository.SaveChangesAsync();
            return new SoftDeleteProductResponse();
        }
    }
}