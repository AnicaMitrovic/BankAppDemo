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
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BankAccountController(IBankAccountService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        // we use this method in get all Bank accounts for this user
        private int GetLoggedInCustomerId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        private string GetLoggedInUserRole() => _httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.Role)!;

        [HttpGet("GetBankAccountsByCustomerId")]
        public async Task<ActionResult<ServiceResponse<List<GetBankAccountsResponseDto>>>> GetBankAccountsByCustomerId()
        {
            string roleValue = GetLoggedInUserRole();
            if (roleValue == "Admin")
            {
                return Ok("You are logged in as admin and you cannot have bank accounts.");
            }
            if (roleValue == "Customer")
            {
                var customerId = GetLoggedInCustomerId();
                var response = await _service.GetBankAccountsByCustomerId(customerId);
                return Ok(response);
            }

            return Unauthorized();
        }
    }
}
