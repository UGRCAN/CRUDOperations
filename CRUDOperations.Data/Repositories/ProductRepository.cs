using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDOperations.Core.Models;
using CRUDOperations.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperations.Data.Repositories
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        public ProductRepository(InventoryContext context) : base(context)
        {
        }

        // public ValueTask<Products> GetByCodeAsync(string code)
        // {
        //      return Context.Set<Products>().FirstOrDefaultAsync(x => x.Code == code);
        // }
    }
}
