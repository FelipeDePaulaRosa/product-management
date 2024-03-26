using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using Domain.Exceptions;
using System.Threading.Tasks;
using Domain.Products.Contracts;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Application.Products.GetProductByCode
{
    public class GetProductByCodeHandler : IRequestHandler<GetProductByCodeRequest, GetProductByCodeResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public GetProductByCodeHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        
        public async Task<GetProductByCodeResponse> Handle(GetProductByCodeRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .GetQueryable()
                .Where(x => x.Code == request.Code)
                .ProjectTo<GetProductByCodeResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            
            if(product is null)
                throw new ApiNotFoundException($"Product of code: '{request.Code}' not found");
            
            return product;
        }
    }
}