using System;
using System.Collections.Generic;
using CrossCutting.Paged;

namespace Application.Products.GetProducts
{
    public class GetProductsResponse
    {
        public PagedListResponse<GetProducts> Products { get; private set; }
        
        public GetProductsResponse(PagedListResponse<GetProducts> products)
        {
            Products = products;
        }
    }
    
    public class GetProducts {
        public long Code { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime ManufactureDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public GetProductsSupplier Supplier { get; private set; }
    }
    
    public class GetProductsSupplier {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Cnpj { get; set; }
        
        public GetProductsSupplier(string code, string description, string cnpj)
        {
            Code = code;
            Description = description;
            Cnpj = cnpj;
        }
    }
}