using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRUDOperations.Core.Models;

namespace CRUDOperations.Services.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetAllProducts();
        Task<Products> GetProductById(int id);
        // Task<Products> GetProductByCode(string code);
        Task<Products> CreateProduct(Products newProduct);
        Task UpdateProduct(Products ProductToBeUpdated, Products Product);
        Task DeleteProduct(Products Product);
    }
}
