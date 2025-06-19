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
    }
}
