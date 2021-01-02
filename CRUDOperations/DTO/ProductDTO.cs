using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDOperations.Api.DTO
{
    public class ProductDTO
    {
        // public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
