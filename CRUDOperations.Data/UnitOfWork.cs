using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRUDOperations.Core;
using CRUDOperations.Core.Repositories;
using CRUDOperations.Data.Repositories;

namespace CRUDOperations.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryContext _context;
        private ProductRepository _productRepository;


        public UnitOfWork(InventoryContext context)
        {
            this._context = context;
        }

        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);

        

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
