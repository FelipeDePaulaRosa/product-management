using System;

namespace Application.Products.GetProductByCode
{
    public class GetProductByCodeResponse
    {
        public long Code { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime ManufactureDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public GetProductSupplier Supplier { get; private set; }
    }
    
    public class GetProductSupplier {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Cnpj { get; set; }
        
        public GetProductSupplier(string code, string description, string cnpj)
        {
            Code = code;
            Description = description;
            Cnpj = cnpj;
        }
    }
}