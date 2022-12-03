using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class GeolocatorController : ControllerBase
    {
        private readonly IGeoLocation _geoLocation;

        public GeolocatorController(IGeoLocation geoLocation)
        {
            _geoLocation = geoLocation;
        }

        [Authorize(Roles = "User")]
        [HttpPut("createRandomLocation/{animalId}")]
        [ProducesResponseType(typeof(PetResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Update(string animalId, GeoLocationRequest geoLocation)
        {
            var result = await _geoLocation.UpdateGeolocation(animalId, geoLocation);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
