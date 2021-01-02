using System;
using System.Threading.Tasks;
using CRUDOperations.Core.Repositories;

namespace CRUDOperations.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        
        Task<int> CommitAsync();
    }
}
