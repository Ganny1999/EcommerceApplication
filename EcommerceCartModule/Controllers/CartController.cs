using EcommerceCartModule.Models.Dtos;
using EcommerceCartModule.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCartModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("AddCart")]
        public async Task<ActionResult<CartResponseDto>> AddCart([FromBody] AddCartDto addCartDto)
        {
            try
            {
                var result = await _cartService.AddCartAsync(addCartDto);
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
    }
}
