using EcommerceCustomerModule.Models.Dtos;
using EcommerceCustomerModule.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerByID(string ID)
        {
            try
            {
                var result = await _customerService.GetCustomerByIDAsync(ID);
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
    }
}
