using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;

namespace SavePets.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("get-account/{id}", Name = "get-account")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Get(string id)
        {
            //TODO: throw error if IP in token and in GET request doesn`t math
            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.ProfileInfo(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("update-account/{id}", Name = "update-account")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Put(string id,UpdateAccountRequest accountRequest)
        {
            //TODO: throw error if IP in token and in GET request doesn`t math
            var result =  await _accountService.UpdateAccountInfoByUser(id, accountRequest);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("delete-account/{id}", Name = "delete-account")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            //TODO: throw error if IP in token and in GET request doesn`t math
            await _accountService.DeleteAccountByUser(id);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
