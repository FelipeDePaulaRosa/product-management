using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Products.Contracts;
using MediatR;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdOrDefaultAsync(request.Id);
            
            if(product is null)
                throw new ApiNotFoundException($"Product of id:{request.Id} not found");
            
            _mapper.Map(request, product);
            
            await _productRepository.SaveChangesAsync();
            
            return new UpdateProductResponse();
        }
    }
}