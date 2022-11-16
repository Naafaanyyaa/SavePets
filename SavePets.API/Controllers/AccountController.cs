using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities.Identity;

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
        [HttpGet("account-info/")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Get()
        {
            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.ProfileInfo(userIdFromToken);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("update-account/")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Put(UpdateAccountRequest accountRequest)
        {
            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result =  await _accountService.UpdateAccountInfoByUser(userIdFromToken, accountRequest);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("delete/")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync()
        {
            var userIdFromToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _accountService.DeleteAccountByUser(userIdFromToken);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
