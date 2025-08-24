using EcommerceProductModule.Models;
using EcommerceProductModule.Models.Dtos;
using EcommerceProductModule.Models.Dtos.ProductDto;
using EcommerceProductModule.Service;
using EcommerceProductModule.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProductModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            try
            {
                var result = await _productService.CreateProductAsync(productCreateDto);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto)
        {
            try
            {
                var result = await _productService.UpdateProductAsync(productUpdateDto);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Invalid operation");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteProduct/{ProductID:int}")]
        public async Task<ActionResult<ProductResponseDto>> DeleteProduct(int ProductID)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(ProductID);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetProductByID/{ProductID:int}")]
        public async Task<ActionResult<ApiResponse<ProductResponseDto>>> GetProductByID(int ProductID)
        {
            try
            {
                var result = await _productService.GetProductByIdAsync(ProductID);
                if (result != null)
                {
                    return new ApiResponse<ProductResponseDto>(200,true,result,$"Product found at {ProductID}");
                    //return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet("GetAllProductsByCategory/{CategoryID:int}")]
        public async Task<ActionResult<ProductResponseDto>> GetAllProductsByCategory(int CategoryID)
        {
            try
            {
                var result = await _productService.GetAllProductsByCategoryAsync(CategoryID);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Pagination API
        [HttpGet("GetProductData")]
        public List<Product> GetProductData(int page=1,int pageSize=10)
        {
            var ProdList = new List<Product>();
            for(int i=1;i<=100;i++)
            {
                ProdList.Add( new Product { Id=i,Description=$"Description :{i}",Price=10*i,CategoryId=1,StockQuantity=100,Name=$"Item {i}"});
            }

            var ProductPerPage = ProdList.Skip((page-1) * pageSize).Take(pageSize).ToList();
            return ProductPerPage;
        }
    }
}
