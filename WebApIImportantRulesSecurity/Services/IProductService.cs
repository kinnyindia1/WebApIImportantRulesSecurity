using WebApIImportantRulesSecurity.DTOs;
using WebApIImportantRulesSecurity.Models;

namespace WebApIImportantRulesSecurity.Services
{
    public interface IProductService
    {
        //List<Product> GetAll();
        (List<Product> Items, int Total) GetPaged(int page, int pageSize);
        Product? GetById(int id);
        Product Create(CreateProductDto dto);
    }
}
