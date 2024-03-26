using System;
using Domain.Utils.Entities;

namespace Domain.Products.Entities
{
    public class Product : AggregateRoot<Guid>
    {
        public string Code { get; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime ManufactureDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public Supplier Supplier { get; private set; }

        private Product() { }
        
        public Product(
            string description,
            bool isActive,
            DateTime manufactureDate,
            DateTime dueDate,
            string supplierCode,
            string supplierDescription,
            string supplierCnpj)
        {
            Description = description;
            IsActive = isActive;
            ManufactureDate = manufactureDate;
            DueDate = dueDate;
            Supplier = new Supplier(supplierCode, supplierDescription, supplierCnpj);
        }
    }
}