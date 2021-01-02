using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUDOperations.Api.DTO;
using CRUDOperations.Core.Models;

namespace CRUDOperations.Api.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Products,ProductDTO>();
            CreateMap<ProductDTO, Products>();
        }
    }
}
