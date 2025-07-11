using EcommerceCustomerModule.Models;
using EcommerceCustomerModule.Models.Dtos;
using EcommerceCustomerModule.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;


namespace EcommerceCustomerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
                _customerService = customerService;
        }
        [HttpPost("RegisterCustomer")]
        public async Task<ActionResult<CustomerResponseDTO>> RegisterCustomer([FromBody] CustomerRegistrationDTO customerRegistrationDTO)
        {
            try
            {
                var result =await _customerService.RegisterCustomerAsync(customerRegistrationDTO);
                if(result!=null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LogInCustomer")]
        public async Task<ActionResult<LoginResponseDTO>> LogInCustomer([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var result = await _customerService.LoginCustomerAsync(loginDTO);
                if(result!=null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<CustomerResponseDTO>> UpdateCustomer([FromBody] CustomerUpdateDTO customerUpdateDTO)
        {
            try
            {
                var result = await _customerService.UpdateCustomerAsync(customerUpdateDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpDelete("DeleteCustomer/{ID}")]
        public async Task<ActionResult<CustomerResponseDTO>> DeleteCustomer(string ID)
        {
            try
            {
                var result = await _customerService.DeleteCustomerAsync(ID);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("GetCustomerByID/{ID}")]
        public async Task<ActionResult<ApiResponse<CustomerResponseDTO>>> GetCustomerByID(string ID)
        {
            try
            {
                var result = await _customerService.GetCustomerByIDAsync(ID);
                if (result != null)
                {
                    return new ApiResponse<CustomerResponseDTO>(result,200,$"Customer found with {ID}",true);
                   // return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet("GetAllActiveOrInActiveUsers/{flag:int}")]
        public async Task<ActionResult<ApiResponse<List<CustomerResponseDTO>>>> GetAllActiveOrInActiveUsers(int flag)
        {
            try
            {
                var result = await _customerService.GetAllActiveOrInActiveUsersAsync(flag);
                if (result != null)
                {
                    return new ApiResponse<List<CustomerResponseDTO>>(result, 200, $"List of customers is {flag}", true);
                    // return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [HttpPut]
        
        public void GetAllUsers() 
        {
            var cust = new Customer()
            {
                FirstName = "Ganesh"
            };
            var rees = JsonConvert.SerializeObject(cust);
            var res = System.Text.Json.JsonSerializer.Serialize(cust);

            var rees1 = JsonConvert.DeserializeObject<Customer>(rees);
            var res1 = System.Text.Json.JsonSerializer.Deserialize<Customer>(res);
        }
    }
}
