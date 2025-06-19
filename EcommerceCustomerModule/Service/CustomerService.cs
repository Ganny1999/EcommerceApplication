using AutoMapper;
using EcommerceCustomerModule.DataContext;
using EcommerceCustomerModule.Models;
using EcommerceCustomerModule.Models.Dtos;
using EcommerceCustomerModule.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceCustomerModule.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;
        public CustomerService(AppDbContext context, UserManager<Customer> userManager, IMapper mapper)
        {
            _context = context;  
            _userManager = userManager;
            _mapper= mapper;
        }
        public async Task<ApiResponse<CustomerResponseDTO>> RegisterCustomerAsync(CustomerRegistrationDTO customerRegistrationDTO)
        {
            try
            {
                if (_context.Customers.Any(u => u.Email == customerRegistrationDTO.Email))
                {
                    return new ApiResponse<CustomerResponseDTO>(400, "Email already exists");
                }

                var customer = new Customer();
                customer.FirstName = customerRegistrationDTO.FirstName;
                customer.LastName = customerRegistrationDTO.LastName;
                customer.UserName = customerRegistrationDTO.FirstName.ToLower() + customerRegistrationDTO.LastName.ToLower();
                customer.PhoneNumber = customerRegistrationDTO.PhoneNumber;
                customer.Email = customerRegistrationDTO.Email;
                customer.isActive = true;
                //customer = _mapper.Map<Customer>(customerRegistrationDTO);

                var registerCustomer = await _userManager.CreateAsync(customer, customerRegistrationDTO.Password);
                if (registerCustomer!=null)
                {
                    var IsCustomerAdded = await _context.Customers.FirstOrDefaultAsync(u => u.Email.ToLower() == customerRegistrationDTO.Email.ToLower());
                    //var IsCustomerAddedDto = _mapper.Map<Customer)ResponseDTO>(IsCustomerAdded);
                    var customerResponse = new CustomerResponseDTO()
                    {
                        FirstName = IsCustomerAdded.FirstName,
                        LastName = IsCustomerAdded.LastName,
                        Email = IsCustomerAdded.Email,
                        PhoneNumber = IsCustomerAdded.PhoneNumber,
                        Id = IsCustomerAdded.Id,
                    };

                    return new ApiResponse<CustomerResponseDTO>(customerResponse, 200, "Customer Added succesfullly.",true);
                }
                return new ApiResponse<CustomerResponseDTO>(500, $"An unexpected error occurred while processing your request");
            }
            catch(Exception e)
            {
                return new ApiResponse<CustomerResponseDTO>(500, $"An unexpected error occurred while processing your request {e}");
            }
            
        }
    }
}
