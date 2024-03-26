using AutoMapper;
using Domain.Products.Entities;
using Application.Products.CreateProduct;

namespace Presentation.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateProductMap();
        }
        
        private void CreateProductMap()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(dest => dest.Code, opt => opt.Ignore()) // Ignore Code property mapping
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => true)) // Set IsActive to true
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(request =>
                    new Supplier(request.SupplierCode, request.SupplierDescription, request.SupplierCnpj)));
        }
    }
}