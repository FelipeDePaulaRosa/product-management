using MediatR;

namespace Application.Products.CreateProduct
{
    public class CreateProductRequest : IRequest<CreateProductResponse>
    {
        public string Description { get; set; }
        public string ManufactureDate { get; set; }
        public string DueDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCnpj { get; set; }
    }
}