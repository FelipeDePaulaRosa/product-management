namespace Domain.Products.Entities
{
    public class Supplier
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Cnpj { get; set; }
        
        public Supplier(string code, string description, string cnpj)
        {
            Code = code;
            Description = description;
            Cnpj = cnpj;
        }
    }
}