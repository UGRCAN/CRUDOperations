using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using CRUDOperations.Api.DTO;
using CRUDOperations.Api.Validators;
using CRUDOperations.Core.Models;
using CRUDOperations.Services.Services;

namespace CRUDOperations.Api.Controllers
{

   

    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private readonly IMapper _mapper;
        private SaveProductValidator _validator;
        public ProductController(IProductService productService, IMapper mapper, SaveProductValidator validator)
        {
            _productService = productService;
            _mapper = mapper;
            _validator = validator;
        }
        // GET: api/<ProductController>
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>Product objects</returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                var mappedProducts = _mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(products);
                return Ok(mappedProducts);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        // GET: api/<ProductController>/5
        /// <summary>
        /// gets products by param id 
        /// </summary>
        /// <param name="id" example="3"></param>
        /// <returns>Product object</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null) return NotFound();

                var mappedProduct = _mapper.Map<Products, ProductDTO>(product);
                return Ok(mappedProduct);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
                
            
            
            
        }
        
        // POST: api/<ProductController>
        /// <summary>
        /// Creates new product 
        /// </summary>
        /// <remarks></remarks>
        /// <param name="id" example="123">The product id</param>
        /// <response code="200">Product created</response>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your product right now</response>
        [HttpPost("")]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductDTO productDto)
        {
            try
            {
                var validator = new SaveProductValidator(_productService);
                var validationResult = await validator.ValidateAsync(productDto);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var productTOCreate = _mapper.Map<ProductDTO, Products>(productDto);



                var newProduct = await _productService.CreateProduct(productTOCreate);

                var product = await _productService.GetProductById(newProduct.Id);

                var productResource = _mapper.Map<Products, ProductDTO>(product);

                return Ok(productResource);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }
        //POST: api/<ProductController/export
        /// <summary>
        /// Updates product by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveProductResource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] ProductDTO saveProductResource)
        {
            try
            {
                
                var validationResult = await _validator.ValidateAsync(saveProductResource);

                var requestIsInvalid = id == 0 || !validationResult.IsValid;

                if (requestIsInvalid)
                    return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                var productToBeUpdate = await _productService.GetProductById(id);

                if (productToBeUpdate == null)
                    return NotFound();

                var product = _mapper.Map<ProductDTO, Products>(saveProductResource);

                await _productService.UpdateProduct(productToBeUpdate, product);

                var updatedProduct = await _productService.GetProductById(id);
                var updatedProductResource = _mapper.Map<Products, ProductDTO>(updatedProduct);

                return Ok(updatedProductResource);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }

        /// <summary>
        /// Deletes product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                var product = await _productService.GetProductById(id);

                if (product == null)
                    return NotFound();

                await _productService.DeleteProduct(product);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }

        // POST: api/<ProductController>
        /// <summary>
        /// Exports products to Excel 
        /// </summary>
        /// <remarks></remarks>
        /// <param name="name" example="export">Export event</param>
        /// <response code="200">Returns Excel file</response>
        
        [HttpPost]
        [Route("{name}")]
        public async Task<IActionResult> ExportToExcel(string name)
        {
            try
            {
                if (name != "export") return BadRequest("invalid URL. To export URL: api/product/export");


                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "products.xlsx";
                return File(SetExcel(), contentType, fileName);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
           
        }

        private byte[] SetExcel()
        {
            using (var stream = new MemoryStream())
            {
                
                var workbook = new XLWorkbook();
                IXLWorksheet worksheet = workbook.Worksheets.Add("Products");
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Code";
                worksheet.Cell(1, 3).Value = "Name";
                worksheet.Cell(1, 4).Value = "Photo";
                worksheet.Cell(1, 5).Value = "Price";
                worksheet.Cell(1, 6).Value = "LastUpdate";
                var allProducts = _productService.GetAllProducts().Result.ToList();
                for (int index = 1; index <= allProducts.Count(); index++)
                {
                    worksheet.Cell(index + 1, 1).Value = allProducts[index - 1].Id;
                    worksheet.Cell(index + 1, 2).Value = allProducts[index - 1].Code;
                    worksheet.Cell(index + 1, 3).Value = allProducts[index - 1].Name;
                    worksheet.Cell(index + 1, 4).Value = allProducts[index - 1].Photo;
                    worksheet.Cell(index + 1, 5).Value = allProducts[index - 1].Price;
                    worksheet.Cell(index + 1, 6).Value = allProducts[index - 1].LastUpdate;
                }

                workbook.SaveAs(stream);
                return stream.ToArray();
                
            }
        }
    }
}
