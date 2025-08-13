using AutoMapper;
using Mago.Services.ProductApi.Data;
using Mago.Services.ProductApi.Models.DbModels;
using Mago.Services.ProductApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ProductApiController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            ResponseDto<IEnumerable<ProductDto>> responseDto = new();
            try
            {
                IEnumerable<Product> products = _appDbContext.Products.ToList();

                if (products == null || !products.Any())
                {
                    return NotFound("No products found.");
                }
                responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(products);

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetProductById(int id)
        {
            ResponseDto<ProductDto> responseDto = new();

            try
            {
                Product product = await _appDbContext.Products.FirstOrDefaultAsync(u => u.ProductId == id) ?? new Product();

                if (product == null || product.ProductId <= 0)
                {
                    responseDto.IsSuccess = false;
                    responseDto.UserMessage = ($"No product found for Id {id}.");
                }
                else
                {
                    ProductDto productDto = _mapper.Map<ProductDto>(product);
                    responseDto.Result = productDto;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(responseDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            ResponseDto<ProductDto> responseDto = new();

            try
            {
                Product product = _mapper.Map<Product>(productDto);
                if (product == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.UserMessage = "Product data is invalid.";
                    return BadRequest(responseDto);
                }

                if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
                {
                    responseDto.IsSuccess = false;
                    responseDto.UserMessage = "Product name and price must be provided.";
                    return BadRequest(responseDto);
                }

                if (product.ProductId > 0)
                {
                    // If ProductId is provided, update the existing product
                    Product existingProduct = await _appDbContext.Products.FirstOrDefaultAsync(u => u.ProductId == product.ProductId);
                    if (existingProduct != null)
                    {
                        existingProduct.Name = product.Name;
                        existingProduct.Price = product.Price;
                        existingProduct.Description = product.Description;
                        existingProduct.CategoryName = product.CategoryName;
                        existingProduct.ImageUrl = product.ImageUrl;
                        _appDbContext.Update(existingProduct);
                        product = existingProduct; // Use the updated product
                    }
                }
                else
                {
                    // If ProductId is not provided, create a new product

                    _appDbContext.Add(product);

                }
                _appDbContext.SaveChanges();
                ProductDto createdProductDto = _mapper.Map<ProductDto>(product);
                responseDto.Result = createdProductDto;
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(responseDto);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Put([FromBody] ProductDto productDto)
        {
            ResponseDto<ProductDto> responseDto = new();

            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _appDbContext.Update(product);
                _appDbContext.SaveChanges();

                ProductDto createdProductDto = _mapper.Map<ProductDto>(product);
                responseDto.Result = createdProductDto;
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(responseDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            ResponseDto<ProductDto> responseDto = new();

            try
            {
                Product product = _appDbContext.Products.FirstOrDefault(u => u.ProductId == id) ?? new Product();
                _appDbContext.Remove(product);
                _appDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(responseDto);
        }
    }
}
