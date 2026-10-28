using WebApIImportantRulesSecurity.DTOs;
using WebApIImportantRulesSecurity.Models;

namespace WebApIImportantRulesSecurity.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m },
            new Product { Id = 2, Name = "Phone", Price = 499.99m },
            new Product { Id = 3, Name = "Laptop3", Price = 999.99m },
            new Product { Id = 4, Name = "Phone4", Price = 499.99m },
            new Product { Id = 5, Name = "Laptop5", Price = 999.99m },
            new Product { Id = 6, Name = "Phone6", Price = 499.99m },
            new Product { Id = 7, Name = "Laptop7", Price = 999.99m },
            new Product { Id = 8, Name = "Phone8", Price = 499.99m },
            new Product { Id = 9, Name = "Laptop9", Price = 999.99m },
            new Product { Id = 10, Name = "Phone10", Price = 499.99m },
            new Product { Id = 11, Name = "Laptop11", Price = 999.99m },
            new Product { Id = 12, Name = "Phone12", Price = 499.99m }
        };
        //public List<Product> GetAll() => _products;
        public (List<Product> Items, int Total) GetPaged(int page, int pageSize)
        {
            var total = _products.Count;
            var items = _products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return (items, total);
        }
        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
        public Product Create(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = _products.Max(p => p.Id) + 1,
                Name = dto.Name,
                Price = dto.Price
            };
            _products.Add(product);
            return product;
        }
    }
}
