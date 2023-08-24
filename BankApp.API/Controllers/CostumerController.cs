using BankApp.Core.Interfaces;
using BankApp.Domain.Models;
using BankApp.Domain.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IBankAccountService _bankAccService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(ICustomerService service, IHttpContextAccessor httpContextAccessor, IBankAccountService bankAccountService)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _bankAccService = bankAccountService;
        }

        // we use this method in get all Bank accounts for this user
        private int GetLoggedInCustomerId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<ServiceResponse<List<Customer>>>> GetAllCustomers()
        {
            // Access the roles claim
            var rolesClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (rolesClaim != null)
            {
                string roleValue = rolesClaim.Value;

                if (roleValue == "Admin")
                {
                    var response = await _service.GetCustomerList();
                    return Ok(response);
                }
            }
            return Unauthorized();
        }

        [HttpPost("AddNewCustomer")]
        public async Task<ActionResult<ServiceResponse<List<Customer>>>> AddNewCustomer(CustomerCreateDto customer)
        {
            try
            {
                var rolesClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (rolesClaim != null)
                {
                    string roleValue = rolesClaim.Value;

                    if (roleValue == "Admin")
                    {
                        var response = await _service.AddNewCustomer(customer);

                        var newBancAcc = new BankAccountCreateDto()
                        {
                            Type = "personal",
                            Balance = 0
                        };

                        return Ok(response);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
