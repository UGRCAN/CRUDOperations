using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CRUDOperations.Api.DTO;
using CRUDOperations.Services.Services;
using FluentValidation;

namespace CRUDOperations.Api.Validators
{
    public class SaveProductValidator: AbstractValidator<ProductDTO>
    {
        private IProductService _productService;
        /// <summary>
        /// Validates create product and update product process
        /// </summary>
        /// <param name="productService"></param>
        public SaveProductValidator(IProductService productService)
        {
            _productService = productService;

            RuleFor(m => m.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(999).WithMessage("Price must be greater than equal 999");

            RuleFor(m => m.Code).NotEmpty().Must(IsUnique).WithMessage("Product Code must be unique");

        }
        /// <summary>
        /// Checks new product record or updated product code is unique or not
        /// </summary>
        /// <param name="product"></param>
        /// <param name="code"></param>
        /// <returns>true, false</returns>
        public bool IsUnique(ProductDTO product, string code)
        {
           var foundProduct = _productService.GetAllProducts().Result.FirstOrDefault(x => x.Code == code);
           return foundProduct == null;
        }

    }
}
