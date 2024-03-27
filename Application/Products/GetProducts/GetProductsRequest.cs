using System;
using CrossCutting.Paged;
using MediatR;

namespace Application.Products.GetProducts
{
    public class GetProductsRequest : PagedQueryRequest, IRequest<GetProductsResponse>
    {
        public GetProductsRequest(int pageNumber,
            int pageSize,
            long? startCode,
            long? endCode,
            string description,
            string supplierCode,
            DateTime? startManufactureDate,
            DateTime? endManufactureDate,
            DateTime? startDueDate,
            DateTime? endDueDate,
            string supplierCnpj,
            string supplierDescription) : base(pageNumber, pageSize)
        {
            StartCode = startCode;
            EndCode = endCode;
            Description = description;
            SupplierCode = supplierCode;
            StartManufactureDate = startManufactureDate;
            EndManufactureDate = endManufactureDate;
            StartDueDate = startDueDate;
            EndDueDate = endDueDate;
            SupplierCnpj = supplierCnpj;
            SupplierDescription = supplierDescription;
        }

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