using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRUDOperations.Core;
using CRUDOperations.Core.Models;

namespace CRUDOperations.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // public async Task<Products> GetProductByCode(string code)
        // {
        //    return await _unitOfWork.Products.GetByCodeAsync(code);
        // }

        public async Task<Products> CreateProduct(Products newProduct)
        {
            await _unitOfWork.Products
                .AddAsync(newProduct);
            await _unitOfWork.CommitAsync();
            return newProduct;
        }

        public async Task DeleteProduct(Products product)
        {
            _unitOfWork.Products.Remove(product);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<Products> GetProductById(int id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task UpdateProduct(Products ProductToBeUpdated, Products Product)
        {
            ProductToBeUpdated.Code = Product.Code;
            ProductToBeUpdated.Name = Product.Name;
            ProductToBeUpdated.Photo = Product.Photo;
            ProductToBeUpdated.LastUpdate = Product.LastUpdate;
            ProductToBeUpdated.Price = Product.Price;
            await _unitOfWork.CommitAsync();
        }
    }
}
