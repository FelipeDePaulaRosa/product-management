using System;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductRequest : IRequest<UpdateProductResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime DueDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCnpj { get; set; }
    }
}