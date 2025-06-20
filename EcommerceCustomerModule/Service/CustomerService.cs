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
                    return new ApiResponse<CustomerResponseDTO>(400, "Email already exists",false);
                }

                //var customer = new Customer();
                //customer.FirstName = customerRegistrationDTO.FirstName;
                //customer.LastName = customerRegistrationDTO.LastName;
                //customer.UserName = customerRegistrationDTO.FirstName.ToLower() + customerRegistrationDTO.LastName.ToLower();
                //customer.PhoneNumber = customerRegistrationDTO.PhoneNumber;
                //customer.Email = customerRegistrationDTO.Email;
                //customer.isActive = true;

                var customer = _mapper.Map<Customer>(customerRegistrationDTO);
                customer.isActive = true;

                var registerCustomer = await _userManager.CreateAsync(customer, customerRegistrationDTO.Password);
                if (registerCustomer!=null)
                {
                    var IsCustomerAdded = await _context.Customers.FirstOrDefaultAsync(u => u.Email.ToLower() == customerRegistrationDTO.Email.ToLower());
                    var customerResponse = _mapper.Map<CustomerResponseDTO>(IsCustomerAdded);
                    
                    
                    //var customerResponse = new CustomerResponseDTO()
                    //{
                    //    FirstName = IsCustomerAdded.FirstName,
                    //    LastName = IsCustomerAdded.LastName,
                    //    Email = IsCustomerAdded.Email,
                    //    PhoneNumber = IsCustomerAdded.PhoneNumber,
                    //    Id = IsCustomerAdded.Id,
                    //};

                    return new ApiResponse<CustomerResponseDTO>(customerResponse, 200, "Customer Added successfully.",true);
                }
                return new ApiResponse<CustomerResponseDTO>(500, $"An unexpected error occurred while processing your request",false);
            }
            catch(Exception e)
            {
                return new ApiResponse<CustomerResponseDTO>(500, $"An unexpected error occurred while processing your request : {e}",false);
            }
            
        }

        public async Task<ApiResponse<LoginResponseDTO>> LoginCustomerAsync(LoginDTO loginDTO)
        {
            try
            {
                var isCustomerExixts = await _context.Customers.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

                if (isCustomerExixts != null)
                {
                    var checkPassward = await _userManager.CheckPasswordAsync(isCustomerExixts, loginDTO.Password);
                    if (checkPassward == true)
                    {
                        var loginResponseDto = new LoginResponseDTO()
                        {
                            CustomerId = isCustomerExixts.Id,
                            CustomerName = isCustomerExixts.FirstName + " " + isCustomerExixts.LastName,
                            Message = "Login successful!"
                        };
                        return new ApiResponse<LoginResponseDTO>(loginResponseDto, 200, "Login successful!", true);
                    }
                    else
                    {
                        return new ApiResponse<LoginResponseDTO>(401, "Login failed successfully!", false);
                    }
                }
                return new ApiResponse<LoginResponseDTO>(400, "Email not found!", false);
            }
            catch(Exception e)
            {
                return new ApiResponse<LoginResponseDTO>(500, $"An unexpected error occurred while processing your request : {e}", false);
            }
        }

        public async Task<ApiResponse<CustomerResponseDTO>> UpdateCustomer(CustomerUpdateDTO customerUpdateDTO)
        {
            try
            {
                var isCustomerExixst = await _context.Customers.FirstOrDefaultAsync(u=>u.Id==customerUpdateDTO.CustomerId);
                if (isCustomerExixst!=null)
                {
                    //isCustomerExixst = _mapper.Map<Customer>(customerUpdateDTO); // Not traking 

                    isCustomerExixst.FirstName = customerUpdateDTO.FirstName;
                    isCustomerExixst.LastName = customerUpdateDTO.LastName;
                    isCustomerExixst.Email = customerUpdateDTO.Email;
                    isCustomerExixst.PhoneNumber = customerUpdateDTO.PhoneNumber;
                    isCustomerExixst.FirstName = customerUpdateDTO.FirstName;

                    await _context.SaveChangesAsync();

                    var customerResponse = _mapper.Map<CustomerResponseDTO>(isCustomerExixst);
                    //var customerResponse = new CustomerResponseDTO()
                    //{
                    //    Email= customerUpdateDTO.Email,
                    //    FirstName=customerUpdateDTO.FirstName,
                    //    LastName =customerUpdateDTO.LastName,
                    //    PhoneNumber=customerUpdateDTO.PhoneNumber
                    //};
                    return new ApiResponse<CustomerResponseDTO>(customerResponse, 200, "Customer data updated successfully!", true);
                }
                return new ApiResponse<CustomerResponseDTO>(400, $"Invalid customer ID, kindly provide correct ID.", false);
            }
            catch(Exception e)
            {
                return new ApiResponse<CustomerResponseDTO>(500, $"An unexpected error occurred while processing your request : {e}", false);
            }
        }

        public Task<ApiResponse<CustomerResponseDTO>> UpdateCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
