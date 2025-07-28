using AutoMapper;
using EcommerceOrderModule.DataContext;
using EcommerceOrderModule.Models;
using EcommerceOrderModule.Models.Dtos;
using EcommerceOrderModule.Service.IExternalService;
using EcommerceOrderModule.Service.Iservice;
using Microsoft.EntityFrameworkCore;

namespace EcommerceOrderModule.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly ICartServiceExternal _cartServiceExternal;
        private readonly IMapper _mapper;
        public OrderService(AppDbContext context, ICartServiceExternal cartServiceExternal,IMapper mapper)
        {
            _context = context;
            _cartServiceExternal = cartServiceExternal;
            _mapper = mapper;
        }
        
        public byte[] GenerateDailyOrderReportExcel(DateTime dateTime)
        {
            throw new NotImplementedException();

            //var orderList = new List<Order>
            //{
            //    new Order{OrderNumber="1",OrderDate=DateTime.Now,CustomerID="111",TotalAmount= 1000 },
            //    new Order{OrderNumber="2",OrderDate=DateTime.Now,CustomerID="222",TotalAmount= 2000 },
            //    new Order{OrderNumber="3",OrderDate=DateTime.Now,CustomerID="333",TotalAmount= 3000 },
            //    new Order{OrderNumber="4",OrderDate=DateTime.Now,CustomerID="444",TotalAmount= 4000 }
            //};

            //var workbook =new XLWorkbook();

            //var workSheet = workbook.AddWorksheet("Daily Orders");
            //workSheet.Cell(1,1).Value = "Order ID";
            //workSheet.Cell(1,2).Value = "Date Time";
            //workSheet.Cell(1,3).Value = "Customer D=ID";
            //workSheet.Cell(1,4).Value = "Total Amount";

            //int row = 2;
            //foreach (var i in orderList)
            //{
            //    workSheet.Cell(row, 1).Value = i.OrderNumber;
            //    workSheet.Cell(row, 2).Value = i.OrderDate;
            //    workSheet.Cell(row, 3).Value = i.CustomerID;
            //    workSheet.Cell(row, 4).Value = i.TotalAmount;
            //    row++;
            //}

            //var stream = new MemoryStream();
            //workbook.SaveAs(stream);
            //return stream.ToArray();
        }

        // Fetch all the details filter by customer ID.
        public OrderResponseDto GetOrderByCustomerAsync(string CUstomerID)
        {
            throw new NotImplementedException();
        }
        // Fetch the order details by order ID.
        public OrderResponseDto GetOrderByIDAsync(string OrderID)
        {
            throw new NotImplementedException();
        }
        // Get the items from the cart
        // Add new order in Db and marks the customer’s cart as checked out.

        public async Task<ApiResponse<OrderResponseDto>> PlaceOrderAsync(int CartID)
        {
            try
            {
                // Get the Cart and CartItems
                var isCartEmpty = await _cartServiceExternal.GetCart(CartID);
                if (isCartEmpty != null)
                {
                    var CartItems = isCartEmpty.Data.CartItems;
                    // Create New Order
                    var placeOrder = new Order
                    {
                        OrderID = Guid.NewGuid().ToString(),
                        OrderNumber = Guid.NewGuid().ToString(),
                        CustomerID = isCartEmpty.Data.CustomerId,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = isCartEmpty.Data.TotalAmount
                    };
                    // Place new Order in the database
                    var isOrderSuccessed = _context.Orders.Add(placeOrder); 
                    await _context.SaveChangesAsync();

                    // Check if Order is Places or not.
                    var isOrderPlaced = await _context.Orders.FirstOrDefaultAsync(u=>u.OrderNumber == placeOrder.OrderNumber);
                    if(isOrderPlaced != null)
                    {
                        var OrderItemList = new List<OrderItem>();
                        foreach(var items in CartItems)
                        {
                            var orderItem = new OrderItem()
                            {
                                OderItemID = Guid.NewGuid().ToString(),
                                OrderID = isOrderPlaced.OrderID,
                                ProductID = items.ProductId,
                                Price = items.Price,
                                Quantity = items.Quantity,
                                TotalPrice = items.TotalItemPrice
                            };
                            OrderItemList.Add(orderItem);
                        }
                        await _context.OrderItems.AddRangeAsync(OrderItemList);
                        await _context.SaveChangesAsync();
                        var isOrderItemsAdded = await _context.OrderItems.Where(u => u.OrderID == isOrderPlaced.OrderID).ToListAsync();

                        isOrderPlaced.OrderItems = isOrderItemsAdded;

                        var OrderDto = _mapper.Map<OrderResponseDto>(isOrderPlaced);
                        OrderDto.OrderItems = _mapper.Map<List<OrderItemsResponseDto>>(isOrderPlaced.OrderItems);

                        // If Order successfully placed then clear the cart
                        
                        if (OrderDto != null && OrderDto.OrderItems !=null)
                        {
                             var CartClered = await _cartServiceExternal.ClearCart(isCartEmpty.Data.CustomerId);
                        }
                        return new ApiResponse<OrderResponseDto>(OrderDto,200, "Order has been placed!!!", true);
                    }
                }
                return new ApiResponse<OrderResponseDto>(404, "Cart is empty, kindly add item to place order!!!", true);
            }
            catch(Exception ex)
            {
                return new ApiResponse<OrderResponseDto>(500, $"Something went wrong: {ex}", false);
            }
        }
        // Updates the order status by ensuring allowed status transitions.
        public bool OrderStatusManagementAsync(string OrderID)
        {
            throw new NotImplementedException();
        }
    }
}
