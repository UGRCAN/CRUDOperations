using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRUDOperations.Core.Models;

namespace CRUDOperations.Core.Repositories
{
    public interface IProductRepository : IRepository<Products>
    {
        // ValueTask<Products> GetByCodeAsync(string code);
    }
}
