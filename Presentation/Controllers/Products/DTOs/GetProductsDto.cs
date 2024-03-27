using System;
using CrossCutting.Paged;

namespace Presentation.Controllers.Products.DTOs
{
    public class GetProductsDto : QueryPagedParameters
    {
        public long? StartCode { get; set; }
        public long? EndCode { get; set; }
        public string Description { get; set; }
        public string SupplierCode { get; set; }
        public DateTime? StartManufactureDate { get; set; }
        public DateTime? EndManufactureDate { get; set; }
        public DateTime? StartDueDate { get; set; }
        public DateTime? EndDueDate { get; set; }
        public string SupplierCnpj { get; set; }
        public string SupplierDescription { get; set; }
    }
}