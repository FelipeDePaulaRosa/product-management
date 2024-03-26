using Application.Products.CreateProduct;
using AutoMapper;
using Domain.Products;

namespace Presentation.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(dest => dest.Code, opt => opt.Ignore()) // Ignore Code property mapping
                .ForMember(dest => dest.IsActive, opt => opt.NullSubstitute(true)) // Set IsActive to true
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(request =>
                    new Supplier(request.SupplierCode, request.SupplierDescription, request.SupplierCnpj)));
        }
    }
}