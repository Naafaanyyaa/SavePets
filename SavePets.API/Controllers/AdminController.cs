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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _adminService.GetUserById(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _adminService.GetUserList();
            return Ok(result);
        }

        [HttpPut("ban-user/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Put(string userId)
        {
            var result = await _adminService.BanUser(userId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPatch("change-role/{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Patch(string userId)
        {
            var result = await _adminService.ChangeRoleToAdmin(userId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("delete/{animalId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(string animalId)
        {
            await _adminService.DeleteAnimal(animalId);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
