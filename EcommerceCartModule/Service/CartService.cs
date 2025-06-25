using AutoMapper;
using EcommerceCartModule.DataContext;
using EcommerceCartModule.Models;
using EcommerceCartModule.Models.Dtos;
using EcommerceCartModule.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace EcommerceCartModule.Service
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomerServiceExternal _customerService;
        private readonly IProductServiceExternal _productService;
        public CartService(AppDbContext context, IMapper mapper, ICustomerServiceExternal customerService, IProductServiceExternal productService)
        {
            _context = context;
            _mapper = mapper;
            _customerService = customerService;
            _productService = productService;
        }
        public async Task<ApiResponse<CartResponseDto>> AddCartAsync(AddCartDto addCartDto)
        {
            try
            {
                var IsCustomerExist = await _customerService.GetCustomerByIDAsync(addCartDto.CustomerId);
                var IsProductExist = await _productService.GetProductByIDAsync(addCartDto.ProductId);
                var isItemExistsForCustomer = await _context.Carts.FirstOrDefaultAsync(u=>u.CustomerId == IsCustomerExist.Data.Id);
                // check if cart is empty or not
                if(isItemExistsForCustomer == null)
                {

                    var cart = new Cart()
                    {
                        CustomerId = IsCustomerExist.Data.Id,
                        IsCheckedOut = false,
                        CreatedAt  = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await _context.Carts.AddAsync(cart);
                    await _context.SaveChangesAsync();

                    var isCartAdded = await _context.Carts.FirstOrDefaultAsync(u => u.CustomerId == addCartDto.CustomerId);
                    
                    var cartItem = new CartItem()
                    {
                        CartId = isCartAdded.CartId,
                        ProductId = addCartDto.ProductId,
                        Quantity = addCartDto.Quantity,
                        Price = IsProductExist.Data.Price, // need to fetch from product service
                        TotalItemPrice = IsProductExist.Data.Price * addCartDto.Quantity,
                        UpdatedAt= DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    };
                    await _context.CartItems.AddAsync(cartItem);
                    await _context.SaveChangesAsync();

                    var cartItems = await _context.CartItems.Where(u => u.CartId == isCartAdded.CartId).ToListAsync();

                    var cartItemsDto = _mapper.Map<List<CartItemResponseDto>>(cartItems);
                    var cartDto = _mapper.Map<CartResponseDto>(isCartAdded);
                    cartDto.CartItems = cartItemsDto;

                    return new ApiResponse<CartResponseDto>(cartDto,200,"Cart Added successfully.",true);
                }
                // cart is not empty
                // check of item already exists
                // 
                else
                {
                    // product not exist then add new product
                    var isProductExixtInCartItem = await _context.CartItems.FirstOrDefaultAsync(u=>u.ProductId==addCartDto.ProductId);
                    if(isProductExixtInCartItem == null)
                    {
                        var cartItem = new CartItem()
                        {
                            ProductId = addCartDto.ProductId,
                            CartId = isItemExistsForCustomer.CartId,
                            Quantity = addCartDto.Quantity,
                            TotalItemPrice = IsProductExist.Data.Price * addCartDto.Quantity,
                            UpdatedAt = DateTime.UtcNow,
                            CreatedAt = DateTime.UtcNow
                        };
                        await _context.CartItems.AddAsync(cartItem);
                        await _context.SaveChangesAsync();

                        //var cart = await _context.Carts.FirstOrDefaultAsync(u=>u.CartId == isItemExistsForCustomer.CartId);

                        var cartItems = await _context.CartItems.Where(u => u.CartId == isItemExistsForCustomer.CartId).ToListAsync();
                        var cartItemsDto = _mapper.Map<List<CartItemResponseDto>>(cartItems);
                        var cartDto = _mapper.Map<CartResponseDto>(isItemExistsForCustomer);
                        cartDto.CartItems = _mapper.Map<List<CartItemResponseDto>>(cartItemsDto);

                        return new ApiResponse<CartResponseDto>(cartDto, 200, "Cart Added successfully.", true);
                    }
                    // update product count
                    else
                    {
                        isProductExixtInCartItem.Quantity = isProductExixtInCartItem.Quantity + addCartDto.Quantity;
                        isProductExixtInCartItem.TotalItemPrice = IsProductExist.Data.Price * isProductExixtInCartItem.Quantity ;
                        _context.CartItems.Update(isProductExixtInCartItem);
                        await _context.SaveChangesAsync();

                        var cartItems = await _context.CartItems.Where(u => u.CartId == isItemExistsForCustomer.CartId).ToListAsync();
                        var cartItemsDto = _mapper.Map<List<CartItemResponseDto>>(cartItems);
                        var cartDto = _mapper.Map<CartResponseDto>(isItemExistsForCustomer);
                        cartDto.CartItems = _mapper.Map<List<CartItemResponseDto>>(cartItemsDto);

                        return new ApiResponse<CartResponseDto>(cartDto, 200, "Cart Added successfully.", true);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponseDto>(500,$"Sothing went wrong : {ex}",false);
            }
        }
    }
}
