using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Algorithm;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;

namespace SavePets.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(IEnumerable<PetResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] PetAllRequest request)
        {
            var result = await _petService.GetAllPetsByRequest(request);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(PetResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _petService.GetPetById(id);
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(PetResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromForm] PetRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _petService.CreateAsync(request, userId, Request.Form.Files ,Directory.GetCurrentDirectory());
            return StatusCode(StatusCodes.Status201Created, result);
        }


        [HttpPut("edit/{animalId}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(PetResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Put(string animalId, UpdateRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _petService.UpdateByIdAsync(userId, animalId, request);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete/{animalId}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(string animalId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _petService.DeleteByIdAsync(userId, animalId);
            return StatusCode(StatusCodes.Status200OK);
        }
        //[HttpPatch]
        //[Authorize(Roles = "User")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
    }
}
