using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrossCutting.Extensions;
using Domain.Products.Entities;
using Domain.Products.Contracts;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Application.Products.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var query = _productRepository
                .GetQueryable()
                .Where(x => x.IsActive == true);
            
            var products = await ApplyFilter(query, request)
                .ProjectTo<GetProducts>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            var pagedProducts = products.ToPagedList(request.PageNumber, request.PageSize);
            return new GetProductsResponse(pagedProducts);
        }

        private static IQueryable ApplyFilter(IQueryable<Product> query, GetProductsRequest request)
        {
            if(request.StartCode != null)
                query = query.Where(x => x.Code >= request.StartCode);
            
            if(request.EndCode != null)
                query = query.Where(x => x.Code <= request.EndCode);
            
            if(request.Description != null)
                query = query.Where(x => x.Description.Contains(request.Description));
            
            if(request.SupplierCode != null)
                query = query.Where(x => x.Supplier.Code == request.SupplierCode);
            
            if(request.StartManufactureDate != null)
                query = query.Where(x => x.ManufactureDate >= request.StartManufactureDate);
            
            if(request.EndManufactureDate != null)
                query = query.Where(x => x.ManufactureDate <= request.EndManufactureDate);
            
            if(request.StartDueDate != null)
                query = query.Where(x => x.DueDate >= request.StartDueDate);
            
            if(request.EndDueDate != null)
                query = query.Where(x => x.DueDate <= request.EndDueDate);
            
            if(request.SupplierCnpj != null)
                query = query.Where(x => x.Supplier.Cnpj.Equals(request.SupplierCnpj));
            
            if(request.SupplierDescription != null)
                query = query.Where(x => x.Supplier.Description.Contains(request.SupplierDescription));
            
            return query;
        }
    }
}