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
        [HttpPost("createRandomLocation/{animalId}")]
        [ProducesResponseType(typeof(PetResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Update(string animalId)
        {
            var request = new GeoLocationRequest()
            {
                AnimalId = animalId,
                Count = new Random().Next(100, 1000),
                Latitude = new Random().NextDouble(-90.000000, 90.000000),
                Longitude = new Random().NextDouble(-180.000000, 180.000000),
                Alt = new Random().NextDouble(1.00, 5000.00),
            };

            var result = await _geoLocation.UpdateGeolocation(request);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }

    public static class RandomExtensions
    {
        public static double NextDouble(
            this Random random,
            double minValue,
            double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
