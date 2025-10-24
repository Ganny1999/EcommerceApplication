using EcommerceCustomerModule.Models;
using EcommerceCustomerModule.Models.Dtos;
using EcommerceCustomerModule.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(Roles = "admin,customer")]
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
        [Authorize(Roles = "admin,customer")]
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
        [Authorize(Roles = "admin,customer")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin,customer")]
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
        [Authorize(Roles = "admin")]
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
        [HttpGet("CreateRole/{role}")]
        [Authorize(Roles ="admin")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> CreateRole(string role)
        {
            try
            {
                var result = await _customerService.CreateRole(role);
                if (result.Status == true)
                {
                    return new ApiResponse<bool>(200, $"Role : {role.ToUpper()} has been created!", true);
                    // return Ok(result);
                }
                return new ApiResponse<bool>(400, $"Role : {role.ToUpper()} already exist!", false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
