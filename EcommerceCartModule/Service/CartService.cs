using AutoMapper;
using EcommerceCartModule.DataContext;
using EcommerceCartModule.Models;
using EcommerceCartModule.Models.Dtos;
using EcommerceCartModule.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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

        public async Task<ApiResponse<CartResponseDto>> UpdateCartAsync(UpdateCartDto updateCartDto)
        {
            try
            {
                var IsCustomerExist = await _customerService.GetCustomerByIDAsync(updateCartDto.CustomerId);
                var IsProductExist = await _productService.GetProductByIDAsync(updateCartDto.ProductId);
                var isCartExistsForCustomer = await _context.Carts.FirstOrDefaultAsync(u => u.CustomerId == IsCustomerExist.Data.Id);
                if (isCartExistsForCustomer!= null)
                {
                    var cartItem = await _context.CartItems.FirstOrDefaultAsync(u=>u.CartId==isCartExistsForCustomer.CartId && u.ProductId == updateCartDto.ProductId);
                    if(cartItem!=null)
                    {
                        cartItem.Quantity = updateCartDto.Quantity;
                        cartItem.TotalItemPrice = IsProductExist.Data.Price * cartItem.Quantity;
                        cartItem.UpdatedAt = DateTime.UtcNow;

                        _context.CartItems.Update(cartItem);
                        await _context.SaveChangesAsync();

                        var cartItems = await _context.CartItems.Where(u => u.CartId == isCartExistsForCustomer.CartId).ToListAsync();

                        var cartItemsDto = _mapper.Map<List<CartItemResponseDto>>(cartItems);
                        var cartDto = _mapper.Map<CartResponseDto>(isCartExistsForCustomer);
                        cartDto.CartItems = _mapper.Map<List<CartItemResponseDto>>(cartItemsDto);

                        return new ApiResponse<CartResponseDto>(cartDto, 200, "Cart updated successfully.", true);
                    }
                }
                return new ApiResponse<CartResponseDto>(400, $"Cart does not have Items with ", false);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartResponseDto>(500, $"Sothing went wrong : {ex}", false);
            }
        }

        public async Task<ApiResponse<bool>> ClearCartAsync(string customerID)
        {
            try
            {
                var IsCustomerExist = await _customerService.GetCustomerByIDAsync(customerID);
                var isCartExistsForCustomer = await _context.Carts.FirstOrDefaultAsync(u => u.CustomerId == IsCustomerExist.Data.Id);
                if(isCartExistsForCustomer != null)
                {
                    var cartItems = (from items in await _context.CartItems.ToListAsync()
                              where items.CartId == isCartExistsForCustomer.CartId
                              select items).ToList();

                    _context.CartItems.RemoveRange(cartItems);
                    var removeCart = _context.Carts.Remove(isCartExistsForCustomer);
                    await _context.SaveChangesAsync();

                    var isCartItemsDeleted = (from items in await _context.CartItems.ToListAsync()
                                    where items.CartId == isCartExistsForCustomer.CartId
                                    select items).ToList();
                    if (isCartItemsDeleted.Count==0)
                    {
                        return new ApiResponse<bool>(200, $"Cart has been cleared", true);
                    }

                }
                return new ApiResponse<bool>(400, $"Cart is empty", false);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, $"Something went wrong : {ex}", false);
            }
        }

        public async Task<ApiResponse<CartResponseDto>> GetCartAsync(int CartID)
        {
            try
            {
                // Check if Cart Is Not Empty
                var isCartFound = await _context.Carts.FirstOrDefaultAsync(u => u.CartId == CartID);
                isCartFound.CartItems = await _context.CartItems.Where(u => u.CartId == isCartFound.CartId).ToListAsync();

                // Fetch price from product & assign to products inside CartItem.
                
                /* Not working yet need to check external endpoint data is null.
                foreach(var item in isCartFound.CartItems)
                {
                    var product = await _productService.GetProductByIDAsync(CartID);
                    item.Price = product.Data.Price;
                }
                */
                if (isCartFound != null)
                {
                    var CartDto = _mapper.Map<CartResponseDto>(isCartFound);
                    CartDto.CartItems = _mapper.Map<List<CartItemResponseDto>>(isCartFound.CartItems);
                    return new ApiResponse<CartResponseDto>(CartDto, 200, "Cart details!", true);
                }
                return new ApiResponse<CartResponseDto>(404, "Cart details could not found!", false);
            }
            catch(Exception ex)
            {
                return new ApiResponse<CartResponseDto>(500, $"Something went wrong : {ex}", false);
            }
        }
    }
}
