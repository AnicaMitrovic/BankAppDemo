using BankApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _service;

        public AdminController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult Get()
        {
            //if()
            //{
                var response = _service.GetCustomerList();
                return Ok(response);
            //}
            //else 
            //{ 
            //    return BadRequest(); 
            //}
        }
    }
}
