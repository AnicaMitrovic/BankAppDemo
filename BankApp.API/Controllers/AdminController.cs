using Azure;
using BankApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            // Access the roles claim
            var rolesClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (rolesClaim != null)
            {
                string roleValue = rolesClaim.Value;

                if (roleValue == "Admin")
                {
                    var response = _service.GetCustomerList();
                    return Ok(response);
                }
                else if (roleValue == "User")
                {
                    return BadRequest();
                }
            }           
                return Unauthorized();         
        }
    }
}
