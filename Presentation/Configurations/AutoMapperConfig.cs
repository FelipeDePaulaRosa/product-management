﻿using AutoMapper;
using Domain.Products.Entities;
using Application.Products.GetProducts;
using Application.Products.CreateProduct;
using Application.Products.GetProductByCode;
using Application.Products.UpdateProduct;

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
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description.Trim()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => true))
                .ForMember(dest => dest.ManufactureDate, opt => opt.MapFrom(x => x.ManufactureDate.Date))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(x => x.DueDate.Date))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(request =>
                    new Supplier(request.SupplierCode, request.SupplierDescription, request.SupplierCnpj)));

            CreateMap<Product, GetProductByCodeResponse>()
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(request =>
                    new GetProductSupplier(request.Supplier.Code, request.Supplier.Description, request.Supplier.Cnpj)));

            CreateMap<Product, GetProducts>()
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(request =>
                    new GetProductsSupplier(request.Supplier.Code, request.Supplier.Description, request.Supplier.Cnpj)));
            
            CreateMap<UpdateProductRequest, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description.Trim()))
                .ForMember(dest => dest.ManufactureDate, opt => opt.MapFrom(x => x.ManufactureDate.Date))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(x => x.DueDate.Date))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(request =>
                    new Supplier(request.SupplierCode, request.SupplierDescription, request.SupplierCnpj)));
        }
    }
}